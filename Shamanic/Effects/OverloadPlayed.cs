using Hearthstone_Deck_Tracker.Hearthstone;
using Shamanic.Properties;
using System;
using static HearthDb.CardIds.Collectible;

namespace Shamanic.Effects
{
    internal class OverloadPlayed : IEffectConfig
    {
        public string Name => Strings.Get("OverloadPlayedEffectName");
        public string[] ShowOnCardIds => new string[] { Shaman.ChargedCall, Shaman.CommandTheElements };
        public DisplayMode Player => Settings.Default.PlayerShowOverloadPlayed;
        public DisplayMode Opponent => Settings.Default.OpponentShowOverloadPlayed;
        public Predicate<Card> Condition => card => card.Overload > 0;
        public Func<Card, int> Increment => card => 1;
        public IncrementOn IncrementOn => IncrementOn.Play;
    }
}
