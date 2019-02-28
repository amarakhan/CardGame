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
                if (regex.IsMatch(check) == true)
                {
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
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine($"Hi Player {i + 1},"); //Players are named in the console initially as index + 1
                allPlayers[i] = new Players();
            }

            //Setting player number for each player
            for (int i = 0; i < allPlayers.Length; i++)
            {
                allPlayers[i].PlayerNumber = i + 1;
            }

            //Listing out all current players
            Console.WriteLine
            ("Current Players: ");
            foreach (var elem in allPlayers)
            {
                Console.WriteLine("P" + elem.PlayerNumber + ": " + elem.FirstName + " " + elem.LastName + " ");
            }

            Deck deck = new Deck();
            deck.Shuffle();
            deck.DealCards(playerCount, allPlayers); //Deal cards to players
            //Console.WriteLine(deck);
            //Console.ReadLine();

            //Checking that players were dealt cards
            //foreach (var elem in allPlayers[0].Hand)
            //{
            //    Console.WriteLine(elem.Suit);
            //}


            //Start the game
            bool gameOver = false;
            int rounds = 0;

            switch (playerCount)
            {
                case 2:
                    while (rounds < 2000 && gameOver == false)
                    {
                        Console.WriteLine($"_____ Round {rounds}_____");

                        if (allPlayers[0].Hand.Count == 0 && allPlayers[1].Hand.Count == 0)
                        {
                            allPlayers[0].Winner = false;
                            allPlayers[1].Winner = false;
                            gameOver = true;
                            Console.WriteLine("The game is over, both players ran out of cards, no winners");
                        }
                        else if (allPlayers[0].Hand.Count == 0)
                        {
                            allPlayers[0].Winner = false;
                            allPlayers[1].Winner = true;
                            gameOver = true;
                            Console.WriteLine("The game is over, P1 ran out of cards");
                        }
                        else if (allPlayers[1].Hand.Count == 0)
                        {
                            allPlayers[0].Winner = true;
                            allPlayers[1].Winner = false;
                            gameOver = true;
                            Console.WriteLine("The game is over, P2 ran out of cards");
                        }
                        else
                        {
                            deck.Round(playerCount, allPlayers, gameOver);
                        }
                        rounds++;
                        //Console.WriteLine($"End of round {rounds}");
                    }

                    Console.WriteLine($"There were {rounds} rounds");
                    if (allPlayers[0].Winner == true)
                    {
                        Console.WriteLine("Player 1 won! Player 2 lost!");
                    }
                    else
                    {
                        Console.WriteLine("Player 2 won! Player 1 lost!");
                    }
                    break;
                case 3:
                    Console.WriteLine("Game is not available for 3 players yet");
                    gameOver = true;
                    break;
                case 4:
                    Console.WriteLine("Game is not available for 4 players yet");
                    gameOver = true;
                    break;
            }
            //Console.WriteLine("P1 is winner? "+allPlayers[0].Winner);
            //Console.WriteLine("P2 is winner? " + allPlayers[1].Winner);
        }//end of main method
    }//end of program class
}
