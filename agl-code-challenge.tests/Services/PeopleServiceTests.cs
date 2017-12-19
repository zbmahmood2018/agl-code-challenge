using System.Collections.Generic;
using System.Threading.Tasks;
using agl_code_challenge.Models;
using agl_code_challenge.Services;
using Moq;
using Xunit;

namespace agl_code_challenge.tests.Services
{
    public class PeopleServiceTests
    {
        private Mock<IParser> _mockService;
        private PeopleService _service;

        private void SetupData(bool hasContent = true)
        {
            List<Person> response = null;
            if (hasContent)
            {
                response = new List<Person>
                {
                    new Person
                    {
                        Name = "Bob",
                        Gender = "Male",
                        Age = 23,
                        Pets = new List<Pet>
                        {
                            new Pet {Name = "Tom", Type = "Cat"},
                            new Pet {Name = "Fido", Type = "Dog"}
                        }
                    },
                    new Person
                    {
                        Name = "Jennifer",
                        Gender = "Female",
                        Age = 18,
                        Pets = new List<Pet>
                        {
                            new Pet {Name = "Garfield", Type = "Cat"}
                        }
                    },
                    new Person
                    {
                        Name = "Fred",
                        Gender = "Male",
                        Age = 40,
                        Pets = new List<Pet>
                        {
                            new Pet {Name = "Garfield", Type = "Cat"},
                            new Pet {Name = "Sam", Type = "Dog"}
                        }
                    }
                };
            }

            _mockService = new Mock<IParser>();
            _mockService.Setup(x => x.ParseData<Person>())
                    .Returns(Task.FromResult(response));
            
            _service = new PeopleService(_mockService.Object);
        }

        [Fact]
        public async Task ParseData_Pass_Test()
        {
            var expected = new Dictionary<string, List<string>>
            {
                { "Male", new List<string> { "Garfield", "Tom" } },
                { "Female", new List<string> { "Garfield" } }
            };

            SetupData();
            var actual = await _service.GetCatListByOwnerGender();
            _mockService.Verify();

            Assert.NotNull(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            foreach (var item in expected)
            {
                Assert.True(actual.ContainsKey(item.Key));
                Assert.Equal(item.Value, actual[item.Key]);
            }
        }

        [Fact]
        public async Task ParseData_DataException_Test()
        {
            SetupData(false);
            var ex = await Assert.ThrowsAsync<DataParseException>(() => _service.GetCatListByOwnerGender());
            _mockService.Verify();

            Assert.Equal("No data found", ex.Message);
        }
    }
}