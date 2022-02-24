using System;
using System.ComponentModel;

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
            //BindingContext = BattleEngineViewModel.Instance;

            var DatabaseCharacterList = ViewModel.DatabaseCharacterList;

            // Clear the Database List and the Party List to start
            ViewModel.PartyCharacterList.Clear();

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
                addSelectedCharacter(data);
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

            //PartyCountLabel.Text = currentCount.ToString();
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
        public void addSelectedCharacter(CharacterModel data)
        {

            int location = ViewModel.PartyCharacterList.IndexOf(data);
            switch (location)
            {
                case 0:
                    SelectedCharacterZero.Source = data.HeadshotImageURI;
                    SelectedCharacterZero.IsEnabled = true;
                    SelectedCharacterZero.IsVisible = true;
                break;
                case 1:
                    SelectedCharacterOne.Source = data.HeadshotImageURI;
                    SelectedCharacterOne.IsEnabled = true;
                    SelectedCharacterOne.IsVisible = true;
                    break;
                case 2:
                    SelectedCharacterTwo.Source = data.HeadshotImageURI;
                    SelectedCharacterTwo.IsEnabled = true;
                    SelectedCharacterTwo.IsVisible = true;
                    break;
                case 3:
                    SelectedCharacterThree.Source = data.HeadshotImageURI;
                    SelectedCharacterThree.IsEnabled = true;
                    SelectedCharacterThree.IsVisible = true;
                    break;
                case 4:
                    SelectedCharacterFour.Source = data.HeadshotImageURI;
                    SelectedCharacterFour.IsEnabled = true;
                    SelectedCharacterFour.IsVisible = true;
                    break;
                case 5:
                    SelectedCharacterFive.Source = data.HeadshotImageURI;
                    SelectedCharacterFive.IsEnabled = true;
                    SelectedCharacterFive.IsVisible = true;
                    break;
                default:
                    break;
            }
        }
        private void SelectedCharacterZero_Clicked(object sender, EventArgs e)
        {

            CharacterModel data = ViewModel.PartyCharacterList.ElementAt(0);

            CharacterDetailsPotrait.Source = data.HeadshotImageURI;
            PopupNameLabel.Text = data.Name;
            PopupDescriptionLabel.Text = data.Description;

            PopupHealthSlider.Value = data.MaxHealth;
            PopupHealthLabel.Text = data.MaxHealth.ToString();

            PopupAttackSlider.Value = data.GetAttackTotal;
            PopupAttackLabel.Text = data.GetAttackTotal.ToString();

            PopupDefenseSlider.Value = data.GetDefenseTotal;
            PopupDefenseLabel.Text = data.GetDefenseTotal.ToString();

            PopupSpeedSlider.Value = data.GetSpeedTotal;
            PopupSpeedLabel.Text = data.GetSpeedTotal.ToString();

            PopupCharacterDetails.IsVisible = true;

        }


    }
}