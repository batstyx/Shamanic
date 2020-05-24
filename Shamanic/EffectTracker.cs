using Hearthstone_Deck_Tracker.Hearthstone;
using Shamanic.Properties;
using System.Diagnostics;

namespace Shamanic
{
    internal class EffectTracker
    {
        public Effect Overload { get; } = new Effect(Strings.Get("OverloadEffectName"));
        public Effect Totems { get; } = new Effect(Strings.Get("TotemsEffectName"));

        private bool IncrementOverload(Card card) => card.Overload > 0;
        private bool IncrementTotems(Card card) => card.Type == "Minion" && card.Race == "Totem" || card.Race == "All"; //TODO: Localise Card Type and Race?

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
