using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using agl_code_challenge.Controllers;
using agl_code_challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace agl_code_challenge.tests.Controller
{
    public class HomeControllerTests
    {
        private Mock<IPeopleService> _mockService;
        private HomeController _controller;

        private void SetupData(bool isValid = true)
        {
            _mockService = new Mock<IPeopleService>();

            if (isValid)
            {
                _mockService.Setup(x => x.GetCatListByOwnerGender())
                    .Returns(Task.FromResult(new Dictionary<string, List<string>>()));
            }
            else
            {
                _mockService.Setup(x => x.GetCatListByOwnerGender())
                    .Throws<HttpRequestException>();
            }
            _controller = new HomeController(_mockService.Object);
        }

        [Theory]
        [InlineData(true, "Index")]
        [InlineData(false, "Error")]
        public async Task Index_Test(bool isValid, string viewName)
        {
            SetupData(isValid);
            var result = await _controller.Index() as ViewResult;
            _mockService.Verify();

            Assert.NotNull(result);
            Assert.Equal(viewName, result.ViewName);
        }
    }
}