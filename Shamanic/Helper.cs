using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System.Linq;

namespace Shamanic
{
    internal static class Helper
    {
        public static Card GetCard(string cardId) => Database.GetCardFromId(cardId);
        public static Entity GetEntity(Card card) => 
            Core.Game.Player.PlayerEntities.FirstOrDefault(x => (x.CardId == card.Id || x.Name == card.Name) && x.Info.OriginalZone != null);
        private static bool? DeckContains(Card card) => DeckList.Instance.ActiveDeck?.Cards.Any(x => x.Id == card.Id || x.Name == card.Name);
        private static bool CheckShaman(string @class) => @class == "Shaman";
        private static bool CheckClass(string @class) => CheckShaman(@class) || @class == "Rogue" || @class == "Priest";
        private static bool CheckCard(bool? cardIsInDeck, Entity entity) => cardIsInDeck.HasValue && (entity != null || cardIsInDeck.Value);
        private static bool CheckCard(Card card) => CheckCard(DeckContains(card), GetEntity(card));
        private static bool CheckCard(string cardId) => CheckCard(GetCard(cardId));
        private static bool CheckCards(params string[] cardIds)
        {
            foreach (var cardId in cardIds)
            {
                if (CheckCard(cardId)) return true;
            }
            return false;
        }
        private static bool ShowCounter(string @class, DisplayMode mode, params string[] cardIds)
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
        public static bool ShowPlayerCounter(IEffectConfig config) =>
            ShowCounter(Core.Game.Player.Class, config.Player, config.ShowOnCardIds);
        public static bool ShowOpponentCounter(IEffectConfig config) =>
           ShowCounter(Core.Game.Opponent.Class, config.Opponent, config.ShowOnCardIds);

        public static bool MatchSpellSchool(Card card, int spellSchool)
        {
            if (card.Type == "Spell" && HearthDb.Cards.All.TryGetValue(card.Id, out HearthDb.Card dbCard))
            {
                return dbCard?.SpellSchool == spellSchool;
            }
            return false;
        }
    }
}
