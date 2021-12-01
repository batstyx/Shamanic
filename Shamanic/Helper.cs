﻿using Hearthstone_Deck_Tracker;
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
        private static bool CheckCards(params string[] cardIds)
        {
            foreach (var cardId in cardIds)
            {
                if (CheckCard(cardId)) return true;
            }
            return false;
        }
        private static bool ShowCounter(DisplayMode mode, string @class, params string[] cardIds)
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
                    return CheckCards(cardIds);
                case DisplayMode.Never:
                default:
                    return false;
            }
        }

        public static bool ShowPlayerOverloadTotal => 
            ShowCounter(Settings.Default.PlayerShowOverloadTotal, Core.Game.Player.Class, SnowfuryGiant);
        public static bool ShowPlayerOverloadPlayed =>
            ShowCounter(Settings.Default.PlayerShowOverloadPlayed, Core.Game.Player.Class, ChargedCall, CommandTheElements);
        public static bool ShowPlayerTotemsPlayed =>
            ShowCounter(Settings.Default.PlayerShowTotemsPlayed, Core.Game.Player.Class, ThingFromBelow);
        public static bool ShowOpponentTotemsPlayed =>
            ShowCounter(Settings.Default.OpponentShowOverloadTotal, Core.Game.Opponent.Class);
        public static bool ShowOpponentOverloadPlayed =>
            ShowCounter(Settings.Default.OpponentShowOverloadPlayed, Core.Game.Opponent.Class);
        public static bool ShowOpponentOverloadTotal =>
            ShowCounter(Settings.Default.OpponentShowTotemsPlayed, Core.Game.Opponent.Class);
    }
}
