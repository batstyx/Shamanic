using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Shamanic.Properties;
using System.Linq;
using static HearthDb.CardIds;

namespace Shamanic
{
    internal static class Helper
    {
        public static Entity PlayerSnowfuryGiant => Core.Game.Player.PlayerEntities.FirstOrDefault(x => x.CardId == Collectible.Shaman.SnowfuryGiant && x.Info.OriginalZone != null);
        public static Entity PlayerThingFromBelow => Core.Game.Player.PlayerEntities.FirstOrDefault(x => (x.CardId == Collectible.Shaman.ThingFromBelow) && x.Info.OriginalZone != null);

        public static bool? SnowfuryGiantInDeck => DeckContains(Collectible.Shaman.SnowfuryGiant);
        public static bool? ThingFromBelowInDeck => DeckContains(Collectible.Shaman.ThingFromBelow);

        private static bool? DeckContains(string cardId) => DeckList.Instance.ActiveDeck?.Cards.Any(x => x.Id == cardId);

        private static bool CheckShaman(string @class) => @class == "Shaman";

        private static bool CheckClass(string @class) => CheckShaman(@class) || @class == "Rogue" || @class == "Priest";

        private static bool CheckCard(bool? card, Entity entity)=> card.HasValue && (entity != null || card.Value);

        public static bool ShowOverloadCounter => 
            Settings.Default.OverloadCounterDisplay == DisplayMode.Always
            || (Settings.Default.OverloadCounterDisplay == DisplayMode.Shaman && CheckShaman(Core.Game.Player.Class))
            || (Settings.Default.OverloadCounterDisplay == DisplayMode.Class && CheckClass(Core.Game.Player.Class))
            || (Settings.Default.OverloadCounterDisplay == DisplayMode.Card && CheckCard(SnowfuryGiantInDeck, PlayerSnowfuryGiant));

        public static bool ShowTotemsCounter => 
            Settings.Default.TotemsCounterDisplay == DisplayMode.Always
            || (Settings.Default.TotemsCounterDisplay == DisplayMode.Shaman && CheckShaman(Core.Game.Player.Class))
            || (Settings.Default.TotemsCounterDisplay == DisplayMode.Class && CheckClass(Core.Game.Player.Class))
            || (Settings.Default.TotemsCounterDisplay == DisplayMode.Card && CheckCard(ThingFromBelowInDeck, PlayerThingFromBelow));

        public static bool ShowOpponentCounters => 
            Settings.Default.OpponentCountersDisplay == DisplayMode.Always
            || (Settings.Default.OpponentCountersDisplay == DisplayMode.Shaman && CheckShaman(Core.Game.Opponent.Class))
            || (Settings.Default.OpponentCountersDisplay == DisplayMode.Class && CheckClass(Core.Game.Opponent.Class));
    }
}
