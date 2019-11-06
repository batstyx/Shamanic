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

        public static bool ShowOverloadCounter => !Core.Game.IsInMenu && (
            Settings.Default.OverloadCounterDisplay == DisplayMode.Always
                || (Settings.Default.OverloadCounterDisplay == DisplayMode.Auto && SnowfuryGiantInDeck.HasValue && (PlayerSnowfuryGiant != null || SnowfuryGiantInDeck.Value))
            );

        public static bool ShowTotemsCounter => !Core.Game.IsInMenu && (
            Settings.Default.TotemsCounterDisplay == DisplayMode.Always
                || (Settings.Default.TotemsCounterDisplay == DisplayMode.Auto && ThingFromBelowInDeck.HasValue && (PlayerThingFromBelow != null || ThingFromBelowInDeck.Value))
            );

        public static bool ShowOpponentCounters => !Core.Game.IsInMenu &&
            Settings.Default.OpponentCountersDisplay == DisplayMode.Always ||
            (Settings.Default.OpponentCountersDisplay == DisplayMode.Auto &&
            Core.Game.Opponent.Class == "Shaman");
    }
}
