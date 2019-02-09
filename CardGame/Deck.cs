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
                        allplayers[0].Hand.Add(this.Cards[i]);
                        allplayers[1].Hand.Add(this.Cards[i+1]);    
                    }
                    break;
                case 3:
                    maxCards = 17;
                    for (int i = 0; i < (this.Cards.Length-1); i += 3)
                    {
                        allplayers[0].Hand.Add(this.Cards[i]);
                        allplayers[1].Hand.Add(this.Cards[i + 1]);
                        allplayers[1].Hand.Add(this.Cards[i + 2]);
                    }
                    break;
                case 4:
                    maxCards = 13;
                    for (int i = 0; i < this.Cards.Length; i += 4)
                    {
                        allplayers[0].Hand.Add(this.Cards[i]);
                        allplayers[1].Hand.Add(this.Cards[i + 1]);
                        allplayers[1].Hand.Add(this.Cards[i + 2]);
                    }
                    break;
            }
            Console.WriteLine($"Each player gets {maxCards} cards"); 
        }
    }
}
