using System;
using System.Text.RegularExpressions;

namespace CardGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
                
            //Getting player count
            bool isNumber = false;
            string check;
            int playerCount = 0;
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");

            while (playerCount > 4 || playerCount < 2 || isNumber == false)
            {
                Console.WriteLine("Please enter number of players (between 2 and 4 inclusively): ");
                check = Console.ReadLine();
                if (regex.IsMatch(check)== true) {
                    isNumber = true;
                    playerCount = int.Parse(check);
                    if (playerCount > 4 || playerCount < 2)
                    {
                        Console.WriteLine("That player number is not in bounds!!");
                    }
                }
                else 
                {
                    Console.WriteLine("That's not a number!");
                }
            }
    

            //Creating an array to store all the players
            Players[] allPlayers = new Players[playerCount];
            for (int i=0; i<playerCount; i++)
            {
                Console.WriteLine($"Hello Player {i+1},"); //Players are named in the console initially as index + 1
                allPlayers[i] = new Players();
            }

            //Setting player number for each player
            for (int i=0; i<allPlayers.Length; i++) 
            {
                allPlayers[i].PlayerNumber = i + 1;
            }

            //Listing out all current players
            Console.WriteLine
            ("Current Players: ");
            foreach (var elem in allPlayers)
            {
                Console.WriteLine("Player"+ elem.PlayerNumber+": " +elem.FirstName + " " + elem.LastName + " ");
            }

            Deck deck = new Deck();

            deck.Shuffle();
            //Console.WriteLine(deck);
            //Console.ReadLine();

            deck.DealCards(playerCount);


        }
    }
}
