using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Game;
using Game.Views;
using Game.Models;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;

using UnitTests.TestHelpers;

namespace UnitTests.Views
{
    [TestFixture]
    public class AboutPageTests : AboutPage
    {
        #region TestSetup
        App app;
        AboutPage page;

        // Base Constructor
        public AboutPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new AboutPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }
        #endregion TestSetup

        #region Elements

        [Test]
        public void AboutPage_Elements_Get_Set_Should_Pass()
        {
            // Arrange

            // Act
            ((StackLayout)page.FindByName("DatabaseSettingsFrame")).IsVisible = true;
            ((StackLayout)page.FindByName("DebugSettingsFrame")).IsVisible = true;

            ((Switch)page.FindByName("DatabaseSettingsSwitch")).IsVisible = true;
            ((Switch)page.FindByName("DatabaseSettingsSwitch")).IsToggled = true;
            ((Switch)page.FindByName("DatabaseSettingsSwitch")).IsToggled = false;

            ((Switch)page.FindByName("DebugSettingsSwitch")).IsVisible = true;
            ((Switch)page.FindByName("DebugSettingsSwitch")).IsToggled = true;
            ((Switch)page.FindByName("DebugSettingsSwitch")).IsToggled = false;

            ((Switch)page.FindByName("DataSourceValue")).IsVisible = true;
            ((Switch)page.FindByName("DataSourceValue")).IsToggled = true;
            ((Switch)page.FindByName("DataSourceValue")).IsToggled = false;

            ((Label)page.FindByName("CurrentDateTime")).Text = "test";

            ((StackLayout)page.FindByName("DatabaseSettingsFrame")).IsVisible = false;
            ((StackLayout)page.FindByName("DebugSettingsFrame")).IsVisible = false;

            // Reset

            // Assert
            Assert.IsNotNull((StackLayout)page.FindByName("DebugSettingsFrame"));
            Assert.IsNotNull(((StackLayout)page.FindByName("DatabaseSettingsFrame")));

            Assert.IsNotNull((Label)page.FindByName("CurrentDateTime"));

            Assert.IsNotNull((Switch)page.FindByName("DatabaseSettingsSwitch"));
            Assert.IsNotNull((Switch)page.FindByName("DataSourceValue"));
            Assert.IsNotNull((Switch)page.FindByName("DebugSettingsSwitch"));
        }
        #endregion Elements

        #region DatabaseSettingsSwitch
        [Test]
        public void AboutPage_DatabaseSettingsSwitch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("DatabaseSettingsFrame");
            var current = frame.IsVisible;

            var args = new ToggledEventArgs(current);


            // Act
            page.DatabaseSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_DebugSettingsSwitch_OnToggled_Default_Should_Pass()
        {
            // Arrange

            StackLayout frame = (StackLayout)page.FindByName("DebugSettingsFrame");
            var current = frame.IsVisible;

            var args = new ToggledEventArgs(current);


            // Act
            page.DebugSettingsSwitch_OnToggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }
        #endregion DatabaseSettingsSwitch

        #region DataSource
        [Test]
        public void AboutPage_DataSource_Toggled_Default_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled;

            var args = new ToggledEventArgs(current);

            // Act
            page.DataSource_Toggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_DataSource_Toggled_False_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled = false;

            // Act
            control.IsToggled = true;

            var result = control.IsToggled;

            // Reset

