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
            Cards = new Card[52];
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    Cards[(i * values.Length) + j] = new Card(suits[i], values[j]);

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

        public void DealCards(int players)
        {
            //determine how many cards each player gets and creates hands
            int maxCards = 0;
            int j = 0;
            Card[,] hands;
            switch (players)
            {
                case 2:
                    maxCards = 26;
                    hands = new Card[2, 26];
                    for (int i = 0; i < this.Cards.Length; i+=2)
                    {
                        hands[0, j] = this.Cards[i];
                        hands[1, j] = this.Cards[i + 1];
                        j++;
                    }   
                    break;
                case 3:
                    maxCards = 17;
                    hands = new Card[3, 17];
                    for (int i = 0; i < this.Cards.Length; i += 3)
                    {
                        hands[0,j] = this.Cards[i];
                        hands[1,j] = this.Cards[i + 1];
                        hands[2,j] = this.Cards[i + 2];
                        j++;
                    }
                    break;
                case 4:
                    maxCards = 13;
                    hands = new Card[4, 13];
                    for (int i = 0; i < this.Cards.Length; i += 4)
                    {
                        hands[0,j] = this.Cards[i];
                        hands[1,j] = this.Cards[i + 1];
                        hands[2,j] = this.Cards[i + 2];
                        hands[3,j] = this.Cards[i + 3];
                        j++;
                    }
                    break;

            }
            Console.WriteLine($"Each player gets {maxCards} cards");
        }
    }
}
