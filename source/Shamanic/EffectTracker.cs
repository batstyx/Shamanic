using Hearthstone_Deck_Tracker.Hearthstone;
using System.Diagnostics;

namespace Shamanic
{
    internal class EffectTracker
    {
        //TODO: Localise Effect names
        public Effect Overload { get; } = new Effect("Overload");
        public Effect Totems { get; } = new Effect("Totems");

        private bool IncrementOverload(Card card) => card.Overload > 0;
        private bool IncrementTotems(Card card) => card.Type == "Minion" && card.Race == "Totem"; //TODO: Localise Card Type and Race?

        internal void GameStart()
        {
            Debug.WriteLine("Shamanic GameStart");
            Overload.Reset();
            Totems.Reset();
        }

        internal void Play(Card card)
        {
            Debug.WriteLine("Shamanic Play Card: {0}+{1}+{2}", card.Type, card.Race, card.Overload);

            if (IncrementTotems(card))
                Totems.Increment();

            if (IncrementOverload(card))
                Overload.Increment(card.Overload);
        }

        internal void CreateInPlay(Card card)
        {
            Debug.WriteLine("Shamanic CreateInPlay Card: {0}+{1}+{2}", card.Type, card.Race, card.Overload);

            if (IncrementTotems(card))
                Totems.Increment();
        }
    }

}
