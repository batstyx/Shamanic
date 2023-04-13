using Hearthstone_Deck_Tracker.Hearthstone;
using Shamanic.Properties;
using System;
using static HearthDb.CardIds.Collectible;

namespace Shamanic.Effects
{
    internal class FrostSpells : IEffectConfig
    {
        public string Name => Strings.Get("FrostSpellsEffectName");
        public string[] ShowOnCardIds => new string[] { Shaman.BearonGlashear };
        public DisplayMode Player => Settings.Default.PlayerShowFrostSpells;
        public DisplayMode Opponent => Settings.Default.OpponentShowFrostSpells;
        public Predicate<Card> Condition => card => Helper.MatchSpellSchool(card, 3);
        public Func<Card, int> Increment => card => 1;
        public IncrementOn IncrementOn => IncrementOn.Play;        
    }
}
