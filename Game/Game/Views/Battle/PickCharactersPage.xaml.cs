using System;
using System.ComponentModel;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// Selecting Characters for the Game
    /// 
    /// TODO: Team
    /// Mike's game allows duplicate characters in a party (6 Mikes can all fight)
    /// If you do not allow duplicates, change the code below
    /// Instead of using the database list directly make a copy of it in the viewmodel
    /// Then have on select of the database remove the character from that list and add to the part list
    /// Have select from the party list remove it from the party list and add it to the database list
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {

        // The view model, used for data binding
        BattleEngineViewModel ViewModel = BattleEngineViewModel.Instance;
        //helps determine which party member to remove when the remove button is clicked
        public int CharacterDetailsDisplayIndex;
        //Holds index image button pairs
        public Dictionary<int, ImageButton> MapIndexButton;
        //Controls max party size
        public const int MAX_PARTY_SIZE = 6;

        // Empty Constructor for UTs
        public PickCharactersPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public PickCharactersPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;

            var DatabaseCharacterList = ViewModel.DatabaseCharacterList;

            // Clear the Database List and the Party List to start
            ViewModel.PartyCharacterList.Clear();

            //prep button index info
            MapIndexButton = new Dictionary<int, ImageButton>();
            PopulateButtonIndexDictionary();
            CharacterDetailsDisplayIndex = -1;

            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnDatabaseCharacterItemSelected(object sender, SelectionChangedEventArgs args)
        {
            CharacterModel data = (args.CurrentSelection.FirstOrDefault() as CharacterModel);
            if (data == null)
            {
                return;
            }

            // Manually deselect Character.
            CharactersListView.SelectedItem = null;

            // Don't add more than the party max
            if (ViewModel.PartyCharacterList.Count() < ViewModel.Engine.EngineSettings.MaxNumberPartyCharacters)
            {
                ViewModel.PartyCharacterList.Add(data);
                DrawSelectedCharacters();
            }

            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPartyCharacterItemSelected(object sender, SelectionChangedEventArgs args)
        {
            CharacterModel data = (args.CurrentSelection.FirstOrDefault() as CharacterModel);
            if (data == null)
            {
                return;
            }

            // Manually deselect Character.
            //PartyListView.SelectedItem = null;

            // Remove the character from the list
            _ = ViewModel.PartyCharacterList.Remove(data);

            UpdateNextButtonState();
        }

        /// <summary>
        /// Next Button is based on the count
        /// 
        /// If no selected characters, disable
        /// 
        /// Show the Count of the party
        /// 
        /// </summary>
        public void UpdateNextButtonState()
        {
            // If no characters disable Next button
            BeginBattleButton.IsEnabled = true;

            var currentCount = ViewModel.PartyCharacterList.Count();
            if (currentCount == 0)
            {
                BeginBattleButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Jump to the Battle
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleButton_Clicked(object sender, EventArgs e)
        {
            CreateEngineCharacterList();

            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            await Navigation.PushModalAsync(new NavigationPage(new NewRoundPage()));

            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Clear out the old list and make the new list
        /// </summary>
        public void CreateEngineCharacterList()
        {
            // Clear the currett list
            ViewModel.Engine.EngineSettings.CharacterList.Clear();

            // Load the Characters into the Engine
            foreach (var data in ViewModel.PartyCharacterList)
            {
                data.CurrentHealth = data.GetMaxHealthTotal;
                ViewModel.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            }
        }

        /// <summary>
        /// Takes in a character model and updates the grid with the correct headshot image.
        /// </summary>
        /// <param name="data"></param>
        public void DrawSelectedCharacters()
        {

            for (int charIndex = 0; charIndex < MAX_PARTY_SIZE; charIndex++)
            {
                ImageButton characterSelectedButton = MapIndexButton[charIndex];

                if (charIndex >= ViewModel.PartyCharacterList.Count)
                {
                    characterSelectedButton.Source = null;
                    characterSelectedButton.IsEnabled = false;
                    characterSelectedButton.IsVisible = false;
                }

                if (charIndex < ViewModel.PartyCharacterList.Count)
                {
                    CharacterModel character = ViewModel.PartyCharacterList.ElementAt(charIndex);
                    characterSelectedButton.Source = character.HeadshotImageURI;
                    characterSelectedButton.IsEnabled = true;
                    characterSelectedButton.IsVisible = true;
                }
            }
        }

        /// <summary>
        /// Populates the button index dictionary
        /// </summary>
        public void PopulateButtonIndexDictionary()
        {
            MapIndexButton.Add(0, SelectedCharacterZero);
            MapIndexButton.Add(1, SelectedCharacterOne);
            MapIndexButton.Add(2, SelectedCharacterTwo);
            MapIndexButton.Add(3, SelectedCharacterThree);
            MapIndexButton.Add(4, SelectedCharacterFour);
            MapIndexButton.Add(5, SelectedCharacterFive);
        }

        /// <summary>
        /// Opens the popup for the selected character and fills it with that characters details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedCharacter_Clicked(object sender, EventArgs e)
        {
            //set the display index
            CharacterDetailsDisplayIndex = MapIndexButton.First(x => x.Value == (ImageButton)sender).Key;
            //grab the character being looked at
            CharacterModel character = ViewModel.PartyCharacterList.ElementAt(CharacterDetailsDisplayIndex);

            //Set all the details in the popup
            CharacterDetailsPotrait.Source = character.HeadshotImageURI;
            PopupNameLabel.Text = character.Name;
            PopupDescriptionLabel.Text = character.Description;

            //Health details
            PopupHealthSlider.Value = character.MaxHealth;
            PopupHealthLabel.Text = character.MaxHealth.ToString();

            //Attack details
            PopupAttackSlider.Value = character.Attack;
            PopupAttackLabel.Text = character.Attack.ToString();

            //Defense details
            PopupDefenseSlider.Value = character.Defense;
            PopupDefenseLabel.Text = character.Defense.ToString();

            //Speed details
            PopupSpeedSlider.Value = character.Speed;
            PopupSpeedLabel.Text = character.Speed.ToString();

            //Make the popup visible
            PopupCharacterDetails.IsVisible = true;
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
            PopupCharacterDetails.IsVisible = false;
            CharacterDetailsDisplayIndex = -1;
        }

        /// <summary>
        /// When the user clicks the trash can in the popup, remove the character from the list and then click the close popup button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemovePartyMember_Clicked(object sender, EventArgs e)
        {
            ViewModel.PartyCharacterList.RemoveAt(CharacterDetailsDisplayIndex);
            ClosePopup_Clicked(sender, e);
            DrawSelectedCharacters();
        }
    }
}