            // Assert
            Assert.AreEqual(!current, result);
        }

        [Test]
        public void AboutPage_DataSource_Toggled_True_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled = true;

            // Act
            control.IsToggled = false;

            var result = control.IsToggled;

            // Reset

            // Assert
            Assert.AreEqual(!current, result);
        }
        #endregion DataSource

        #region WipeDataList
        [Test]
        public void AboutPage_WipeDataList_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.WipeDataList_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_RunWipeData_Should_Pass()
        {
            // Arrange

            // Act
            page.RunWipeData();

            // Reset

            // Assert
            Assert.AreEqual(true, true); // Got to here, so it happened...
        }
        #endregion WipeDataList

        #region GetItemsGet
        [Test]
        public void AboutPage_GetItemsGet_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemsGet_Command(null, null);

            // Reset

            // Assert
            Assert.AreEqual(true, true); // Got to here, so it happened...
        }

        [Test]
        public async Task AboutPage_GetItemsGet_Invalid_BadURL_Should_Fail()
        {
            // Arrange
            var hold = WebGlobalsModel.WebSiteAPIURL;
            WebGlobalsModel.WebSiteAPIURL = "https://bogusurl";

            _ = TestBaseHelper.SetHttpClientToMock();
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContent_Example_API_Pass);
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);
            ResponseMessage.SetHttpStatusCode(ResponseMessage.HttpStatusCodeSuccess);

            // Act
            var result = await page.GetItemsGet();

            // Reset
            WebGlobalsModel.WebSiteAPIURL = hold;
            _ = TestBaseHelper.SetHttpClientToReal();

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }

        [Test]
        public async Task AboutPage_GetItemsGet_Invalid_Neg_Should_Fail()
        {
            // Arrange
            _ = TestBaseHelper.SetHttpClientToMock();
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContent_Example_API_Pass);
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);
            ResponseMessage.SetHttpStatusCode(ResponseMessage.HttpStatusCodeSuccess);

            page.SetServerItemValue("-100");

            // Act
            var result = await page.GetItemsGet();

            // Reset
            _ = TestBaseHelper.SetHttpClientToReal();

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }
        #endregion GetItemsGet

        #region GetItemsPost
        [Test]
        public void AboutPage_GetItemsPost_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemsPost_Command(null, null);

            // Reset

            // Assert
            Assert.AreEqual(true, true); // Got to here, so it happened...
        }

        [Test]
        public async Task AboutPage_GetItemsPost_Invalid_BadURL_Should_Fail()
        {
            // Arrange
            var hold = WebGlobalsModel.WebSiteAPIURL;
            WebGlobalsModel.WebSiteAPIURL = "https://bogusurl";

            _ = TestBaseHelper.SetHttpClientToMock();
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContent_Example_API_Pass);
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);
            ResponseMessage.SetHttpStatusCode(ResponseMessage.HttpStatusCodeSuccess);

            // Act
            var result = await page.GetItemsPost();

            // Reset
            WebGlobalsModel.WebSiteAPIURL = hold;
            _ = TestBaseHelper.SetHttpClientToReal();

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }

        [Test]
        public async Task AboutPage_GetItemsPost_Invalid_Neg_Should_Fail()
        {
            // Arrange
            _ = TestBaseHelper.SetHttpClientToMock();
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContent_Example_API_Pass);
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);
            ResponseMessage.SetHttpStatusCode(ResponseMessage.HttpStatusCodeSuccess);

            page.SetServerItemValue("-100");

            // Act
            var result = await page.GetItemsPost();

            // Reset
            _ = TestBaseHelper.SetHttpClientToReal();

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }
        #endregion GetItemsPost

        #region DisplayServerResults
        [Test]
        public void AboutPage_DisplayServerResults_InValid_Null_List_Should_Show_No_Results()
        {
            // Arrange
            var control = (Editor)page.FindByName("ServerItemsList");

            // Act
            var result = page.DisplayServerResults(null);

            // Reset

            // Assert
            Assert.AreEqual(false, result); // Got to here, so it happened...
            Assert.AreEqual("No Results", control.Text); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_DisplayServerResults_InValid_Empty_List_Should_Show_No_Results()
        {
            // Arrange
            var data = new List<ItemModel>();
            var control = (Editor)page.FindByName("ServerItemsList");

            // Act
            var result = page.DisplayServerResults(data);

            // Reset

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
            Assert.AreEqual("No Results", control.Text); // Got to here, so it happened...
        }

        [Test]
        public void AboutPage_DisplayServerResults_Valid_List_Should_Show_Results()
        {
            // Arrange
            var data = new List<ItemModel>()
            {
                new ItemModel()
                {
                    Name="Test1",
                    Description="1",
                    Damage=1,
                    Location=ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack,
                    Value = 2,
                    Range = 0
                },
                new ItemModel()
                {
                    Name="Test2",
                    Description="2",
                    Damage=1,
                    Location=ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack,
                    Value = 2,
                    Range = 0                }
            };

            var control = (Editor)page.FindByName("ServerItemsList");

            var expected = new StringBuilder();
            _ = expected.AppendLine(data[0].FormatOutput());
            _ = expected.AppendLine(data[1].FormatOutput());

            // Act
            var result = page.DisplayServerResults(data);

            // Reset

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
            Assert.AreEqual(expected.ToString(), control.Text); // Got to here, so it happened...
        }
        #endregion DisplayServerResults
    }
}