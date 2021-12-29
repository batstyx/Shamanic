using Hearthstone_Deck_Tracker.Hearthstone;
using Shamanic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamanic.Effects
{
    internal class FrostSpells : IEffectConfig
    {
        public string Name => Strings.Get("FrostSpellsEffectName");
        public string[] Cards => new string[] { HearthDb.CardIds.Collectible.Shaman.BearonGlashear };
        public DisplayMode Player => Settings.Default.PlayerShowFrostSpells;
        public DisplayMode Opponent => Settings.Default.OpponentShowFrostSpells;
        public Predicate<Card> Condition => card => Helper.MatchSpellSchool(card, 3);
        public Func<Card, int> Increment => card => 1;
        public IncrementOn IncrementOn => IncrementOn.Play;

        
    }
}
