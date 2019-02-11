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
                    if (playerCount > 4)
                    {
                        Console.WriteLine("Too many players!");
                    }
                    else if (playerCount < 2)
                    {
                        Console.WriteLine("Too few players!");
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
            deck.DealCards(playerCount, allPlayers); //Deal cards to players
            //Console.WriteLine(deck);
            //Console.ReadLine();
            bool gameOver = false;
            int rounds = 0;
            switch (playerCount) {
                case 2:
                    while (gameOver == false && rounds < 2000 && (allPlayers[0].Winner == false && allPlayers[1].Winner == false))
                    {
                        deck.Round(playerCount, allPlayers, gameOver);
                        //Console.WriteLine("_________________________________________________");
                        //foreach(var elem in allPlayers[0].Hand)
                        //{
                        //    Console.Write($"{elem.Value} of {elem.Suit} |");
                        //}
                        //Console.WriteLine("");
                        //Console.WriteLine("_________________________________________________");
                        //foreach (var elem in allPlayers[1].Hand)
                        //{
                        //    Console.Write($"{elem.Value} of {elem.Suit} |");
                        //}
                        //Console.WriteLine("");
                        //Console.WriteLine("_________________________________________________");
                        rounds +=1;
                        if (allPlayers[0].Hand.Count == 0 || allPlayers[1].Hand.Count == 0)
                        {
                            if (allPlayers[1].Hand.Count == 52)
                            {
                                allPlayers[1].Winner = true;
                                Console.WriteLine("Player 2 has all the cards");
                                gameOver = true;
                            }
                            else if (allPlayers[0].Hand.Count == 52)
                            {
                                allPlayers[0].Winner = true;
                                Console.WriteLine("Player 1 has all the cards");
                                gameOver = true;
                            }
                            else
                            {
                                Console.WriteLine("A player ran out of cards.. ");
                                gameOver = true;
                            }
                        }
                    }
                    if (rounds== 2000)
                    {
                        allPlayers[0].Winner = false;
                        allPlayers[1].Winner = false;
                    }

                    if (allPlayers[0].Hand.Count > 0 && allPlayers[1].Hand.Count == 0)
                    {
                        allPlayers[0].Winner = true;
                        allPlayers[1].Winner = false;
                    }
                    else if (allPlayers[1].Hand.Count > 0 && allPlayers[0].Hand.Count == 0)
                    {
                        allPlayers[1].Winner = true;
                        allPlayers[0].Winner = false;
                    }
                    Console.WriteLine("_____ Game Summary _____");
                    Console.WriteLine($"Did player 1 win the game? {allPlayers[0].Winner}. Player 1 has {allPlayers[0].Hand.Count} cards left");
                    Console.WriteLine($"Did player 2 win the game? {allPlayers[1].Winner}. Player 2 has {allPlayers[1].Hand.Count} cards left");
                    break;
                case 3:
                    //TODO
                    Console.WriteLine($"Functionality has not yet been added for {playerCount} players");
                    break;
                case 4:
                    //TODO
                    Console.WriteLine($"Functionality has not yet been added for {playerCount} players");
                    break;

            }
            Console.WriteLine($"There were {rounds} rounds");
            if (allPlayers[0].Winner == false && allPlayers[1].Winner == false)
            {
                Console.WriteLine("No one won this game");
            }

            if (rounds == 2000)
            {
                Console.WriteLine("Your game ended because it was taking too long..");
            }

        }


    }
}
