using System;

namespace CoffeeShopSimulator
{
    public class GameRecord
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public DateTime Date { get; set; }
        public string Ending { get; set; }
        public decimal Money { get; set; }

        // Game session statistics for the records table
        public GameRecord(int id, string nickname, string ending, DateTime date, decimal money)
        {
            Id = id;
            Nickname = nickname;
            Date = date;
            Ending = ending;
            Money = money;
        }
    }
}
