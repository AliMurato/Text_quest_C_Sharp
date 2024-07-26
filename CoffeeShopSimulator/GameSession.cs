using System;

namespace CoffeeShopSimulator
{
    public class GameSession
    {
        public int CurrentDay { get; private set; } = 0;  // Current day
        public decimal CurrentMoney { get; private set; } = 20m;  // Current money (starting the game with $20)
        public decimal TargetMoney { get; } = 100m;  // The goal amount to earn
        public decimal DailyTax { get; } = 5m;  // Daily taxes
        public int TotalDays { get; } = 10;  // Number of days (episodes)
        public int CupsOfCoffeeMade { get; private set; }  // Prepared servings of coffee
        public int CupsOfCoffeeSold { get; private set; }  // Sold servings of coffee
        public int VisitorsToday { get; private set; }  // Number of visitors today
        public bool GameOver { get; private set; } = false;  // To check if the game is over

        public decimal CostPricePerCup { get; } = 2m;  // Cost price per cup of coffee when preparing
        public decimal PricePerCup { get; } = 5m;  // Sale price per cup of coffee when selling

        public int TotalCupsSold { get; private set; } = 0; // Total number of cups of coffee sold
        public int TotalSpoiledCups { get; private set; } = 0; // Total number of spoiled cups of coffee

        public void StartNewDay(int cupsOfCoffee)  // Main gameplay, processing events of the day
        {
            decimal costOfCoffee = cupsOfCoffee * CostPricePerCup;
            if (CurrentMoney >= TargetMoney)
            {
                GameOver = true; // Game ended (good ending A)
            }
            else if (CurrentMoney < costOfCoffee)
            {
                // Insufficient funds to prepare coffee - player is bankrupt (bad ending C)
                GameOver = true;
                CurrentMoney = 0;
            }
            else
            {
                // Continue the game if there are enough funds to prepare coffee
                CurrentMoney -= costOfCoffee;
                CupsOfCoffeeMade = cupsOfCoffee;
                VisitorsToday = new Random().Next(2, 11);
                CupsOfCoffeeSold = Math.Min(VisitorsToday, CupsOfCoffeeMade);
                CurrentMoney += CupsOfCoffeeSold * PricePerCup;
                CurrentMoney -= DailyTax;

                // After selling coffee, update the surplus statistics (spoiled coffee)
                TotalCupsSold += CupsOfCoffeeSold;
                int spoiledCups = CupsOfCoffeeMade - CupsOfCoffeeSold; // Amount of unsold coffee
                TotalSpoiledCups += spoiledCups; // Add to the total amount of spoiled cups of coffee

                if (CurrentDay >= TotalDays || CurrentMoney <= 0)
                {
                    GameOver = true; // End the game after reaching the last day or when funds are exhausted
                }
                else
                {
                    CurrentDay++;
                }
            }
        }

        public string CheckEndGame()
        {
            // Check end game conditions and return the corresponding ending
            if (CurrentMoney >= TargetMoney)
            {
                return "ENDING A | Congratulations! You've reached your goal! A well-deserved bonus and perhaps a promotion await you!";  // ending A
            }
            else if (CurrentMoney >= 50)
            {
                return "ENDING B | You didn't reach the goal, but you're still in business. Maybe you can persuade the boss to give you more time.";  // ending B
            }
            else
            {
                return "ENDING C | Unfortunately, it didn't work out. At best, you'll lose your bonus, and at worst...";  // ending C
            }
        }

        public string GetEndingLetter()
        {
            // Logic to determine the ending letter (needed in the future for saving to the table)
            if (CurrentMoney >= TargetMoney)
            {
                return "A"; // "A" for a successful ending
            }
            else if (CurrentMoney >= 50)
            {
                return "B"; // "B" for a neutral ending
            }
            else
            {
                return "C"; // "C" for an unsuccessful ending
            }
        }

        public string GetAikoText()
        {
            // Determine the ending and return the corresponding text
            if (CurrentMoney >= TargetMoney)
            {
                return "Aiko: Congratulations! We've achieved amazing results, and now our coffeehouse is one of the best in town. " +
                    "Your efforts and decisions have led us to this success. " +
                    "I wholeheartedly thank you for your hard work and belief in our dream!";  // Aiko's words for ending A
            }
            else if (CurrentMoney >= 50)
            {
                return "Aiko: Not bad work! We didn't reach our main goal, but you did everything possible, and our coffeehouse continues to operate. " +
                    "New days and new opportunities for success await us ahead. " +
                    "Let's not rest on our laurels!";  // Aiko's words for ending B
            }
            else
            {
                return "Aiko: Unfortunately, we failed to reach the goal, and now we are facing serious difficulties. " +
                    "I know you put in a lot of effort, and I'm sorry that things turned out this way. But don't lose hope! " +
                    "Every experience is a step towards future victories. Thank you for being with us...";  // Aiko's words for ending C
            }
        }

        public string GetDailyResults()
        {
            // Return a string with the day's results
            return $"Episode {CurrentDay}\n" +
                   $" | There were {VisitorsToday} visitors in the coffeehouse today.\n" +
                   $" | You sold {CupsOfCoffeeSold} servings of coffee.\n" +
                   $" | Remaining funds: ${CurrentMoney}.\n";
        }
    }

}
