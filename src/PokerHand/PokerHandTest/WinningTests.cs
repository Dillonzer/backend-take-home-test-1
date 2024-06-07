

using FluentAssertions;
using PokerHand;

namespace PokerHandTest
{
    public class WinningTests
    {
        [Fact]
        public void Flush()
        {
            var black = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 5, Suit = CardSuit.Club }, new Card { Number = 3, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.Queen, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Club }});
            var white = new Hand(new List<Card> { new Card { Number = 7, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 6, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.Jack, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Club }});

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.Flush);
        }

        [Fact]
        public void ThreeOfAKind()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 4, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.Queen, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 2, Suit = CardSuit.Diamond }, new Card { Number = 2, Suit = CardSuit.Spade }, new Card { Number = (int)FaceValue.Jack, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(black);
            score.Value.Should().Be(Scorer.Ranks.ThreeOfAKind);
        }

        [Fact]
        public void TwoPair_NoMatches()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 6, Suit = CardSuit.Heart }, new Card { Number = 6, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 2, Suit = CardSuit.Diamond }, new Card { Number = 9, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.TwoPairs);
        }

        [Fact]
        public void TwoPair_OneMatch()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 6, Suit = CardSuit.Heart }, new Card { Number = 6, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 2, Suit = CardSuit.Diamond }, new Card { Number = 6, Suit = CardSuit.Spade }, new Card { Number = 6, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(black);
            score.Value.Should().Be(Scorer.Ranks.TwoPairs);
        }

        [Fact]
        public void TwoPair_TwoMatch()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 9, Suit = CardSuit.Club }, new Card { Number = 9, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = 4, Suit = CardSuit.Heart }, new Card { Number = 9, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.TwoPairs);
        }

        [Fact]
        public void TwoPair_Tie()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 9, Suit = CardSuit.Club }, new Card { Number = 9, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = 4, Suit = CardSuit.Heart }, new Card { Number = 9, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Value.Should().Be(Scorer.Ranks.Tie);
        }

        [Fact]
        public void Pair_OneMatch()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 6, Suit = CardSuit.Heart }, new Card { Number = 7, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Heart }, new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Spade }, new Card { Number = 8, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.Pair);
        }

        [Fact]
        public void Pair_NoMatch()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 6, Suit = CardSuit.Heart }, new Card { Number = 7, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 2, Suit = CardSuit.Diamond }, new Card { Number = 9, Suit = CardSuit.Spade }, new Card { Number = 8, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(black);
            score.Value.Should().Be(Scorer.Ranks.Pair);
        }

        [Fact]
        public void Pair_Tie()
        {
            var black = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Diamond }, new Card { Number = 9, Suit = CardSuit.Heart }, new Card { Number = 7, Suit = CardSuit.Diamond }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Spade } });
            var white = new Hand(new List<Card> { new Card { Number = 4, Suit = CardSuit.Heart }, new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Spade }, new Card { Number = 7, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Club } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Value.Should().Be(Scorer.Ranks.Tie);
        }

        [Fact]
        public void TestCase1FromREADME()
        {
            var black = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Heart }, new Card { Number = 3, Suit = CardSuit.Diamond }, new Card { Number = 5, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Diamond } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 3, Suit = CardSuit.Heart }, new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = 8, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Heart } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.HighCard);
        }

        [Fact]
        public void TestCase2FromREADME()
        {
            var black = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 2, Suit = CardSuit.Spade }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Spade }, new Card { Number = (int)FaceValue.Jack, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Club } });
            var white = new Hand(new List<Card> { new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Heart }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Diamond }, new Card { Number = 2, Suit = CardSuit.Heart }, new Card { Number = 3, Suit = CardSuit.Spade }, new Card { Number = 6, Suit = CardSuit.Spade } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.Pair);
        }

        [Fact]
        public void TestCase3FromREADME()
        {
            var black = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Heart }, new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = 4, Suit = CardSuit.Club }, new Card { Number = 3, Suit = CardSuit.Diamond }, new Card { Number = 4, Suit = CardSuit.Heart } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Spade }, new Card { Number = 8, Suit = CardSuit.Spade }, new Card { Number = (int)FaceValue.Ace, Suit = CardSuit.Spade }, new Card { Number = (int)FaceValue.Queen, Suit = CardSuit.Spade }, new Card { Number = 3, Suit = CardSuit.Spade } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.Flush);
        }

        [Fact]
        public void TestCase4FromREADME()
        {
            var black = new Hand(new List<Card> { new Card { Number = 3, Suit = CardSuit.Club }, new Card { Number = 7, Suit = CardSuit.Club }, new Card { Number = 6, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.Jack, Suit = CardSuit.Club }, new Card { Number = 4, Suit = CardSuit.Club } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Spade }, new Card { Number = 8, Suit = CardSuit.Spade }, new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = (int)FaceValue.Queen, Suit = CardSuit.Spade }, new Card { Number = 3, Suit = CardSuit.Spade } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(white);
            score.Value.Should().Be(Scorer.Ranks.Flush);
        }

        [Fact]
        public void TestCase5FromREADME()
        {
            var black = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Heart }, new Card { Number = 3, Suit = CardSuit.Diamond }, new Card { Number = 5, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Diamond } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Club }, new Card { Number = 3, Suit = CardSuit.Heart }, new Card { Number = 4, Suit = CardSuit.Spade }, new Card { Number = 8, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Heart } });

            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();

            score.Key.Should().Be(black);
            score.Value.Should().Be(Scorer.Ranks.HighCard);
        }



        [Fact]
        public void TestCase6FromREADME()
        {
            var black = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Heart }, new Card { Number = 3, Suit = CardSuit.Diamond }, new Card { Number = 5, Suit = CardSuit.Spade }, new Card { Number = 9, Suit = CardSuit.Club }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Diamond } });
            var white = new Hand(new List<Card> { new Card { Number = 2, Suit = CardSuit.Diamond }, new Card { Number = 3, Suit = CardSuit.Heart }, new Card { Number = 5, Suit = CardSuit.Club }, new Card { Number = 9, Suit = CardSuit.Spade }, new Card { Number = (int)FaceValue.King, Suit = CardSuit.Heart } });
 
            var scorer = new Scorer(new List<Hand> { black, white });

            var score = scorer.CalculateWinningHand();
;
            score.Value.Should().Be(Scorer.Ranks.Tie);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        public void OutOfRangeCardValue(int cardValue)
        {
            FluentActions.Invoking(() => new Card { Number = cardValue }).Should().Throw<ArgumentOutOfRangeException>();          

        }

    }
}