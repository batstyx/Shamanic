using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Shamanic.Properties;
using System;
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
            try
            {
                Log.Info("Shamanic GameStart");
                Overload.Reset();
                Totems.Reset();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            
        }

        internal void Play(Card card)
        {
            try
            {
                Log.Info($"Shamanic Play Card: {card.Type}+{card.Race}+{card.Overload}");

                if (IncrementTotems(card))
                    Totems.Increment();

                if (IncrementOverload(card))
                    Overload.Increment(card.Overload);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        internal void CreateInPlay(Card card)
        {
            try
            {
                Log.Info($"Shamanic CreateInPlay Card: {card.Type}+{card.Race}+{card.Overload}");

                if (IncrementTotems(card))
                    Totems.Increment();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }

}
