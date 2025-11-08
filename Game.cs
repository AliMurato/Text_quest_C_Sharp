using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Game  // contains the main game process
    {
        // fields necessary for game functions
        public DateTime Date = DateTime.Now;  // creates a new "Date" variable and assigns it the current date and time obtained from DateTime.Now.
        private string Nickname = string.Empty;
        public string Score = string.Empty;
        private int counter = 0;  // number of correct answers + orientation in the array of riddles
        private string savedString = string.Empty;  // to avoid Console.Clear()
        public string Ending = string.Empty; // end of the game
        private int attempts = 4;
        private int speed { get; set; }  // text print speed

        public void Start()  // start of the game
        {
            Console.Clear();
            Console.Write("Set text print speed: ");
            speed = SpeedOfText();
            StartIntro();  // game intro

            // Player enters nickname
            Console.Write("\n| My name is: ");

            this.Nickname = Console.ReadLine();
            Console.Clear();
            // if the player does not enter anything, a default name will be used
            if (string.IsNullOrWhiteSpace(Nickname))
            {
                this.Nickname = "Daniel";
                Console.WriteLine("\n| The name washed away due to water droplets!" +
                    "\n| Hmm, the letter said that the wall might know my name." +
                    "\n| I didn't have time to turn to the wall when the inscription appeared on it");
                Writer("\n\n| 'Good day, Daniel...'", 0);
            }
            else Console.WriteLine();

            Writer("\n| The time is also displayed on the wall: " +
                $"\n| {Date}", speed);
            StoryTeller();  // gameplay
        }

        private void StoryTeller()  // main "gameplay"
        {
            Texts story = new();

            Writer("\n| I decided to follow the advice from the note and turned to the wall (although it looked strange)" +
                "\n| 'Explain the rules of the game'", speed);
            Writer("\n| We're playing a riddle game! There will be 10 questions in total." +
                "\n| A missing answer can also be an answer. " +
                "\n| There is only one answer to each question." +
                "\n| Are you ready to play?", speed);

            Console.WriteLine("\n| 'Yes, let's start!'");
            KeyPresser();  // performs the function of the Console.ReadKey() method but allows the program to be terminated by pressing "ESC"

            Writer("\n| The game begins! Remember, there is only one answer.", speed);

            while (true)
            {
                foreach (char c in story.Riddles[counter])  // gradually displays 10 riddles
                {
                    Console.Write(c);
                    Thread.Sleep(0);
                }
                Console.Write("\n   | Answer: ");
                // convert uppercase letters to lowercase so TruthSeeker counts the answer if the user types in uppercase
                string answer = Console.ReadLine().ToLower();

                if (TruthSeeker(answer) == true)
                {
                    Console.Clear();
                    counter++;
                    // the line is saved to a variable, we will still need it for the YesOrNot and Final methods to avoid Console.Clear()
                    savedString = $"\n| Correct answer, you get a point: {counter}/10." +
                        $" Do you want to continue? 'Buttons appear under the inscription ...'";
                    if (counter == 10)  // if the player correctly solves all the riddles, the Final() method will start, probably with a good ending
                    {
                        Console.WriteLine($"| Correct answer, you get a point: {counter}/ 10.");
                        KeyPresser();
                        Final();
                        return;
                    }
                    if (YesOrNot() == false)  // the player may not want to continue - leads to a bad ending
                    {
                        Final();
                        return;
                    }
                    Console.Clear();
                }
                else  // incorrect answer to the riddle
                {
                    if (string.IsNullOrWhiteSpace(answer)) Console.WriteLine("\n   | A missing answer is also an answer, but in this case it is a wrong answer.");
                    Hangman(attempts);  // hangman (4 allowable mistakes)
                    KeyPresser();
                    Console.Clear();

                    if (attempts == 0)
                    {
                        Console.WriteLine("\n| Unfortunately, you lost");
                        KeyPresser();
                        Final();
                        break;
                    }
                    attempts--;
                }
            }
        }
        private bool YesOrNot()  // displays a small menu consisting of two items (Yes/No)
        {
            int selectedOption = 0;  // variable for determining the position
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;  // text color
                                                               // highlight the selected menu item
                switch (selectedOption)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" " + savedString);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" > Yes");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("   No");
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine(" " + savedString);
                        Console.WriteLine("   Yes");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" > No");
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();  // reading navigation keys
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("\n\n - Ending the program (ESC)\n");
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.Enter:
                        // Actions performed when selecting a menu item
                        switch (selectedOption)
                        {
                            case 0:  // Yes
                                return true;
                            case 1:  // No
                                return false;
                        }
                        break;
                }
            }
        }

        private void Final()  // determines the end of the game based on the game's progress and calls HistorySaver() to save the results
        {
            Score = counter.ToString();
            string badEnding = $"\n| Player {Nickname} remains in the room forever ... ";

            if (counter == 10)
            {
                Console.Clear();
                savedString = "" +
                    "\n| 'Clanking metal' \n| 'The door opens, behind it is a dark unknown.'" +
                    $"\n| {Nickname} looks around, shifting gaze" +
                    "\n| from room to room and then to the open door." +
                    "\n\n 'An inscription appears on the wall' - 'If you want, you can stay here.' - 'Buttons appear under the inscription ...'";
                Console.WriteLine(savedString);
                Console.ReadKey();

                if (YesOrNot() == false)
                {
                    Console.WriteLine($"\n| {Nickname} passed the test and left the room");
                    Console.ReadKey();
                    Ending = "good";
                    HistorySaver(Nickname, Score, Ending);
                    return;
                }
                else
                {
                    Console.WriteLine(badEnding + $"\n| Your score: {Score}");
                    Console.ReadKey();
                    Ending = "bad";
                    HistorySaver(Nickname, Score, Ending);
                    return;
                }
            }
            else if (counter != 10)
            {
                Console.WriteLine(badEnding + $"\n| Your score: {Score}");
                Console.ReadKey();
                Ending = "bad";
                HistorySaver(Nickname, Score, Ending);
                return;
            }
            return;
        }

        private bool TruthSeeker(string answer)  // compares the user's answers with the correct ones
        {
            Texts a = new();
            string[] array = a.Answers;

            if (answer == array[counter]) return true;
            else if (counter == 5 && string.IsNullOrWhiteSpace(answer)) return true;
            else return false;
        }
        public void HistorySaver(string nickname, string score, string ending)  // save data to table (date, nickname, score, ending)
        {
            string path = @"GameResults.csv";  // define the path to the file where game results will be stored
            string results = $"{Date.ToShortDateString()} |  {nickname}   |   {score}   | {ending}\n" +
                $"----------------------------------------\n";  // create a string with the game results

            // open the file for writing and use CsvWriter to write the results in CSV format
            // the using construct is used to automatically close the stream after work is completed
            using (var stream = new FileStream(path, FileMode.Append))  // FileMode.Append is used to add data to the end of the file
            using (var writer = new StreamWriter(stream))  // create a new instance of StreamWriter to write text data to the file stream
            {
                writer.Write(results);  // open the file for writing and use CsvWriter to write the results in CSV format
            }
        }

        public void HistoryLoader()  // table: information about previous results
        {
            // Read data from the file
            using (var reader = new StreamReader(@"GameResults.csv"))  // open the file "GameResults.csv" for reading using StreamReader
            {
                string Results = reader.ReadToEnd();  // read the contents of the file and save it to the Results variable
                Console.WriteLine(Results);  // display the contents of Results in the console

                // Display data in the console as a table
                // Date       | Nickname  | Score | Ending
                // Column names are displayed in the main program when selecting the "history" option in the menu (variable - cap)
            }
        }

        private void Hangman(int attempts)  // hangman (right to make a mistake)
        {
            string[] hangman = new string[5]
            {
        "    ___________" +
        "\n   |/        |" +
        "\n   |        (_)" +
        "\n   |        _|_" +
        "\n   |       / | \\" +
        "\n   |         | " +
        "\n   |        / \\" +
        "\n   |       /   \\" +
        "\n  _|___________" +
        "\n  |           |" +
        "\n  * * * RIP * * *",
        "    ___________" +
        "\n   |/        |" +
        "\n   |        (_)" +
        "\n   |        _|_" +
        "\n   |       / | \\" +
        "\n   |         | " +
        "\n   |" +
        "\n   |" +
        "\n  _|___________" +
        "\n  |           |" +
        "\n  * * * 4/5 * * *",
        "    ___________" +
        "\n   |/        |" +
        "\n   |        (_)" +
        "\n   |         | " +
        "\n   |         | " +
        "\n   |         | " +
        "\n   |" +
        "\n   |" +
        "\n  _|___________" +
        "\n  |           |" +
        "\n  * * * 3/5 * * *",
        "    ___________" +
        "\n   |/        |" +
        "\n   |        (_)" +
        "\n   |" +
        "\n   |" +
        "\n   |" +
        "\n   |" +
        "\n   |" +
        "\n  _|___________" +
        "\n  |           |" +
        "\n  * * * 2/5 * * *",
        "    ___________" +
        "\n   |/        |" +
        "\n   |" +
        "\n   |" +
        "\n   |" +
        "\n   |" +
        "\n   |" +
        "\n   |" +
        "\n  _|___________" +
        "\n  |           |" +
        "\n  * * * 1/5 * * *"
            };

            Console.WriteLine(hangman[attempts]);
        }

        static void Writer(string words, int speed)  // text output with a printing effect, with the ability to set custom speed
        {
            foreach (char c in words)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            KeyPresser();
            Console.Clear();
        }
        private void StartIntro()  // intro text output
        {
            Console.Clear();
            Texts i = new Texts();
            Writer(i.intro0, speed);
            Writer(i.intro1, speed);
            Writer(i.intro2, speed);
        }
        static int SpeedOfText()  // called at the beginning, allows setting text print speed (turn off printing at 0)
        {
            Console.Clear();
            int speed;

            while (true)  // infinite loop for repeatedly entering speed
            {
                Console.Write("\n| Text speed (15 <= number <= 50): ");

                string number = Console.ReadLine();
                if (int.TryParse(number, out speed))  // if successful in loading the number and converting it to an integer
                {
                    if (speed < 15) speed = 15;
                    else if (speed > 50) speed = 50;
                    return 50 - speed;
                }
                Console.WriteLine("\n| Invalid input, try again.");  // if the user entered invalid input, this message will be displayed
                KeyPresser();
                Console.Clear();
            }
        }
        static void KeyPresser()  // performs the function of the Console.ReadKey() method but allows the program to be terminated by pressing "ESC"
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\n\n - Ending the program (ESC)\n");
                Environment.Exit(0);
            }
        }
    }
}
