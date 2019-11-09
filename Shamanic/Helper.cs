using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Shamanic.Properties;
using System.Linq;

namespace Shamanic
{
    internal static class Helper
    {
        public static Entity PlayerSnowfuryGiant => Core.Game.Player.PlayerEntities.FirstOrDefault(x => x.CardId == HearthDb.CardIds.Collectible.Shaman.SnowfuryGiant && x.Info.OriginalZone != null);
        public static Entity PlayerThingFromBelow => Core.Game.Player.PlayerEntities.FirstOrDefault(x => (x.CardId == HearthDb.CardIds.Collectible.Shaman.ThingFromBelowOG || x.CardId == HearthDb.CardIds.Collectible.Shaman.ThingFromBelowWILD_EVENT) && x.Info.OriginalZone != null);

        public static bool? SnowfuryGiantInDeck => DeckContains(HearthDb.CardIds.Collectible.Shaman.SnowfuryGiant);
        public static bool? ThingFromBelowInDeck => DeckContains(HearthDb.CardIds.Collectible.Shaman.ThingFromBelowOG) | DeckContains(HearthDb.CardIds.Collectible.Shaman.ThingFromBelowWILD_EVENT);

        private static bool? DeckContains(string cardId) => DeckList.Instance.ActiveDeck?.Cards.Any(x => x.Id == cardId);

        private static bool CheckClass(string @class)
        {
            return @class == "Shaman" || @class == "Rogue" || @class == "Priest";
        }

        private static bool CheckCard(bool? card, Entity entity)
        {
            return card.HasValue && (entity != null || card.Value);
        }

        public static bool ShowOverloadCounter => 
            Settings.Default.OverloadCounterDisplay == DisplayMode.Always
            || (Settings.Default.OverloadCounterDisplay == DisplayMode.Class && CheckClass(Core.Game.Player.Class))
            || (Settings.Default.OverloadCounterDisplay == DisplayMode.Card && CheckCard(SnowfuryGiantInDeck, PlayerSnowfuryGiant));

        public static bool ShowTotemsCounter => 
            Settings.Default.TotemsCounterDisplay == DisplayMode.Always
            || (Settings.Default.TotemsCounterDisplay == DisplayMode.Class && CheckClass(Core.Game.Player.Class))
            || (Settings.Default.TotemsCounterDisplay == DisplayMode.Card && CheckCard(ThingFromBelowInDeck, PlayerThingFromBelow));

        public static bool ShowOpponentCounters => 
            Settings.Default.OpponentCountersDisplay == DisplayMode.Always 
            || (Settings.Default.OpponentCountersDisplay == DisplayMode.Class && CheckClass(Core.Game.Opponent.Class));
    }
}
