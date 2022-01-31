using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Monster Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {
        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Difficulty
            var difficulty = this.ViewModel.Data.Difficulty;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ViewModel.Data.Difficulty = difficulty;

            return true;
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        ///// <summary>
        ///// Randomize the Monster, keep the level the same
        ///// </summary>
        ///// <returns></returns>
        //public bool RandomizeMonster()
        //{
        //    // Randomize Name
        //    ViewModel.Data.Name = RandomPlayerHelper.GetMonsterName();
        //    ViewModel.Data.Description = RandomPlayerHelper.GetMonsterDescription();

        //    // Randomize the Attributes
        //    ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
        //    ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
        //    ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();

        //    ViewModel.Data.Difficulty = RandomPlayerHelper.GetMonsterDifficultyValue();

        //    ViewModel.Data.ImageURI = RandomPlayerHelper.GetMonsterImage();

        //    ViewModel.Data.UniqueItem = RandomPlayerHelper.GetMonsterUniqueItem();

        //    _ = UpdatePageBindingContext();

        //    return true;
        //}

        /// <summary>
        /// Returns true if all required fields are filled out
        /// </summary>
        /// <returns></returns>
        public bool CheckIfReadyToSubmit()
        {
            if (NameEntry.Text.Length == 0)
            {
                return false;
            }
            if (DescriptionEntry.Text.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Catch changes in the name text box and changes the color if empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Name_onTextChange(object sender, ValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                NameEntry.BackgroundColor = Color.FromHex("848884");
                NameFrame.BorderColor = Color.Red;
            }
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                NameEntry.BackgroundColor = Color.FromHex("848884");
                NameFrame.BorderColor = Color.Red;
            }
            if (!string.IsNullOrEmpty(NameEntry.Text))
            {
                NameEntry.BackgroundColor = Color.FromHex("36454F");
                NameFrame.BorderColor = Color.FromHex("#696969");
            }
            if (!string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                NameEntry.BackgroundColor = Color.FromHex("36454F");
                NameFrame.BorderColor = Color.FromHex("#696969");
            }
        }
    }
}