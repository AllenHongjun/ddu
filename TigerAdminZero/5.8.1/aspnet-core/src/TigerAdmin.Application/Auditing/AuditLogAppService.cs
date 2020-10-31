using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.EntityHistory;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Castle.DynamicProxy.Internal;
using Castle.Windsor.Installer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TigerAdmin.Auditing.Dto;
using TigerAdmin.Authorization;
using TigerAdmin.Authorization.Users;
using TigerAdmin.Common.Dto;
using System.Linq.Dynamic.Core;
using Nito.AsyncEx;

namespace TigerAdmin.Auditing
{
    [DisableAuditing]
    //[AbpAuthorize(PermissionNames.Pages_Administration)]
    public class AuditLogAppService : TigerAdminAppServiceBase, IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<EntityChange, long> _entityChangeRepository;
        private readonly IRepository<EntityChangeSet, long> _entityChangeSetRepository;
        private readonly IRepository<EntityPropertyChange, long> _entityPropertyChangeReposity;
        private readonly IRepository<User, long> _userRepository;
        private readonly IAbpStartupConfiguration _abpStartupConfiguration;

        public AuditLogAppService(
            IRepository<AuditLog, long> auditLogRepository, 
            IRepository<EntityChange, long> entityChangeRepository, 
            IRepository<EntityChangeSet, long> entityChangeSetRepository,
            IRepository<EntityPropertyChange, long> entityPropertyChangeReposity,
            IRepository<User, long> userRepository,
            IAbpStartupConfiguration abpStartupConfiguration)
        {
            _auditLogRepository = auditLogRepository;
            _entityChangeRepository = entityChangeRepository;
            _entityChangeSetRepository = entityChangeSetRepository;
            _entityPropertyChangeReposity = entityPropertyChangeReposity;
            _userRepository = userRepository;
            _abpStartupConfiguration = abpStartupConfiguration;
        }

        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input)
        {
            var query = CreateAuditLogAndUserQuery(input);
            var resultCount = await query.CountAsync();
            var resultList = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var auditLogListDtos = ConvertToAuditLogListDtos(resultList);
            return new PagedResultDto<AuditLogListDto>(resultCount, auditLogListDtos);

        }

        public  Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input)
        {
            //var auditLogs = await CreateAuditLogAndUserQuery(input)
            //    .AsNoTracking()
            //    .OrderByDescending(al => al.AuditLog.ExecutionTime)
            //    .ToListAsync();

            //var auditLogListDtos = ConvertToAuditLogListDtos(auditLogs);
            //return _auditLogListExcelExporter.ExportToFile(auditLogListDtos);
             throw new NotImplementedException();
        }

        public async Task<PagedResultDto<EntityChangeListDto>> GetEntityChanges(GetEntityChangeInput input)
        {
            var query = CreateEntityChangesAndUserQuery(input);
            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var entityChangeListDtos = ConvertToEntityChangeListDtos(results);

            return new PagedResultDto<EntityChangeListDto>(resultCount, entityChangeListDtos);


            //throw new NotImplementedException();
        }

        public Task<FileDto> GetEntityChangesToExcel(GetEntityChangeInput input)
        {
            throw new NotImplementedException();
        }

        public List<NameValueDto> GetEntityHistoryObjectTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EntityPropertyChangeDto>> GetEntityPropertyChanges(long entityChangeId)
        {
            var entityPropertyChanges = (await _entityPropertyChangeReposity.GetAllListAsync())
                .Where(epc => epc.EntityChangeId == entityChangeId);

            return ObjectMapper.Map<List<EntityPropertyChangeDto>>(entityPropertyChanges);
            //throw new NotImplementedException();
        }

        public async Task<PagedResultDto<EntityChangeListDto>> GetEntityTypeChanges(GetEntityTypeChangeInput input)
        {
            var entityId = "\"" + input.EntityId + "\"";

            var query = from entityChangeSet in _entityChangeSetRepository.GetAll()
                        join entityChange in _entityChangeRepository.GetAll() on entityChangeSet.Id equals entityChange.EntityChangeSetId
                        join user in _userRepository.GetAll() on entityChangeSet.UserId equals user.Id
                        where entityChange.EntityTypeFullName == input.EntityTypeFullName &&
                        (entityChange.EntityId == input.EntityId || entityChange.EntityId == entityId)
                        select new EntityChangeAndUser
                        {
                            EntityChange = entityChange,
                            User = user
                        };
            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var entityChangeListDtos = ConvertToEntityChangeListDtos(results);
            return new PagedResultDto<EntityChangeListDto>(resultCount, entityChangeListDtos);

            //throw new NotImplementedException();
        }


        private List<AuditLogListDto> ConvertToAuditLogListDtos(List<AuditLogAndUser> result)
        {
            return result.Select(result =>
            {
                var auditLogListDto = ObjectMapper.Map<AuditLogListDto>(result.AuditLog);
                auditLogListDto.UserName = result.User?.UserName;
                //auditLogListDto.ServiceName = _namespaceStripper
                return auditLogListDto;
            }).ToList();
        }


        private IQueryable<AuditLogAndUser> CreateAuditLogAndUserQuery(GetAuditLogsInput input)
        {
            var query = from auditLog in _auditLogRepository.GetAll()
                        join user in _userRepository.GetAll() on auditLog.UserId equals user.Id into userJoin
                        from joinedUser in userJoin.DefaultIfEmpty()
                        where auditLog.ExecutionTime >= input.StartDate && auditLog.ExecutionTime <= input.EndDate
                        select new AuditLogAndUser
                        {
                            AuditLog = auditLog,
                            User = joinedUser
                        };
            query = query
                .WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.User.UserName.Contains(input.UserName))
                .WhereIf(!input.ServiceName.IsNullOrWhiteSpace(), item => item.AuditLog.ServiceName.Contains(input.ServiceName))
                .WhereIf(!input.MethodName.IsNullOrWhiteSpace(), item => item.AuditLog.MethodName.Contains(input.MethodName))
                .WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(), item => item.AuditLog.BrowserInfo.Contains(input.BrowserInfo))
                .WhereIf(!input.MinExecutionDuration.HasValue && input.MinExecutionDuration > 0, item => item.AuditLog.ExecutionDuration <= input.MinExecutionDuration.Value)
                .WhereIf(input.HasException == true, item => item.AuditLog.Exception != null && item.AuditLog.Exception != "")
                .WhereIf(input.HasException == false, item => item.AuditLog.Exception == null || item.AuditLog.Exception == "");
            return query;
        }


        private List<EntityChangeListDto> ConvertToEntityChangeListDtos(List<EntityChangeAndUser> results)
        {
            return results.Select(
                    result =>
                    {
                        var entityChangeListDto = ObjectMapper.Map<EntityChangeListDto>(result.EntityChange);
                        entityChangeListDto.UserName = result.User?.UserName;
                        return entityChangeListDto;
                    }
                ).ToList();
        }



        private IQueryable<EntityChangeAndUser> CreateEntityChangesAndUserQuery(GetEntityChangeInput input)
        {
            var query = from entityChangeSet in _entityChangeSetRepository.GetAll()
                        join entityChange in _entityChangeRepository.GetAll() on entityChangeSet.Id equals entityChange.EntityChangeSetId
                        join user in _userRepository.GetAll() on entityChangeSet.UserId equals user.Id
                        where entityChange.ChangeTime >= input.StartDate && entityChange.ChangeTime <= input.EndDate
                        select new EntityChangeAndUser
                        {
                            EntityChange = entityChange,
                            User = user
                        };

            var query2 = query
                 .WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.User.UserName.Contains(input.UserName))
                 .WhereIf(!input.EntityTypeFullName.IsNullOrWhiteSpace(), item => item.EntityChange.EntityTypeFullName.Contains(input.EntityTypeFullName));
            return query2;
        }

    }
}
