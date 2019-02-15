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

        public void Shuffle() //this method shuffles the deck of cards
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

        //This method deals cards to the hand property for each player stored in the array allplayers
        public void DealCards(int players, Players[] allplayers)
        {
            int maxCards = 0;
            switch (players)
            {
                case 2:
                    maxCards = 26;
                    for (int i=0; i<this.Cards.Length; i+=2)
                    {
                        allplayers[0].Hand.Enqueue(this.Cards[i]);
                        allplayers[1].Hand.Enqueue(this.Cards[i+1]);
                    }
                    break;
                case 3:
                    maxCards = 17;
                    for (int i = 0; i < this.Cards.Length; i += 3)
                    {
                        allplayers[0].Hand.Enqueue(this.Cards[i]);
                        allplayers[1].Hand.Enqueue(this.Cards[i + 1]);
                        allplayers[2].Hand.Enqueue(this.Cards[i + 2]);
                    }
                    break;
                case 4:
                    maxCards = 13;
                    for (int i = 0; i < this.Cards.Length; i += 3)
                    {
                        allplayers[0].Hand.Enqueue(this.Cards[i]);
                        allplayers[1].Hand.Enqueue(this.Cards[i + 1]);
                        allplayers[2].Hand.Enqueue(this.Cards[i + 2]);
                        allplayers[3].Hand.Enqueue(this.Cards[i + 3]);
                    }
                    break;
            } //end of switch statement for dealing cards based on player count
            Console.WriteLine($"Each player gets {maxCards} cards");
        }//end of DealCards method

        public void Round(int players, Players[] allplayers, bool gameover)
        {
            List<Card> tempHand = new List<Card>();
            Card[] p1tempHand = new Card[4];
            Card[] p2tempHand = new Card[4];
            switch (players)
            {
                case 2: //the round that's played when there are only 2 players
                    var p1Card = allplayers[0].Hand.Dequeue();
                    var p2Card = allplayers[1].Hand.Dequeue();
                    tempHand.Add(p1Card);
                    tempHand.Add(p2Card);
                    Console.WriteLine($"Player 1 is playing {p1Card.Value} of {p1Card.Suit}, and Player 2 is playing {p2Card.Value} of {p2Card.Suit}");

                    if (p1Card.Rank > p2Card.Rank) //If p1 plays the higher card
                    {
                        foreach (var elem in tempHand)
                        {
                            allplayers[0].Hand.Enqueue(elem);
                        }
                    }
                    else if (p1Card.Rank < p2Card.Rank){ //If p2 plays the higher card
                        foreach (var elem in tempHand)
                        {
                            allplayers[1].Hand.Enqueue(elem);
                        }
                    }
                    else //WAR (p1 card same value as p2 card)
                    {
                        if (allplayers[0].Hand.Count < 3 && allplayers[1].Hand.Count < 3)
                        {
                            Console.WriteLine("P1 and P2 do not have enough cards for war, both lose");
                            allplayers[0].Winner = false;
                            allplayers[1].Winner = false;
                            gameover = true;
                        }
                        else if (allplayers[0].Hand.Count < 3)
                        {
                            allplayers[0].Winner = false;
                            allplayers[1].Winner = true;
                            Console.WriteLine("P1 does not have enough cards for war, P2 wins");
                            gameover = true;
                        }
                        else if (allplayers[1].Hand.Count < 3)
                        {
                            Console.WriteLine("P2 does not have enough cards for war, P1 wins");
                            allplayers[0].Winner = true;
                            allplayers[1].Winner = false;
                            gameover = true;
                        }
                        else
                        {
                            bool greatestP1 = false;
                            bool greatestP2 = false;
                            //TODO both players qualify for WAR this round
                            p1tempHand[0] = p1Card;
                            p1tempHand[1] = allplayers[0].Hand.Dequeue();
                            p1tempHand[2] = allplayers[0].Hand.Dequeue();
                            p1tempHand[3] = allplayers[0].Hand.Dequeue();
                            p2tempHand[0] = p2Card;
                            p2tempHand[1] = allplayers[1].Hand.Dequeue();
                            p2tempHand[2] = allplayers[1].Hand.Dequeue();
                            p2tempHand[3] = allplayers[1].Hand.Dequeue();

                            while (greatestP1 == false && greatestP2 == false)
                            {
                                for (int i=0; i<p1tempHand.Length;i++)
                                {
                                    for (int j=0; j<p2tempHand.Length;j++)
                                    {
                                        if (p1tempHand[i].Rank > p2tempHand[j].Rank)
                                        {
                                            greatestP1 = true;
                                        }
                                        else if(p1tempHand[i].Rank < p2tempHand[j].Rank)
                                        {
                                            greatestP2 = true;
                                        }
                                        else{
                                            greatestP1 = false;
                                            greatestP2 = false;
                                        }

                                    }
                                }
                            } //end of while loop for finding out if p1 or p2 has a higher card first
                            if (greatestP1 == true)
                            {
                                foreach(var elem in p1tempHand)
                                {
                                    allplayers[0].Hand.Enqueue(elem);
                                }
                                foreach (var elem in p2tempHand)
                                {
                                    allplayers[0].Hand.Enqueue(elem);
                                }
                            }
                            else if (greatestP2 == true)
                            {
                                foreach (var elem in p1tempHand)
                                {
                                    allplayers[0].Hand.Enqueue(elem);
                                }
                                foreach (var elem in p2tempHand)
                                {
                                    allplayers[0].Hand.Enqueue(elem);
                                }
                            }
                        }//end of WAR if both players have enough cards
                    }//end of WAR COMPLETELY for this round

                    break;
                case 3: //the round that's played when there are 3 players
                    break;
                case 4: //the round that's played when there are 4 players
                    break;
            } //end of switch statement
        } //end of Round method
    }//end of Deck class
} //end of CardGame namespace
