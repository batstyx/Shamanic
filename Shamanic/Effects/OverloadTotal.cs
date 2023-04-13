using Hearthstone_Deck_Tracker.Hearthstone;
using Shamanic.Properties;
using System;
using static HearthDb.CardIds.Collectible;

namespace Shamanic.Effects
{
    internal class OverloadTotal : IEffectConfig
    {
        public string Name => Strings.Get("OverloadTotalEffectName");
        public string[] ShowOnCardIds => new string[] { Shaman.SnowfuryGiantICECROWN };
        public DisplayMode Player => Settings.Default.PlayerShowOverloadTotal;
        public DisplayMode Opponent => Settings.Default.OpponentShowOverloadTotal;
        public Predicate<Card> Condition => card => card.Overload > 0;
        public Func<Card,int> Increment => card => card.Overload;
        public IncrementOn IncrementOn => IncrementOn.Play;
    }
}
