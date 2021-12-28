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
    internal class OverloadPlayed : IEffectConfig
    {
        public string Name => Strings.Get("OverloadPlayedEffectName");
        public string[] Cards => new string[] { ChargedCall, CommandTheElements };
        public DisplayMode Player => Settings.Default.PlayerShowOverloadPlayed;
        public DisplayMode Opponent => Settings.Default.OpponentShowOverloadPlayed;
        public Predicate<Card> Condition => card => card.Overload > 0;
        public Func<Card, int> Increment => card => 1;
    }
}
