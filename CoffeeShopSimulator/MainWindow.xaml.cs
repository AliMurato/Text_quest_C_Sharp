using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CoffeeShopSimulator
{
    public partial class MainWindow : Window
    {
        private GameplayWindow gameplayWindow;
        private string userNickname; // Variable to store the nickname
        public MainWindow()
        {
            InitializeComponent();
        }

        // Handling the "Start" button click (entering a nickname)
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide the menu and show the nickname entry form
            MenuStackPanel.Visibility = Visibility.Collapsed;
            NicknameEntryPanel.Visibility = Visibility.Visible;
            NicknameTextBox.Focus();
        }

        // Handling the "History" button click (records table)
        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide the main menu
            MenuStackPanel.Visibility = Visibility.Collapsed;

            // Load and display history
            var gameDataService = new GameDataService();
            List<GameRecord> records = gameDataService.LoadRecords();
            RecordsDataGrid.ItemsSource = records;
            HistoryPanel.Visibility = Visibility.Visible;
        }

        // Handling the "Instruction" button click (game UI overview)
        private void InstructionButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide the main menu and show the instruction panel
            MenuStackPanel.Visibility = Visibility.Collapsed;
            InstructionPanel.Visibility = Visibility.Visible;
        }

        // Handling the "Exit" button click (closing the application)
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Handling the text change in the nickname entry field
        private void NicknameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove all non-alphabetic characters from the nickname (create an array from the text, exclude invalid characters)
            NicknameTextBox.Text = new string(NicknameTextBox.Text.Where(char.IsLetter).ToArray());

            // Move the cursor to the end of the text
            NicknameTextBox.CaretIndex = NicknameTextBox.Text.Length;
        }

        // Handling the "Clear History" button click
        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            var gameDataService = new GameDataService();
            gameDataService.ClearHistory(); // Delete all records
            RecordsDataGrid.ItemsSource = null; // Refresh the table
        }

        // Handling the "Back to Menu" button click
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            // Determine which panel was active
            if (HistoryPanel.Visibility == Visibility.Visible)
            {
                HistoryPanel.Visibility = Visibility.Collapsed;
            }
            else if (InstructionPanel.Visibility == Visibility.Visible)
            {
                InstructionPanel.Visibility = Visibility.Collapsed;
            }

            // Show the main menu
            MenuStackPanel.Visibility = Visibility.Visible;
        }

        // Confirm nickname and transition to the gameplay window
        private void NicknameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrWhiteSpace(NicknameTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid nickname. It cannot be empty or whitespace.");
                    return; // Interrupt if the nickname is empty or consists of spaces
                }

                if (NicknameTextBox.Text.Length > 20)
                {
                    MessageBox.Show("Nickname must be 20 characters or less.");
                    return; // Interrupt if the nickname is too long
                }

                // Confirm the nickname and hide the current window (menu)
                userNickname = NicknameTextBox.Text;
                this.Hide();

                // Move the user from the main menu to the gameplay window, passing the nickname to the GameplayWindow constructor
                gameplayWindow = new GameplayWindow(userNickname);
                gameplayWindow.Show();

                // Make the menu buttons visible (needed when returning to the menu after the game ends)
                ShowMainMenu();
            }
        }

        private void ShowMainMenu()
        {
            // Show the stack panel with menu buttons
            MenuStackPanel.Visibility = Visibility.Visible;

            // Hide the nickname entry panel and prompt
            NicknameEntryPanel.Visibility = Visibility.Collapsed;
        }
    }
}

