using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Demo;

namespace TigerAdmin.Tests.Demo.Task
{
    public class TaskAppService_Tests: TigerAdminTestBase
    {

        private readonly ITaskAppService _taskAppService;

        public TaskAppService_Tests()
        {
            _taskAppService = Resolve<ITaskAppService>();
        }

        public async System.Threading.Tasks.Task Should_Get_All_Tasks()
        {
            //Act
            var output = await _taskAppService.GetAll(new GetAllTasksInput());
            output.Items.Count.ShouldBe(3);
        }

        public async System.Threading.Tasks.Task Should_Get_Filtered_Tasks()
        {
            var output = await _taskAppService.GetAll(new GetAllTasksInput { State = TaskState.Open });

            //Assert
            output.Items.ShouldAllBe(t => t.State == TaskState.Open);
        }
    }
}
