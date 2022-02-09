using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.GameRules;
using Game.Models;
using Game.ViewModels;

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

        // Copy of Monster for cancel
        public MonsterModel MonsterCopy;

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            MonsterCopy = new MonsterModel(data.Data);

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

            // To update the picker values
            DifficultyPicker.SelectedItem = this.ViewModel.Data.Difficulty.ToString();
            JobPicker.SelectedItem = this.ViewModel.Data.MonsterJob.ToString();

            AddItemsToDisplay();

            return true;
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            //check for empty fields. If any are empty don't do anything
            if (CheckIfReadyToSubmit())
            {
                // If the image in the data box is empty, use the default one..
                if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
                {
                    ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
                }


                MessagingCenter.Send(this, "Create", ViewModel.Data);

                _ = await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            // Return to original state
            ViewModel.Data.Update(MonsterCopy);

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

        /// <summary>
        /// Catch changes in the name text box and changes the color if empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Name_onTextChange(object sender, ValueChangedEventArgs e)
        {
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
            if (!string.IsNullOrEmpty(NameEntry.Text))
            {
                NameEntry.BackgroundColor = (Color)Application.Current.Resources["EntryColor"];
                NameFrame.BorderColor = (Color)Application.Current.Resources["BorderColor"];
            }
            if (!string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                NameEntry.BackgroundColor = (Color)Application.Current.Resources["EntryColor"];
                NameFrame.BorderColor = (Color)Application.Current.Resources["BorderColor"];
            }
        }

        /// <summary>
        /// Catch changes in description text and changes the color of the box if empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescriptionEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
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
            if (!string.IsNullOrEmpty(DescriptionEntry.Text))
            {
                DescriptionEntry.BackgroundColor = (Color)Application.Current.Resources["EntryColor"];
                DescriptionFrame.BorderColor = (Color)Application.Current.Resources["BorderColor"];
            }
            if (!string.IsNullOrWhiteSpace(DescriptionEntry.Text))
            {
                DescriptionEntry.BackgroundColor = (Color)Application.Current.Resources["EntryColor"];
                DescriptionFrame.BorderColor = (Color)Application.Current.Resources["BorderColor"];
            }
        }

        /// <summary>
        /// Set the attack slider to integer increments and update a corresponding label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttackSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var updatedValue = Math.Round(e.NewValue);
            AttackSlider.Value = updatedValue;
            AttackLabel.Text = updatedValue.ToString();
        }

        /// <summary>
        /// Set the attack slider to integer increments and update a corresponding label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefenseSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var updatedValue = Math.Round(e.NewValue);
            DefenseSlider.Value = updatedValue;
            DefenseLabel.Text = updatedValue.ToString();
        }

        /// <summary>
        /// Set the speed slider to integer increments and update a corresponding label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var updatedValue = Math.Round(e.NewValue);
            SpeedSlider.Value = updatedValue;
            SpeedLabel.Text = updatedValue.ToString();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            _ = ViewModel.Data.AddItem(PopupLocationEnum, data.Id);

            AddItemsToDisplay();

            ClosePopup();
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemLocationEnum location)
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Items for :";
            PopupLocationValue.Text = location.ToMessage();

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null, // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel>
            {
                NoneItem
            };

            // Add the rest of the items to the list
            itemList.AddRange(ItemIndexViewModel.Instance.GetLocationItems(location));

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            // Remember the location for this popup
            PopupLocationEnum = location;

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup()
        {
            PopupItemSelector.IsVisible = false;
        }

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemBox.Children.Remove(data);
            }
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the Plus
            var ImageSource = "icon_add.png";

            var data = ViewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                data = new ItemModel { Location = location, ImageURI = ImageSource };
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(location);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageLabelBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

    }
}