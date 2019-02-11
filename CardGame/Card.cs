using System;
using System.Collections.Generic;
using System.Text;


namespace CardGame
{
    public class Card
    {
        public Card(string suit, string value, int rank)
        {
            Suit = suit;
            Value = value;
            Rank = rank;
        }

        public string Suit { get; }
        public string Value { get; }
        public int Rank { get; }


        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
