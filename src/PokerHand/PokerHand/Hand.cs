using PokerHand.CardInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHand
{
    public class Hand
    {
        public List<Card> _cards { get; }

        public Hand()
        {
            _cards = new List<Card>(); //I would call Dealer.DealHand here if I had a shuffling algorithm
        }

        public Hand(List<Card> cards)
        {
            _cards = cards;
        }
    }
}
