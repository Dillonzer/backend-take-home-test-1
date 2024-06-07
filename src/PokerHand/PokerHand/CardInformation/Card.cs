using System;

namespace PokerHand.CardInformation
{
    public class Card
    {
        int _number;

        public int Number 
        {
            get { return _number; }
            set 
            {
                if (value < 2 || value > 14)
                    throw new ArgumentOutOfRangeException();

                _number = value; 
            }
        }

        public CardSuit Suit { get; set; }
    }
}
