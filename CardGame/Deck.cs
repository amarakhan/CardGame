using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CardGame
{
    public class Deck
    {
        //This is a special type of method called a constructor.
        //it is run every time you create a deck of cards
        public Deck()
        {
            string[] suits = { "Spades", "Clubs", "Hearts", "Diamonds" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            int[] rank = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            Cards = new Card[52];
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    Cards[(i * values.Length) + j] = new Card(suits[i], values[j], rank[i]);

                }
            }
        }

        public Card[] Cards { get; private set; }

        public void Shuffle()
        {
            Random rng = new Random();
            for (int i = 0; i < this.Cards.Length * 100; i++)
            {

                int position1 = rng.Next(0, this.Cards.Length);
                int position2 = rng.Next(0, this.Cards.Length);
                Card temp = this.Cards[position1];
                this.Cards[position1] = this.Cards[position2];
                this.Cards[position2] = temp;
            }
        }

        public override string ToString()
        {
            return string.Join(",", Cards.Select(x => x.ToString()));
        }

        public void DealCards(int players, Players[] allplayers)
        {
            //determine how many cards each player gets and creates hands
            int maxCards = 0;
            switch (players)
            {
                case 2:
                    maxCards = 26;
                    for (int i = 0; i < this.Cards.Length; i+=2)
                    {
                        allplayers[0].Hand.Enqueue(this.Cards[i]);
                        allplayers[1].Hand.Enqueue(this.Cards[i+1]);    
                    }
                    break;
                case 3:
                    maxCards = 17;
                    for (int i = 0; i < (this.Cards.Length-1); i += 3)
                    {
                        allplayers[0].Hand.Enqueue(this.Cards[i]);
                        allplayers[1].Hand.Enqueue(this.Cards[i + 1]);
                        allplayers[1].Hand.Enqueue(this.Cards[i + 2]);
                    }
                    break;
                case 4:
                    maxCards = 13;
                    for (int i = 0; i < this.Cards.Length; i += 4)
                    {
                        allplayers[0].Hand.Enqueue(this.Cards[i]);
                        allplayers[1].Hand.Enqueue(this.Cards[i + 1]);
                        allplayers[1].Hand.Enqueue(this.Cards[i + 2]);
                    }
                    break;
            }
            Console.WriteLine($"Each player gets {maxCards} cards"); 
        }

        public void Round(int players, Players[] allplayers, bool gameover)
        {
            List<Card> tempHand = new List<Card>();

            switch (players)
            {
                case 2:
                    var p1Card = allplayers[0].Hand.Dequeue();
                    var p2Card = allplayers[1].Hand.Dequeue();

                    tempHand.Add(p1Card);
                    tempHand.Add(p2Card);
                    Console.WriteLine($"Player 1 is playing {p1Card.Value} of {p1Card.Suit}, and Player 2 is playing {p2Card.Value} of {p2Card.Suit}");


                    if (p1Card.Rank >p2Card.Rank)
                    {
                        foreach(var elem in tempHand)
                        {
                            allplayers[0].Hand.Enqueue(elem);
                        }
                        Console.WriteLine("Player 1 won this round");
                    }
                    else if (p2Card.Rank>p1Card.Rank)
                    {
                        foreach (var elem in tempHand)
                        {
                            allplayers[1].Hand.Enqueue(elem);
                        }
                        Console.WriteLine("Player 2 won this round");
                    }
                    //WAR
                    else //(p1Card.Rank == p2Card.Rank)
                    {
                        if (allplayers[0].Hand.Count < 3 && allplayers[1].Hand.Count < 3)
                        {
                            allplayers[0].Winner = false;
                            allplayers[1].Winner = false;
                            Console.WriteLine("THIS IS WAR BUT.. The game is over, player 1 and 2 do not have enough cards for war");
                            gameover = true;
                        }
                        else if (allplayers[0].Hand.Count < 3)
                        {
                            allplayers[0].Winner = false;
                            allplayers[1].Winner = true;
                            Console.WriteLine("THIS IS WAR BUT.. The game is over, player 1 does not have enough cards for war");
                            gameover = true;
                        }
                        else if (allplayers[1].Hand.Count < 3)
                        {
                            allplayers[1].Winner = false;
                            allplayers[0].Winner = true;
                            Console.WriteLine("THIS IS WAR BUT.. The game is over, player 2 does not have enough cards for war");
                            gameover = true;
                        }
                        else
                        {
                            Console.WriteLine("THIS IS WAR");
                            var card1P1 = allplayers[0].Hand.Dequeue();
                            var card2P1 = allplayers[0].Hand.Dequeue();
                            var card3P1 = allplayers[0].Hand.Dequeue();


                            var card1P2 = allplayers[1].Hand.Dequeue();
                            var card2P2 = allplayers[1].Hand.Dequeue();
                            var card3P2 = allplayers[1].Hand.Dequeue();


                            Card[] p1Hand = { p1Card, card1P1, card2P1, card3P1 };
                            Card[] p2Hand = { p2Card, card1P2, card2P2, card3P2 };
                            if (p1Hand[0]==p2Hand[0] && p1Hand[1] == p2Hand[1] && p1Hand[2] == p2Hand[2] && p1Hand[3] == p2Hand[3])
                            {
                                Console.WriteLine("No one has won war... cards will be discarded");
                            }
                            else
                            {
                                if (p1Hand[0].Rank > p2Hand[0].Rank)
                                {
                                    foreach (var elem in p1Hand)
                                    {
                                        allplayers[0].Hand.Enqueue(elem);
                                    }
                                    Console.WriteLine("Player 1 won this round");
                                }
                                else if (p2Hand[0].Rank > p1Hand[0].Rank)
                                {
                                    foreach (var elem in p1Hand)
                                    {
                                        allplayers[1].Hand.Enqueue(elem);
                                    }
                                    Console.WriteLine("Player 2 won this round");
                                }
                                else //both cards at index 0 are equal
                                {
                                    if (p1Hand[1].Rank > p2Hand[1].Rank)
                                    {
                                        foreach (var elem in p1Hand)
                                        {
                                            allplayers[0].Hand.Enqueue(elem);
                                        }
                                        Console.WriteLine("Player 1 won this round");
                                    }
                                    else if (p2Hand[1].Rank > p1Hand[1].Rank)
                                    {
                                        foreach (var elem in p1Hand)
                                        {
                                            allplayers[1].Hand.Enqueue(elem);
                                        }
                                        Console.WriteLine("Player 2 won this round");
                                    }
                                    else //both cards at index 1 are equal
                                    {
                                        if (p1Hand[2].Rank > p2Hand[2].Rank)
                                        {
                                            foreach (var elem in p1Hand)
                                            {
                                                allplayers[0].Hand.Enqueue(elem);
                                            }
                                            Console.WriteLine("Player 1 won this round");
                                        }
                                        else if (p2Hand[2].Rank > p1Hand[2].Rank)
                                        {
                                            foreach (var elem in p1Hand)
                                            {
                                                allplayers[1].Hand.Enqueue(elem);
                                            }
                                            Console.WriteLine("Player 2 won this round");
                                        }
                                        else
                                        {
                                            if (p1Hand[3].Rank > p2Hand[3].Rank)
                                            {
                                                foreach (var elem in p1Hand)
                                                {
                                                    allplayers[0].Hand.Enqueue(elem);
                                                }
                                                Console.WriteLine("Player 1 won this round");
                                            }
                                            else if (p2Hand[3].Rank > p1Hand[3].Rank)
                                            {
                                                foreach (var elem in p1Hand)
                                                {
                                                    allplayers[1].Hand.Enqueue(elem);
                                                }
                                                Console.WriteLine("Player 2 won this round");
                                            }
                                        }

                                    }
                                }
                                //TODO
                                //if (allplayers[1].Hand.Count == 52)
                                //{
                                //    allplayers[1].Winner = true;
                                //    Console.WriteLine("Player 2 has all the cards");
                                //}
                                //else if (allplayers[0].Hand.Count == 52)
                                //{
                                //    allplayers[0].Winner = true;
                                //    Console.WriteLine("Player 1 has all the cards");
                                //}
                            }

                        }
                    }

                    break;
                case 3:
                    //TODO
                    break;
                case 4:
                    //TODO
                    break;
            }
        }
    }
}
