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
            //ensures code doesn't execute of item isn't properly filled out
            if (CheckIfReadyToSubmit())
            {
                // If the image in the data box is empty, use the default one..
                if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
                {
                    ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
                }

                MessagingCenter.Send(this, "Update", ViewModel.Data);
                _ = await Navigation.PopModalAsync();
            }
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
            RangeValue.Text = string.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = string.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = string.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch changes in the name text box and changes the color if empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void Name_onTextChange(object sender, ValueChangedEventArgs e)
        {
            NameEntry.BackgroundColor = (Color)Application.Current.Resources["ViewBackgroundColor"];
            NameFrame.BorderColor = (Color)Application.Current.Resources["BorderColor"];

            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                NameEntry.BackgroundColor = (Color)Application.Current.Resources["SecondaryBackgroundColor"];
                NameFrame.BorderColor = (Color)Application.Current.Resources["Error"];
            }
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                NameEntry.BackgroundColor = (Color)Application.Current.Resources["SecondaryBackgroundColor"];
                NameFrame.BorderColor = (Color)Application.Current.Resources["Error"];
            }
        }

        /// <summary>
        /// Catch changes in description text and changes the color of the box if empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Description_onTextChange(object sender, ValueChangedEventArgs e)
        {
            DescriptionEntry.BackgroundColor = (Color)Application.Current.Resources["ViewBackgroundColor"];
            DescriptionFrame.BorderColor = (Color)Application.Current.Resources["BorderColor"];

            if (string.IsNullOrEmpty(DescriptionEntry.Text))
            {
                DescriptionEntry.BackgroundColor = (Color)Application.Current.Resources["SecondaryBackgroundColor"];
                DescriptionFrame.BorderColor = (Color)Application.Current.Resources["Error"];
            }
            if (string.IsNullOrWhiteSpace(DescriptionEntry.Text))
            {
                DescriptionEntry.BackgroundColor = (Color)Application.Current.Resources["SecondaryBackgroundColor"];
                DescriptionFrame.BorderColor = (Color)Application.Current.Resources["Error"];
            }
        }

        /// <summary>
        /// Prevents submission if name or description is emtpy
        /// </summary>
        /// <returns></returns>
        public bool CheckIfReadyToSubmit()
        {
            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(DescriptionEntry.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(DescriptionEntry.Text))
            {
                return false;
            }
            return true;
        }

    }
}