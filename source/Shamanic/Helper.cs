using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System.Linq;

namespace Shamanic
{
    internal static class Helper
    {
        public static Entity PlayerSnowfuryGiant => Core.Game.Player.PlayerEntities.FirstOrDefault(x => x.CardId == HearthDb.CardIds.Collectible.Shaman.SnowfuryGiant && x.Info.OriginalZone != null);
        public static Entity PlayerThingFromBelow => Core.Game.Player.PlayerEntities.FirstOrDefault(x => x.CardId == HearthDb.CardIds.Collectible.Shaman.ThingFromBelow && x.Info.OriginalZone != null);

        public static bool? SnowfuryGiantInDeck => DeckContains(HearthDb.CardIds.Collectible.Shaman.SnowfuryGiant);
        public static bool? ThingFromBelowInDeck => DeckContains(HearthDb.CardIds.Collectible.Shaman.ThingFromBelow);

        private static bool? DeckContains(string cardId) => DeckList.Instance.ActiveDeck?.Cards.Any(x => x.Id == cardId);

        public static bool ShowOverloadCounter => !Core.Game.IsInMenu && (
            Config.Instance.PlayerSpellsCounter == DisplayMode.Always
                || (Config.Instance.PlayerSpellsCounter == DisplayMode.Auto && SnowfuryGiantInDeck.HasValue && (PlayerSnowfuryGiant != null || SnowfuryGiantInDeck.Value))
            );

        public static bool ShowTotemsCounter => !Core.Game.IsInMenu && (
            Config.Instance.PlayerSpellsCounter == DisplayMode.Always
                || (Config.Instance.PlayerSpellsCounter == DisplayMode.Auto && ThingFromBelowInDeck.HasValue && (PlayerThingFromBelow != null || ThingFromBelowInDeck.Value))
            );
    }
}
