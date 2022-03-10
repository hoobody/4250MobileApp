using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        // Empty Constructor for UTs
        bool UnitTestSetting;
        public BattlePage(bool UnitTest) { UnitTestSetting = UnitTest; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            // Set initial State to Starting
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            // Set up the UI to Defaults
            BindingContext = BattleEngineViewModel.Instance;

            // Start the Battle Engine
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);


            // Ask the Game engine to select who goes first
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            // Add Players to Display
            DrawGameAttackerDefenderBoard();

            // Set the Battle Mode
            ShowBattleMode();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ShowBattleMode();
        }

        #region BasicBattleMode

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI
            DrawGameBoardClear();

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Show the Attacker and Defender
            DrawGameBoardAttackerDefenderSection();
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender
        /// </summary>
        public void DrawGameBoardAttackerDefenderSection()
        {
            BattlePlayerBoxVersus.Text = "";

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker == null)
            {
                return;
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender == null)
            {
                return;
            }

            AttackerImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.ImageURI;
            AttackerName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            AttackerHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetCurrentHealthTotal.ToString() +
                " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetMaxHealthTotal.ToString();
            // Show what action the Attacker used
            AttackerAttack.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.PreviousAction.ToImageURI();

            var item = ItemIndexViewModel.Instance.GetItem(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PrimaryHand);
            if (item != null)
            {
                AttackerAttack.Source = item.ImageURI;
            }

            DefenderImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.ImageURI;
            DefenderName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Name;
            DefenderHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetMaxHealthTotal.ToString();

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Alive == false)
            {
                DefenderImage.BackgroundColor = Color.Red;
            }

            BattlePlayerBoxVersus.Text = "vs";
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender areas to be null
        /// </summary>
        public void DrawGameBoardClear()
        {
            AttackerImage.Source = string.Empty;
            AttackerName.Text = string.Empty;
            AttackerHealth.Text = string.Empty;

            DefenderImage.Source = string.Empty;
            DefenderName.Text = string.Empty;
            DefenderHealth.Text = string.Empty;
            DefenderImage.BackgroundColor = Color.Transparent;

            BattlePlayerBoxVersus.Text = string.Empty;
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            NextAttackExample(null);
        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Setttings_Clicked(object sender, EventArgs e)
        {
            await ShowBattleSettingsPage();
        }

        /// <summary>
        /// Next Attack Example
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// </summary>
        public void NextAttackExample(PlayerInfoModel data)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            SetAttackerAndDefender(data);


            var attacker = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            var defender = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender;

            if (attacker.PlayerType == PlayerTypeEnum.Monster && defender != null)
            {
                GameMessageWithInput( attacker.Name + " is attacking " + defender.Name );
                _ = Task.Delay(WaitTime);
                Debug.WriteLine("Waited one second");
            }

            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                Wait(1000);
            }

            Debug.WriteLine("Waited one second");

            if (RoundCondition == RoundEnum.NewRound || BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Count < 1)
            {
                // Uncomment this to allow the BattlePage to draw a new round screen between rounds
                //BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver || BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Count < 1)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return;
            }

            PrepareRound();
        }

        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender(PlayerInfoModel data)
        {
            //_ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());

            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // User would select who to attack

                    // for now just auto selecting
                    if (data == null)
                    {
                        _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    }

                    else
                    {
                        _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(data);
                    }

                    break;

                case PlayerTypeEnum.Monster:
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;
            }
        }

        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            ShowBattleMode();
        }
        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage, BattleMessages.Text);

            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
            }

            //htmlSource.Html = BattleEngineViewModel.Instance.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
            //HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        /// Method to write directly to BattleMessages.Text
        /// </summary>
        /// <param name="message"></param>
        public void GameMessageWithInput(string message)
        {
            // Output The Message that happened.
            BattleMessages.Text = message;

            Debug.WriteLine(BattleMessages.Text);
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.GetHTMLBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            PrepareRound();
            ShowBattleMode();
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }
        #endregion PageHandelers

        #region DisplayControlMethods

        /// <summary>
        /// Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {

            int i = 0, j = 0; // Counters
            int c = 2, r = 0; // Grid Locations

            var CharacterGridList = CharacterGrid.Children.ToList();
            foreach (var data in CharacterGridList)
            {
                _ = CharacterGrid.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                Frame PlayerFrame = new Frame()
                {
                    HeightRequest = 125              
                };
                PlayerFrame.Content = CharacterGridDisplay(data);
                CharacterGrid.Children.Add(PlayerFrame, c, r);
                i++;

                switch (c)
                {
                    case 2:
                        c = 3;
                        break;
                    case 3:
                        c = 1;
                        r = 1;
                        break;
                    case 1:
                        c = 4;
                        break;
                    case 4:
                        c = 0;
                        r = 2;
                        break;
                    case 0:
                        c = 5;
                        break;
                    default:
                        break;
                }

                if (i == 3)
                {
                    j++;
                }
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                _ = MonsterBox.Children.Remove(data);
            }

            // Reset counters
            i = 0;
            j = 0;

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data), i % 3, j);
                i++;

                if (i == 3)
                {
                    j++;
                }
            }


            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            CharacterGrid.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Character into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CharacterGridDisplay(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = "",
                    MaxHealth = 0,
                    CurrentHealth = 0,
                };
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleMediumStyle"],
                Source = data.ImageURI
            };


            var HP = new Label()
            {
                Text = data.CurrentHealth.ToString() + "/" + data.GetMaxHealthTotal,
                Style = (Style)Application.Current.Resources["TinyTitleStyle"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 0,
                LineHeight = 1,
                MaxLines = 1,
            };

            if (data.MaxHealth == 0)
            {
                HP.Text = "";
            }

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    HP
                },
            };

            HighlightAttackerDefender(data, PlayerStack);

            return PlayerStack;
        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            var ClickableButton = true;

            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = "",
                    Name = "",
                    MaxHealth = 0,
                    CurrentHealth = 0,
                };
                ClickableButton = false;
            }

            // Hookup the image
            var PlayerImage = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageButtonBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Sets up each image for selective targeting of characters
            if (ClickableButton)
            {
                PlayerImage.Clicked += (sender, args) => NextAttackExample(data);
            }

            // Changes the first image button that is null to target the first monster instead
            else
            {
                ClickableButton = true;
                PlayerImage.Clicked += (sender, args) => NextAttackExample(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList[0]);
            }

            var PlayerNameLabel = new Label()
            {
                Text = data.Name,
                Style = (Style)Application.Current.Resources["TinyTitleStyle"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 0,
                LineHeight = 1,
                MaxLines = 5,
            };

            var HP = new Label()
            {
                Text = data.CurrentHealth.ToString() + "/" + data.GetMaxHealthTotal,
                Style = (Style)Application.Current.Resources["TinyTitleStyle"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 0,
                LineHeight = 1,
                MaxLines = 1,
            };

            if (data.MaxHealth == 0)
            {
                HP.Text = "";
            }

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerNameLabel,
                    HP
                },
            };

            HighlightAttackerDefender(data, PlayerStack);

            return PlayerStack;
        }


        /// <summary>
        /// Checks if battle has started and then highlights the attacker and defender
        /// </summary>
        /// <param name="data"></param>
        /// <param name="playerStack"></param>
        public void HighlightAttackerDefender(PlayerInfoModel data, StackLayout playerStack)
        {
            if(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum != BattleStateEnum.Battling)
            {
                return;
            }
            
            if (data == BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker)
            {
                playerStack.BackgroundColor = (Color)Application.Current.Resources["TriciaryBackgroundColor"];
            }

            if (data == BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender)
            {
                playerStack.BackgroundColor = (Color)Application.Current.Resources["TriciaryTextColor"];
            }
        }

        /// <summary>
        /// Hide the differnt button states
        /// Hide the message display box
        /// </summary>
        public void HideUIElements()
        {
            //NextRoundButton.IsVisible = false;
            //AttackButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            BattlePlayerInfomationBox.IsVisible = false;
            Gem.IsVisible = false;
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());

            // If running in UT mode, 
            if (UnitTestSetting)
            {
                return;
            }

            HideUIElements();

            ClearMessages();

            DrawPlayerBoxes();

            // Update the Mode
            BattleModeValue.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum.ToMessage();

            ShowBattleModeDisplay();

            ShowBattleModeUIElements();
        }

        /// <summary>
        /// Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUIElements()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    //GameUIDisplay.IsVisible = false;
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();

                    GameUIDisplay.IsVisible = true;
                    BattlePlayerInfomationBox.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    NextRoundButton.IsVisible = true;
                    AttackButton.IsEnabled = false;
                    AbilityButton.IsEnabled = false;
                    CharacterGrid.IsVisible = true;
                    Gem.IsVisible = true;
                    BattlePlayerBox.IsVisible = false;
                    BattleBottomBox.IsVisible = false;
                    BattlePlayerInfomationBox.HeightRequest = 70;
                    break;

                case BattleStateEnum.NewRound:
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    NextRoundButton.IsVisible = true;
                    //AttackButton.IsVisible = false;
                    //AbilityButton.IsVisible = false;
                    MonsterBox.RowSpacing = -10;
                    CharacterGrid.IsVisible = false;
                    Gem.IsVisible = false;
                    break;

                case BattleStateEnum.GameOver:
                    // Hide the Game Board
                    GameUIDisplay.IsVisible = false;
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    TotalScore.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ExperienceGainedTotal.ToString();
                    // Show the Game Over Display
                    GameOverDisplay.IsVisible = true;
                    break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    GameUIDisplay.IsVisible = true;
                    BattlePlayerInfomationBox.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    NextRoundButton.IsVisible = false;
                    AttackButton.IsEnabled = true;
                    AbilityButton.IsEnabled = true;
                    CharacterGrid.IsVisible = true;
                    BattlePlayerBox.IsVisible = true;
                    BattleBottomBox.IsVisible = true;
                    Gem.IsVisible = true;
                    BattlePlayerInfomationBox.HeightRequest = 150;
                    break;

                // Based on the State disable buttons
                case BattleStateEnum.Unknown:
                default:
                    break;
            }
        }

        /// <summary>
        /// Control the Map Mode or Simple
        /// </summary>
        public void ShowBattleModeDisplay()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
            {
                case BattleModeEnum.MapAbility:
                case BattleModeEnum.MapFull:
                case BattleModeEnum.MapNext:
                    GamePlayersTopDisplay.IsVisible = false;
                    break;

                case BattleModeEnum.SimpleAbility:
                case BattleModeEnum.SimpleNext:
                case BattleModeEnum.Unknown:
                default:
                    GamePlayersTopDisplay.IsVisible = true;
                    break;
            }
        }

        #endregion

        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            ContinueButton.IsVisible = false;
            AttackButton.IsEnabled = true;
            AbilityButton.IsEnabled = true;
        }

        /// <summary>
        /// Waits a set number of time given in milliseconds
        /// </summary>
        /// <param name="milliseconds"></param>
        public void Wait(int milliseconds)
        {
            var timer = new Timer();
            bool timerDone = false;

            if (milliseconds == 0)
            {
                return;
            }
            if (milliseconds < 0)
            {
                return;
            }

            //prep the timer
            timer.Interval = milliseconds;
            timer.Enabled = true;
            timer.AutoReset = false;
            //set up complete condition
            timer.Elapsed += (sender, args) => timerDone = true;
            timer.Start();

            //wait until the timer is done
            while (!timerDone)
            {

            };

        }


        /// <summary>
        /// 
        /// </summary>
        public void PrepareRound()
        {
            //set next attacker
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());
            var attacker = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            DrawPlayerBoxes();

            if (attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                NextAttackExample(attacker);
            }


        }
    }
}