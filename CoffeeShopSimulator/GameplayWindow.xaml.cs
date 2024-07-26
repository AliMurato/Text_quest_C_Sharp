using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CoffeeShopSimulator
{
    public partial class GameplayWindow : Window
    {
        private List<string> storyParts; // Parts of the text to print
        private int currentPartIndex = 0; // Index of the current part
        private string currentText; // Current text to print
        private int textIndex = 0; // Index of the current character in the text to print
        private bool instantPrint = false; // Flag for instant text print (when Enter is pressed)

        private DispatcherTimer textTimer;
        private int textSpeed = 30; // Speed in milliseconds

        private string userNickname; // Here the nickname will be stored

        private GameSession gameSession;

        private List<string> sessionLogs = new List<string>(); // Storage for episode texts (session logs)

        public GameplayWindow(string nickname)
        {
            InitializeComponent();
            gameSession = new GameSession();
            userNickname = nickname;

            storyParts = new List<string>  // Introductory text
        {
        $"Aiko: Welcome to the \"Dream Cafe\", {userNickname}-san! My name is Aiko, and I'll be your assistant... Our goal is to earn the missing $100 in 10 days. " +
        "Each day you decide how much coffee to prepare, but remember: unused coffee spoils by the next day.",
        "Aiko: Visitors come randomly, averaging from 2 to 11 people, so it's necessary to balance to avoid losses. " +
        "Don't forget that we have to pay taxes of $2 daily, so planning is the key to success. " +
        $"It's time to show what we are capable of! Let's get started! How many servings of coffee would you like to prepare today, {userNickname}-san?"
        };

            InitializeTextTimer();  // Initialization and start of the text timer

            UpdateUI(); // Update the UI with initial values
        }

        private void InitializeTextTimer()
        {
            textTimer = new DispatcherTimer();
            textTimer.Interval = TimeSpan.FromMilliseconds(textSpeed);
            textTimer.Tick += TextTimer_Tick;
            StartPrintingPart(); // Start printing the first part
        }

        private void StartPrintingPart()
        {
            currentText = storyParts[currentPartIndex]; // Get the current part of the text
            textIndex = 0; // Reset character index
            StoryTextBlock.Text = ""; // Clear the text block
            textTimer.Start(); // Start the timer
        }

        private void TextTimer_Tick(object sender, EventArgs e)
        {
            if (instantPrint || textIndex >= currentText.Length)
            {
                // Print all remaining text
                StoryTextBlock.Text = currentText;
                textIndex = currentText.Length;
                textTimer.Stop(); // Stop the timer
                instantPrint = false; // Reset the flag for instant text print (Enter)
            }
            else if (textIndex < currentText.Length)
            {
                // Print the next character
                StoryTextBlock.Text += currentText[textIndex];
                textIndex++;
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e) // Handling key presses (Esc, Enter)
        {
            if (e.Key == Key.Escape)  // Option to exit to the game menu
            {
                ExitDialog exitDialog = new ExitDialog();
                bool? dialogResult = exitDialog.ShowDialog();

                if (dialogResult == true) // User chose "Exit to menu"
                {
                    this.Close(); // Close the current game window
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.Show();  // Make the main window visible
                }
                // Otherwise, if the user closed the dialog, stay in GameplayWindow
            }
            if (e.Key == Key.Enter)  // Confirming action: instant print of the current text / automatically press the OK button (cup of coffee) if it was previously pressed (thanks to focus on the button)
            {
                if (textTimer.IsEnabled && !instantPrint)
                {
                    // On the first Enter press, if printing is in progress, print all text at once
                    instantPrint = true;
                }
                else if (!textTimer.IsEnabled)
                {
                    // If the text is already printed, move to the next part of the text
                    if (currentPartIndex < storyParts.Count - 1)
                    {
                        currentPartIndex++;
                        StartPrintingPart();
                    }
                }
            }
        }

        // Handling the button press to display the session log
        private void ShowSessionLogs_Click(object sender, RoutedEventArgs e)
        {
            // Create a string to display the session log
            string logText = string.Join("", sessionLogs);

            // Display the session log, for example in a MessageBox or separate window
            MessageBox.Show(logText, "Session log (updated every episode)");
        }

        // Handling the display of a brief guide to managing the coffeehouse
        private void ShowGuide_Click(object sender, RoutedEventArgs e)
        {
            string guideText = "A brief guide to managing the coffeehouse:\n" +
                   "- Each day is a new episode in the life of the coffeehouse. You have a total of 10 days to reach your goal ($100).\n" +
                   "- In the morning, you decide how many servings of coffee to prepare. Each serving costs money, so be careful with your inventory!\n" +
                   "- During the day, a random number of customers visit us. Some days are busier, others quieter.\n" +
                   "- Important! Unused coffee spoils by the next day, so plan carefully.\n" +
                   "- Daily taxes and expenses will also be deducted from our budget.\n" +
                   "- At the end of the day, you'll see how we are progressing towards our goal. Depending on how well you manage, there can be various outcomes - from getting fired to getting a bonus!";

            MessageBox.Show(guideText, "Brief Guide"); // Display the text in a dialog box
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (textTimer.IsEnabled && !instantPrint)
            {
                // On the first Enter press, if printing is in progress, print all text at once
                instantPrint = true;
            }
            else if (!textTimer.IsEnabled)
            {
                // If the text is already printed, move to the next part of the text
                if (currentPartIndex < storyParts.Count - 1)
                {
                    currentPartIndex++;
                    StartPrintingPart();
                }
            }
        }

        private void UpdateUI()  // Update player's statistics above + useful information
        {
            // Update text with current balance, target, and remaining days
            CurrentMoneyTextBlock.Text = $"${gameSession.CurrentMoney}";
            TargetMoneyTextBlock.Text = $"${gameSession.TargetMoney}";
            RemainingDaysTextBlock.Text = $"{gameSession.TotalDays - gameSession.CurrentDay}";

            // Update coffee information
            CostPricePerCupText.Text = $"${gameSession.CostPricePerCup}";  // Cost price per cup of coffee
            PricePerCupText.Text = $"${gameSession.PricePerCup}";  // Sale price per cup of coffee
            TotalCupsSoldText.Text = $"{gameSession.TotalCupsSold}";  // Total number of cups of coffee sold
            TotalSpoiledCupsText.Text = $"{gameSession.TotalSpoiledCups}";  // Total number of spoiled cups of coffee
        }


        private void OnConfirmCoffeeAmount(object sender, RoutedEventArgs e) // The player enters the amount of coffee to be prepared
        {
            if (int.TryParse(CupsOfCoffeeInput.Text, out int cupsOfCoffee) && cupsOfCoffee >= 1)  // Check for valid coffee amount
            {
                decimal costOfCoffee = cupsOfCoffee * gameSession.CostPricePerCup; // Use coffee price from GameSession
                if (gameSession.CurrentMoney >= costOfCoffee)
                {
                    // Stop any current printing process, clear the text area
                    textTimer.Stop();
                    currentText = "";
                    textIndex = 0;

                    // Start a new game day and show the results
                    gameSession.StartNewDay(cupsOfCoffee);
                    UpdateUI();
                    sessionLogs.Add(gameSession.GetDailyResults());  // Add episode record to game session log
                    DisplayDailyResults(gameSession.GetDailyResults());

                    if (gameSession.GameOver)
                    {
                        if (gameSession.CurrentMoney <= 0) // Check for bankruptcy
                        {
                            MessageBox.Show("Not enough funds to prepare even one serving of coffee, you can't continue the game.", "Bankruptcy");
                            EndGame();
                        }
                        else
                        {
                            EndGame(); // End the game if the last day is reached
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Insufficient funds to prepare that amount of coffee.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of coffee servings.");
            }
        }


        private void DisplayDailyResults(string results)
        {
            if (gameSession.GameOver) // In case the game ends, the current text will be replaced by a farewell from Aiko
            {
                currentText = gameSession.GetAikoText();
                textIndex = 0;
                StoryTextBlock.Text = "";
                textTimer.Start();
                return;
            }

            // Set new text for printing (episode results)
            currentText = results + $"Aiko: How many servings of coffee would you like to prepare today, {userNickname}-san?";
            textIndex = 0;
            StoryTextBlock.Text = "";
            textTimer.Start();
        }

        private void EndGame()  // Handling the end of the game session
        {
            string endGameMessage = gameSession.CheckEndGame(); // Check end game conditions and return the corresponding ending.
            MessageBox.Show(endGameMessage, "Game over");

            // Prepare to record game statistics
            var gameDataService = new GameDataService();
            List<GameRecord> existingRecords = gameDataService.LoadRecords();
            int newId = existingRecords.Any() ? existingRecords.Max(r => r.Id) + 1 : 1;

            // Create a new game session record
            var gameRecord = new GameRecord(
                newId, // Unique record identifier
                userNickname,
                gameSession.GetEndingLetter(), // Ending letter
                DateTime.Now, // Current date
                gameSession.CurrentMoney // Amount of money at the time of game end
            );

            // Save the record
            gameDataService.SaveRecord(gameRecord);

            // Close the current window
            this.Close();

            // Display the results window
            ResultsWindow resultsWindow = new ResultsWindow(userNickname, DateTime.Now, gameSession.GetEndingLetter(), gameSession.CurrentMoney);
            resultsWindow.ShowDialog();
        }
    }
}
