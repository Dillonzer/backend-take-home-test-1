using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHand
{
    public class Scorer
    {
        public enum Ranks
        {
            Tie,
            HighCard,
            Pair,
            TwoPairs,
            ThreeOfAKind,
            Flush
        }

        public IList<Hand> _players { get; }

        public Scorer(IList<Hand> players)
        {
            _players = players;
        }

        public KeyValuePair<Hand, Ranks> CalculateWinningHand()
        {
            var handResults = new Dictionary<Hand, Ranks>();
            var winner = new KeyValuePair<Hand, Ranks>(new Hand(), Ranks.Tie);

            foreach (var player in _players)
            {
                var playerList = player._cards.OrderByDescending(x => x.Number).ToList();

                var flush = playerList
                       .GroupBy(x => x.Suit)
                       .Where(x => x.Count() == 5)
                       .Select(x => x.FirstOrDefault()?.Suit);

                if(flush.Any())
                {
                    handResults.Add(player, Ranks.Flush);
                    continue;
                }

                var threeofakind = playerList
                    .GroupBy(x => x.Number)
                    .Where(x => x.Count() == 3)
                    .Select(x => x.FirstOrDefault().Number);

                if (threeofakind.Any())
                {
                    handResults.Add(player, Ranks.ThreeOfAKind);
                    continue;
                }

                var pairs = playerList
                    .GroupBy(x => x.Number)
                    .Where(x => x.Count() == 2)
                    .Select(x => x.FirstOrDefault().Number);

                if (pairs.Count() == 2)
                {
                    handResults.Add(player, Ranks.TwoPairs);
                    continue;
                }

                if (pairs.Count() == 1)
                {
                    handResults.Add(player, Ranks.Pair);
                    continue;
                }

                handResults.Add(player, Ranks.HighCard);


            }

            var compareHand = new KeyValuePair<Hand, Ranks>();

            foreach (var result in handResults)
            {
                if(compareHand.Key == null)
                {
                    compareHand = result;
                    continue;
                }

                if(result.Value > compareHand.Value)
                {
                    winner = result;
                    continue;
                }

                if(compareHand.Value == result.Value)
                {
                    switch(compareHand.Value)
                    {
                        case Ranks.Flush:
                        {
                            var compareHandCards = compareHand.Key._cards.OrderByDescending(x => x.Number).ToList();
                            var resultCards = result.Key._cards.OrderByDescending(x => x.Number).ToList();

                            for (var i = 0; i < 5; i++)
                            {
                                if (compareHandCards[i].Number > resultCards[i].Number)
                                {
                                    winner = compareHand;
                                    break;
                                }
                                else
                                {
                                    winner = result;
                                    break;
                                }
                            }
                            break;
                        }
                        case Ranks.ThreeOfAKind:
                        {
                            var compareHandThree = compareHand.Key._cards.GroupBy(x => x.Number)
                                    .Where(x => x.Count() == 3)
                                    .Select(x => x.FirstOrDefault().Number);

                            var currentThree = result.Key._cards.GroupBy(x => x.Number)
                                    .Where(x => x.Count() == 3)
                                    .Select(x => x.FirstOrDefault().Number);

                            if (currentThree.FirstOrDefault() > compareHandThree.FirstOrDefault())
                            {
                                winner = result;
                                break;
                            }
                            else
                            {
                                winner = compareHand;
                                break;
                            }
                        }
                        case Ranks.TwoPairs:
                        {
                            var comparePairValues = compareHand.Key._cards
                                .GroupBy(x => x.Number)
                                .Where(x => x.Count() == 2)
                                .Select(x => x.FirstOrDefault().Number);

                            var resultPairValues = result.Key._cards
                                .GroupBy(x => x.Number)
                                .Where(x => x.Count() == 2)
                                .Select(x => x.FirstOrDefault().Number);

                            if (comparePairValues.OrderByDescending(x => x).ToList()[1] == resultPairValues.OrderByDescending(x => x).ToList()[1] && comparePairValues.OrderByDescending(x => x).ToList()[0] == resultPairValues.OrderByDescending(x => x).ToList()[0]) 
                            {
                                var compareDecidingNumber = compareHand.Key._cards
                                    .GroupBy(x => x.Number)
                                    .Where(x => x.Count() != 2)
                                    .Select(x => x.FirstOrDefault().Number);

                                var resultDecidingNumber = result.Key._cards
                                    .GroupBy(x => x.Number)
                                    .Where(x => x.Count() != 2)
                                    .Select(x => x.FirstOrDefault().Number);

                                for (var i = 0; i < 1; i++)
                                {
                                    if (resultDecidingNumber.OrderByDescending(x => x).ToList()[i] > compareDecidingNumber.OrderByDescending(x => x).ToList()[i])
                                    {
                                        winner = result;
                                        break;
                                    }
                                    else if (resultDecidingNumber.OrderByDescending(x => x).ToList()[i] < compareDecidingNumber.OrderByDescending(x => x).ToList()[i])
                                    {
                                        winner = compareHand;
                                        break;
                                    }
                                }
                                }
                            else
                            {
                                if(resultPairValues.ToList()[1] > comparePairValues.ToList()[1])
                                {
                                    winner = result;
                                }
                                else if (resultPairValues.ToList()[1] < comparePairValues.ToList()[1])
                                {
                                    winner = compareHand;
                                }
                                else if (resultPairValues.ToList()[0] > comparePairValues.ToList()[0])
                                {
                                    winner = result;
                                }
                                else if (resultPairValues.ToList()[0] < comparePairValues.ToList()[0])
                                {
                                    winner = compareHand;
                                }
                            }

                            break;
                        }
                        case Ranks.Pair:
                        {
                            var comparePairValues = compareHand.Key._cards
                                .GroupBy(x => x.Number)
                                .Where(x => x.Count() == 2)
                                .Select(x => x.FirstOrDefault().Number);

                            var resultPairValues = result.Key._cards
                                .GroupBy(x => x.Number)
                                .Where(x => x.Count() == 2)
                                .Select(x => x.FirstOrDefault().Number);

                            if (comparePairValues.FirstOrDefault() == resultPairValues.FirstOrDefault())
                            { 
                                var compareDecidingNumber = compareHand.Key._cards
                                    .GroupBy(x => x.Number)
                                    .Where(x => x.Count() != 2)
                                    .Select(x => x.FirstOrDefault().Number);

                                var resultDecidingNumber = result.Key._cards
                                    .GroupBy(x => x.Number)
                                    .Where(x => x.Count() != 2)
                                    .Select(x => x.FirstOrDefault().Number);

                                for (var i = 0; i < 2; i++)
                                {
                                    if (resultDecidingNumber.OrderByDescending(x => x).ToList()[i] > compareDecidingNumber.OrderByDescending(x => x).ToList()[i])
                                    {
                                        winner = result;
                                        break;
                                    }
                                    else if (resultDecidingNumber.OrderByDescending(x => x).ToList()[i] < compareDecidingNumber.OrderByDescending(x => x).ToList()[i])
                                    {
                                        winner = compareHand;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (resultPairValues.FirstOrDefault() > comparePairValues.FirstOrDefault())
                                {
                                    winner = result;
                                }
                                else if (resultPairValues.FirstOrDefault() < comparePairValues.FirstOrDefault())
                                {
                                    winner = compareHand;
                                }
                            }
                            break;
                        }
                        case Ranks.HighCard:
                        {
                            var compareHandCards = compareHand.Key._cards.OrderByDescending(x => x.Number).ToList();
                            var resultCards = result.Key._cards.OrderByDescending(x => x.Number).ToList();
                            
                            for (var i = 0; i < 5; i++)
                            {
                                if (compareHandCards[i].Number > resultCards[i].Number)
                                {
                                    winner = compareHand;
                                    break;
                                }
                                else if (compareHandCards[i].Number < resultCards[i].Number)
                                {
                                    winner = result;
                                    break;
                                }
                            }
                            break;
                        }
                        default:
                            break;
                    }
                }

            }

            return winner;
        }
    }
}
