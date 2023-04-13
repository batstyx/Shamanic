using Hearthstone_Deck_Tracker.Hearthstone;
using Shamanic.Properties;
using System;
using static HearthDb.CardIds.Collectible;

namespace Shamanic.Effects
{
    internal class TotemsPlayed : IEffectConfig
    {
        public string Name => Strings.Get("TotemsPlayedEffectName");
        public string[] ShowOnCardIds => new string[] { Shaman.ThingFromBelowOG, Shaman.Gigantotem };
        public DisplayMode Player => Settings.Default.PlayerShowTotemsPlayed;
        public DisplayMode Opponent => Settings.Default.OpponentShowTotemsPlayed;
        public Predicate<Card> Condition => 
            card => card.Type == "Minion" 
            && (card.Race == "Totem" || card.Race == "All");
        public Func<Card, int> Increment => card => 1;
        public IncrementOn IncrementOn => IncrementOn.Play | IncrementOn.CreateInPlay;
    }
}
