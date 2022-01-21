using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using Game.GameRules;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        // Current count of the Images in the Item Image List
        public int ImageListCount = RandomPlayerHelper.ItemImageList.Count;

        //track if page is ready to submit the updates
        public bool readyToSubmit { get; set; } = false;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToString();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Shift Image to the Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftArrow_Clicked(object sender, EventArgs e)
        {
            _ = ChangeImageByIncrement(-1);
        }

        /// <summary>
        /// Shift Image to the Right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightArrow_Clicked(object sender, EventArgs e)
        {
            _ = ChangeImageByIncrement(1);
        }

        /// <summary>
        /// Move the Image left or Right
        /// </summary>
        /// <param name="increment"></param>
        public int ChangeImageByIncrement(int increment)
        {
            // Find the Index for the current Image
            var ImageIndexCurrent = RandomPlayerHelper.ItemImageList.IndexOf(ViewModel.Data.ImageURI);

            // Amount to move
            var indexNew = ImageIndexCurrent + increment;

            if (indexNew >= ImageListCount)
            {
                indexNew = ImageListCount - 1;
            }

            if (indexNew <= 0)
            {
                indexNew = 0;
            }

            // Increment or Decrement to change the to a different image
            ViewModel.Data.ImageURI = RandomPlayerHelper.ItemImageList.ElementAt(indexNew);

            _ = UpdatePageBindingContext();

            return indexNew;
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = ViewModel;

            _ = EnableImageArrowButtons();

            return true;
        }

        /// <summary>
        /// Enable True of False the Image Arrows
        /// Based on the image in the list
        /// </summary>
        /// <returns></returns>
        public bool EnableImageArrowButtons()
        {
            LeftArrowButton.IsEnabled = true;
            RightArrowButton.IsEnabled = true;

            var ImageIndexCurrent = RandomPlayerHelper.ItemImageList.IndexOf(ViewModel.Data.ImageURI);

            if (ImageIndexCurrent < 1)
            {
                LeftArrowButton.IsEnabled = false;
            }

            if (ImageIndexCurrent >= ImageListCount - 1)
            {
                RightArrowButton.IsEnabled = false;
            }

            return true;
        }

        /// <summary>
        /// Function to catch changes in the text box and prevent submission of an empty field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void Name_NameTextChanged(object sender, ValueChangedEventArgs e)
        {

        }

    }
}