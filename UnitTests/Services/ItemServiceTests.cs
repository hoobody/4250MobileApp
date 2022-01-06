using System.Threading.Tasks;
using System.Linq;

using NUnit.Framework;

using Game.Services;
using Game.Models;

using UnitTests.TestHelpers;

namespace UnitTests.Services
{
    [TestFixture]
    public class ItemServiceTests
    {
        #region TestSetup
        [SetUp]
        public void Setup()
        {
            _ = TestBaseHelper.SetHttpClientToMock();
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContent_Example_API_Pass);
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);
            ResponseMessage.SetHttpStatusCode(ResponseMessage.HttpStatusCodeSuccess);

            _ = Game.Helpers.DataSetsHelper.WarmUp();
        }

        [TearDown]
        public async Task TearDown()
        {
            _ = await Game.Helpers.DataSetsHelper.WipeDataInSequence();
            _ = TestBaseHelper.SetHttpClientToReal();
        }
        #endregion TestSetup

        [Test]
        public void ItemService_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemService.DefaultImageURI;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ItemService_GetItemsFromServerGetAsync_Valid_0_Should_Pass()
        {
            // Arrange
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContentItemGetDefault);

            // Act
            var result = await ItemService.GetItemsFromServerGetAsync(0);

            // Reset
            _ = TestBaseHelper.SetHttpClientToReal();

            // Assert
            Assert.AreEqual(true, result.Count > 1);
        }

        [Test]
        public async Task ItemService_GetItemsFromServerGetAsync_InValid_Null_Should_Pass()
        {
            // Arrange
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);

            // Act
            var result = await ItemService.GetItemsFromServerGetAsync(1);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task ItemService_GetItemsFromServerGetAsync_Valid_1_Should_Pass()
        {
            // Arrange
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContentItemGetDefault);

            // Act
            var result = await ItemService.GetItemsFromServerGetAsync(1);

            // Reset
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);

            // Assert
            Assert.AreEqual(true, result.Count == 2);
            Assert.AreEqual("Strong Shield", result[0].Name);
        }

        [Test]
        public async Task ItemService_GetItemsFromServerPostAsync_Valid_1_Should_Pass()
        {
            // Arrange

            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContentItemPostDefault);

            var number = 1;

            var level = 6;  // Max Value of 6
            var attribute = AttributeEnum.Unknown;  // Any Attribute
            var location = ItemLocationEnum.Unknown;    // Any Location
            var random = true;  // Random between 1 and Level
            var updateDataBase = true;  // Add them to the DB
            var category = 0;   // What category to filter down to, 0 is all

            // will return shoes value 10 of speed.

            // Act
            var result = await ItemService.GetItemsFromServerPostAsync(number, level, attribute, location, category, random, updateDataBase);

            // Reset
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);

            // Assert
            Assert.AreEqual(true, result.Count == 1);
        }

        [Test]
        public async Task ItemService_GetItemsFromServerPostAsync_Valid_3_Should_Pass()
        {
            // Arrange
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContentItemGet3);

            var number = 3;

            var level = 6;  // Max Value of 6
            var attribute = AttributeEnum.Unknown;  // Any Attribute
            var location = ItemLocationEnum.Unknown;    // Any Location
            var random = true;  // Random between 1 and Level
            var updateDataBase = true;  // Add them to the DB
            var category = 0;   // What category to filter down to, 0 is all

            // will return shoes value 10 of speed.

            // Act
            var result = await ItemService.GetItemsFromServerPostAsync(number, level, attribute, location, category, random, updateDataBase);

            // Reset
            _ = TestBaseHelper.SetHttpClientToReal();

            // Assert
            Assert.AreEqual(true, result.Count == 3);
        }

        [Test]
        public async Task ItemService_GetItemsFromServerPostAsync_InValid_Should_Fail()
        {
            // Arrange
            var number = -1;
            var level = -1;  // Max Value of 6
            var attribute = AttributeEnum.Unknown;  // Any Attribute
            var location = ItemLocationEnum.Unknown;    // Any Location
            var random = true;  // Random between 1 and Level
            var updateDataBase = true;  // Add them to the DB
            var category = 0;   // What category to filter down to, 0 is all

            // will return shoes value 10 of speed.

            // Act
            var result = await ItemService.GetItemsFromServerPostAsync(number, level, attribute, location, category, random, updateDataBase);

            // Reset

            // Assert
            Assert.AreEqual(true, result.Count == 0);
        }
    }
}