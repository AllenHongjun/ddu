using System.Threading.Tasks;
using TigerAdmin.Models.TokenAuth;
using TigerAdmin.Web.Controllers;
using Shouldly;
using Xunit;

namespace TigerAdmin.Web.Tests.Controllers
{
    public class HomeController_Tests: TigerAdminWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}