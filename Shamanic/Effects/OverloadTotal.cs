using Hearthstone_Deck_Tracker.Hearthstone;
using Shamanic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HearthDb.CardIds.Collectible.Shaman;

namespace Shamanic.Effects
{
    internal class OverloadTotal : IEffectConfig
    {
        public string Name => Strings.Get("OverloadTotalEffectName");
        public string[] Cards => new string[] { SnowfuryGiantCore };
        public DisplayMode Player => Settings.Default.PlayerShowOverloadTotal;
        public DisplayMode Opponent => Settings.Default.OpponentShowOverloadTotal;
        public Predicate<Card> Condition => card => card.Overload > 0;
        public Func<Card,int> Increment => card => card.Overload;
        public IncrementOn IncrementOn => IncrementOn.Play;
    }
}
