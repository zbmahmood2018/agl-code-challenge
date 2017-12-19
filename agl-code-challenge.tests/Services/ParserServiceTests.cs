using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using agl_code_challenge.Configuration;
using agl_code_challenge.Models;
using agl_code_challenge.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace agl_code_challenge.tests.Services
{
    public class ParserServiceTests
    {
        private Mock<IHttpClient> _mockService;
        private ParseService _service;

        private void SetupData(bool hasValidContent = true, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var config = new AppSettings
            {
                UrlPath = statusCode == HttpStatusCode.OK ? "ValidURL" : "Invalid URL"
            };

            var response = new HttpApiResponse
            {
                StatusCode = statusCode,
                Content = statusCode == HttpStatusCode.OK
                    ? hasValidContent
                        ? @"[ { ""name"": ""Bob"", ""gender"": ""Male"", ""age"": 23, ""pets"": [ { ""name"": ""Garfield"", ""type"": ""Cat"" }, { ""name"": ""Fido"", ""type"": ""Dog""  }] }]"
                        : @"{ ""name"": ""Bob"", ""gender"": ""Male"", ""pets"": [ { ""name"": ""Garfield"", ""type"": ""Cat"" }] }"
                    : null
            };

            var mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(c => c.Value)
                .Returns(config);

            _mockService = new Mock<IHttpClient>();

            if (statusCode == HttpStatusCode.OK)
            {
                _mockService.Setup(x => x.GetAsync(It.IsAny<string>()))
                    .Returns(Task.FromResult(response));
            }
            else
            {
                _mockService.Setup(x => x.GetAsync(It.IsAny<string>()))
                    .Throws<HttpRequestException>();
            }

            var mockLogger = new Mock<ILogger<ParseService>>();

            _service = new ParseService(_mockService.Object, mockConfig.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ParseData_Pass_Test()
        {
            SetupData();
            var result = await _service.ParseData<Person>();
            _mockService.Verify();

            Assert.NotNull(result != null);
            var person = result.FirstOrDefault();
            Assert.NotNull(person);
            Assert.Equal("Bob", person.Name);
            Assert.Equal("Male", person.Gender);
            Assert.Equal(23, person.Age);
            Assert.Equal(2, person.Pets.Count);
        }

        [Fact]
        public async Task ParseData_DataException_Test()
        {
            SetupData(false);
            var ex = await Assert.ThrowsAsync<DataParseException>(() => _service.ParseData<Person>());
            _mockService.Verify();

            Assert.Equal("Unable to Parse Data", ex.Message);
        }

        [Fact]
        public async Task ParseData_RequestException_Test()
        {
            SetupData(false, HttpStatusCode.BadRequest);
            _mockService.Verify();

            var ex = await Assert.ThrowsAsync<HttpRequestException>(() => _service.ParseData<Person>());
        }
    }
}