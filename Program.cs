namespace TextQuest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int selectedOption = 1;  // variable for determining the position in the menu
            while (true)
            {
                Console.Clear();
                // ----------------
                // | Menu:        | 
                // | 1. start     | start of the game: nickname and date, intro, game...
                // | 2. history   | information about previous results
                // | 3. info      | description of the game, rules, how to get points
                // | 4. settings  | delete game results, control keys, answers to riddles (password: "amnesia")
                // | 5. exit      | exit the program
                // ----------------

                Console.ForegroundColor = ConsoleColor.White;  // text color
                Console.WriteLine("\n Text Quest v5.0");  // immutable upper part of the menu
                Console.WriteLine("  ----------------");
                Console.WriteLine("  | Menu:        |");
                // highlight the selected menu item
                switch (selectedOption)
                {
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  1. start      <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  | 2. history   |");
                        Console.WriteLine("  | 3. info      |");
                        Console.WriteLine("  | 4. settings  |");
                        Console.WriteLine("  | 5. exit      |");
                        break;
                    case 2:
                        Console.WriteLine("  | 1. start     |");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  2. history    <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  | 3. info      |");
                        Console.WriteLine("  | 4. settings  |");
                        Console.WriteLine("  | 5. exit      |");
                        break;
                    case 3:
                        Console.WriteLine("  | 1. start     |");
                        Console.WriteLine("  | 2. history   |");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  3. info       <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  | 4. settings  |");
                        Console.WriteLine("  | 5. exit      |");
                        break;
                    case 4:
                        Console.WriteLine("  | 1. start     |");
                        Console.WriteLine("  | 2. history   |");
                        Console.WriteLine("  | 3. info      |");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  4. settings   <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  | 5. exit      |");
                        break;
                    case 5:
                        Console.WriteLine("  | 1. start     |");
                        Console.WriteLine("  | 2. history   |");
                        Console.WriteLine("  | 3. info      |");
                        Console.WriteLine("  | 4. settings  |");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  5. exit       <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                }

                Console.WriteLine("  ----------------");  // immutable lower part of the menu
                Console.WriteLine("  Ali Khudaimuratov");

                ConsoleKeyInfo keyInfo = Console.ReadKey();  // reading navigation keys
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 1) ? 5 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == 5) ? 1 : selectedOption + 1;
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("\n|Thank you, goodbye!");
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.Enter:
                        // Actions performed when selecting a menu item
                        switch (selectedOption)
                        {
                            case 1:  // start
                                Game play = new();
                                play.Start();
                                Console.Clear();
                                Console.WriteLine("\n -------------|End|-------------");
                                Console.ReadKey();
                                break;
                            case 2:  // history
                                Console.Clear();
                                string cap = "Date       | Nickname  | Score | Ending" +
                                    "\n----------------------------------------";  // for easy display of results
                                Game results = new();
                                if (File.Exists(@"GameResults.csv"))  // if the file exists, data from it will be displayed in the console as a results table
                                {
                                    Console.WriteLine(cap);
                                    results.HistoryLoader();
                                }
                                else Console.WriteLine(cap);  // otherwise, the user will only see column names
                                Console.ReadKey();
                                break;
                            case 3:  // info
                                Console.Clear();
                                InfoLoader();
                                Console.ReadKey();
                                break;
                            case 4:  // settings
                                Console.Clear();
                                Settings();
                                break;
                            case 5:  // exit
                                Console.Clear();
                                Console.WriteLine("\nThank you, goodbye!");
                                return;
                        }
                        break;
                }
            }
        }

        static void InfoLoader()  // text: game rules, project information
        {
            Console.WriteLine("\n Game Rules:" +
                "\n| We are playing a riddle game! There will be 10 in total. There is only one answer to each" +
                "\n| Answers are always written in Latin without diacritics");
            Console.WriteLine("-----------------------------------------------------" +
                "\n About the project:" +
                "\n| Text quest" +
                "\n| Student: Ali Khudaimuratov" +
                "\n| Programming - CZU FEM, Informatics");
        }

        static void Settings()  // delete game results, control keys, answers to riddles
        {
            int selectedOption = 0;  // variable for determining the position
            while (true)
            {
                //  --------------------------
                //  | Settings:              |
                //  | 1. control keys        |
                //  | 2. delete history      |
                //  | 3. answers to riddles  |
                //  | 4. return to menu      |
                //  --------------------------

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n  --------------------------");  // immutable upper part of the menu
                Console.WriteLine("  | Settings:              |");
                // highlight the selected menu item
                switch (selectedOption)
                {
                    case 0:
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  1. control keys         <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  | 2. delete history      |");
                        Console.WriteLine("  | 3. answers to riddles  |");
                        Console.WriteLine("  | 4. return to menu      |");
                        break;
                    case 1:
                        Console.WriteLine("  | 1. control keys        |");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  2. delete history       <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  | 3. answers to riddles  |");
                        Console.WriteLine("  | 4. return to menu      |");
                        break;
                    case 2:
                        Console.WriteLine("  | 1. control keys        |");
                        Console.WriteLine("  | 2. delete history      |");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  3. answers to riddles   <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("  | 4. return to menu      |");
                        break;
                    case 3:
                        Console.WriteLine("  | 1. control keys        |");
                        Console.WriteLine("  | 2. delete history      |");
                        Console.WriteLine("  | 3. answers to riddles  |");
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" >  4. return to menu       <");
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                }
                Console.WriteLine("  --------------------------");  // immutable lower part of the menu

                ConsoleKeyInfo keyInfo = Console.ReadKey();  // reading navigation keys
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? 3 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == 3) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        // Actions performed when selecting a menu item
                        switch (selectedOption)
                        {
                            case 0:  // control keys
                                Console.Clear();
                                Console.WriteLine("\n Control keys:" +
                                    "\n | menu: ↑ ↓ enter" +
                                    "\n | exit: ESC");
                                Console.ReadKey();
                                break;
                            case 1:  // delete history
                                Console.Clear();
                                File.Delete(@"GameResults.csv");
                                Console.WriteLine("\n - History has been deleted -");
                                Console.ReadKey();
                                break;
                            case 2:  // answers to riddles
                                Console.Clear();
                                Texts answers = new();
                                Console.Write("\n Enter password: ");
                                if (Console.ReadLine() == "amnesia")
                                {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        Console.WriteLine($" | {i + 1}. {answers.Answers[i]}");
                                    }
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("\n | Password is incorrect, access denied");
                                    Console.ReadKey();
                                }
                                break;
                            case 3:  // return to menu
                                Console.Clear();
                                return;
                        }
                        break;
                }
            }
        }

    }
}
