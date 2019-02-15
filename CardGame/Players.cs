using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CardGame
{
    public class Players
    {
        public string FirstName { get; set; }
        public string LastName { get; set;  }
        public bool Winner { get; set; }
        public Queue<Card> Hand { get; set; }
        public int PlayerNumber { get; set; }


        public Players()
        {
            Console.WriteLine("Enter first name");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last name");
            string lastName = Console.ReadLine();
            FirstName = firstName;
            LastName = lastName;
            Winner = false;
            Hand = new Queue<Card>();

        }
    }
}
