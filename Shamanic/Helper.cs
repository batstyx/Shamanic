using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Shamanic.Properties;
using System.Linq;
using static HearthDb.CardIds.Collectible.Shaman;

namespace Shamanic
{
    internal static class Helper
    {
        public static Entity GetEntity(string cardId) => 
            Core.Game.Player.PlayerEntities.FirstOrDefault(x => (x.CardId == cardId) && x.Info.OriginalZone != null);
        private static bool? DeckContains(string cardId) => DeckList.Instance.ActiveDeck?.Cards.Any(x => x.Id == cardId);
        private static bool CheckShaman(string @class) => @class == "Shaman";
        private static bool CheckClass(string @class) => CheckShaman(@class) || @class == "Rogue" || @class == "Priest";
        private static bool CheckCard(bool? card, Entity entity) => card.HasValue && (entity != null || card.Value);
        private static bool CheckCard(string cardId) => CheckCard(DeckContains(cardId), GetEntity(cardId));

        private static bool ShowCounter(DisplayMode mode, string @class, string cardId = null)
        {
            switch (mode)
            {
                case DisplayMode.Always:
                    return true;
                case DisplayMode.Shaman:
                    return CheckShaman(@class);
                case DisplayMode.Class:
                    return CheckClass(@class);
                case DisplayMode.Card:
                    return CheckCard(cardId);
                case DisplayMode.Never:
                default:
                    return false;
            }
        }

        public static bool ShowOverloadTotalCounter => 
            ShowCounter(Settings.Default.OverloadTotalCounterDisplay, Core.Game.Player.Class, SnowfuryGiant);
        public static bool ShowOverloadPlayedCounter =>
            ShowCounter(Settings.Default.OverloadPlayedCounterDisplay, Core.Game.Player.Class, ChargedCall);
        public static bool ShowTotemsPlayedCounter =>
            ShowCounter(Settings.Default.TotemsPlayedCounterDisplay, Core.Game.Player.Class, ThingFromBelow);
        public static bool ShowOpponentCounters =>
            ShowCounter(Settings.Default.OpponentCountersDisplay, Core.Game.Opponent.Class);
    }
}
