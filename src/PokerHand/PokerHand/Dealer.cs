using PokerHand.CardInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHand
{
    public static class Dealer
    {
        //If I needed to implement a shuffling algorithm, I would do so here rather than have the dealing per player functions.

        public static IList<Card> DealHand_Player1()
        {
            return new List<Card>() { 
                new Card { Number = 2, Suit = CardSuit.Club },
                new Card { Number = 4, Suit = CardSuit.Heart },
                new Card { Number = 6, Suit = CardSuit.Spade },
                new Card { Number = (int)FaceValue.Queen, Suit = CardSuit.Spade },
                new Card { Number = (int)FaceValue.Queen, Suit = CardSuit.Club }
            };
        }

        public static IList<Card> DealHand_Player2()
        {
            return new List<Card>() {
                new Card { Number = 7, Suit = CardSuit.Club },
                new Card { Number = 8, Suit = CardSuit.Heart },
                new Card { Number = 9, Suit = CardSuit.Diamond },
                new Card { Number = (int)FaceValue.Queen, Suit = CardSuit.Diamond },
                new Card { Number = (int)FaceValue.King, Suit = CardSuit.Heart }
            };
        }
    }
}
