using System;
using System.Collections.Generic;
using System.IO;

namespace CoffeeShopSimulator
{
    // Slouží k zápisu dat herní session do souboru s záznamy
    public class GameDataService
    {
        private string filePath = "GameRecords.txt"; // Path to the records file

        public List<GameRecord> LoadRecords()
        {
            var records = new List<GameRecord>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        // Create a new game record
                        records.Add(new GameRecord(
                            int.Parse(parts[0]),
                            parts[1],
                            parts[2],
                            DateTime.Parse(parts[3]),
                            decimal.Parse(parts[4])));
                    }
                }
            }
            return records;
        }

        // Save the record to the file
        public void SaveRecord(GameRecord record)
        {
            var recordLine = $"{record.Id}|{record.Nickname}|{record.Ending}|{record.Date}|{record.Money}";
            // Append the record line to the file
            File.AppendAllText(filePath, recordLine + Environment.NewLine);
        }

        // Clear history of records: use this method when the "Clear history" button is pressed in the game menu
        public void ClearHistory()
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Clear the file by setting its size to 0 bytes
                File.WriteAllText(filePath, String.Empty);
            }
        }
    }
}
