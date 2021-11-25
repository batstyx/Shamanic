using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Shamanic.Properties;
using System.Linq;
using static HearthDb.CardIds;

namespace Shamanic
{
    internal static class Helper
    {
        public static Entity PlayerChargedCall => Core.Game.Player.PlayerEntities.FirstOrDefault(x => (x.CardId == Collectible.Shaman.ChargedCall) && x.Info.OriginalZone != null);
        public static Entity PlayerSnowfuryGiant => Core.Game.Player.PlayerEntities.FirstOrDefault(x => x.CardId == Collectible.Shaman.SnowfuryGiant && x.Info.OriginalZone != null);
        public static Entity PlayerThingFromBelow => Core.Game.Player.PlayerEntities.FirstOrDefault(x => (x.CardId == Collectible.Shaman.ThingFromBelow) && x.Info.OriginalZone != null);

        public static bool? ChargedCallInDeck => DeckContains(Collectible.Shaman.ChargedCall);
        public static bool? SnowfuryGiantInDeck => DeckContains(Collectible.Shaman.SnowfuryGiant);
        public static bool? ThingFromBelowInDeck => DeckContains(Collectible.Shaman.ThingFromBelow);

        private static bool? DeckContains(string cardId) => DeckList.Instance.ActiveDeck?.Cards.Any(x => x.Id == cardId);

        private static bool CheckShaman(string @class) => @class == "Shaman";

        private static bool CheckClass(string @class) => CheckShaman(@class) || @class == "Rogue" || @class == "Priest";

        private static bool CheckCard(bool? card, Entity entity)=> card.HasValue && (entity != null || card.Value);

        public static bool ShowOverloadTotalCounter => 
            Settings.Default.OverloadTotalCounterDisplay == DisplayMode.Always
            || (Settings.Default.OverloadTotalCounterDisplay == DisplayMode.Shaman && CheckShaman(Core.Game.Player.Class))
            || (Settings.Default.OverloadTotalCounterDisplay == DisplayMode.Class && CheckClass(Core.Game.Player.Class))
            || (Settings.Default.OverloadTotalCounterDisplay == DisplayMode.Card && CheckCard(SnowfuryGiantInDeck, PlayerSnowfuryGiant));

        public static bool ShowOverloadPlayedCounter =>
            Settings.Default.OverloadPlayedCounterDisplay == DisplayMode.Always
            || (Settings.Default.OverloadPlayedCounterDisplay == DisplayMode.Shaman && CheckShaman(Core.Game.Player.Class))
            || (Settings.Default.OverloadPlayedCounterDisplay == DisplayMode.Class && CheckClass(Core.Game.Player.Class))
            || (Settings.Default.OverloadPlayedCounterDisplay == DisplayMode.Card && CheckCard(ChargedCallInDeck, PlayerChargedCall));

        public static bool ShowTotemsPlayedCounter => 
            Settings.Default.TotemsPlayedCounterDisplay == DisplayMode.Always
            || (Settings.Default.TotemsPlayedCounterDisplay == DisplayMode.Shaman && CheckShaman(Core.Game.Player.Class))
            || (Settings.Default.TotemsPlayedCounterDisplay == DisplayMode.Class && CheckClass(Core.Game.Player.Class))
            || (Settings.Default.TotemsPlayedCounterDisplay == DisplayMode.Card && CheckCard(ThingFromBelowInDeck, PlayerThingFromBelow));

        public static bool ShowOpponentCounters => 
            Settings.Default.OpponentCountersDisplay == DisplayMode.Always
            || (Settings.Default.OpponentCountersDisplay == DisplayMode.Shaman && CheckShaman(Core.Game.Opponent.Class))
            || (Settings.Default.OpponentCountersDisplay == DisplayMode.Class && CheckClass(Core.Game.Opponent.Class));
    }
}
