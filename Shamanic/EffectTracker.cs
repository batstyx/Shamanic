using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Shamanic.Properties;
using System;

namespace Shamanic
{
    internal class EffectTracker
    {        
        public Effect OverloadTotal { get; } = new Effect(Strings.Get("OverloadTotalEffectName"));
        public Effect OverloadPlayed { get; } = new Effect(Strings.Get("OverloadPlayedEffectName"));
        public Effect TotemsPlayed { get; } = new Effect(Strings.Get("TotemsPlayedEffectName"));

        private bool IncrementOverload(Card card) => card.Overload > 0;
        private bool IncrementTotems(Card card) => card.Type == "Minion" && (card.Race == "Totem" || card.Race == "All");

        internal void GameStart()
        {
            try
            {
#if DEBUG
                Log.Info("Shamanic GameStart");
#endif
                OverloadTotal.Reset();
                OverloadPlayed.Reset();
                TotemsPlayed.Reset();
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
#if DEBUG
                Log.Info($"Shamanic Play Card: {card?.Type}+{card?.Race}+{card?.Overload}"); 
#endif

                if (IncrementTotems(card))
                    TotemsPlayed.Increment();

                if (IncrementOverload(card))
                {
                    OverloadPlayed.Increment();
                    OverloadTotal.Increment(card.Overload);
                }
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
#if DEBUG
                Log.Info($"Shamanic CreateInPlay Card: {card?.Type}+{card?.Race}+{card?.Overload}");
#endif

                if (IncrementTotems(card))
                    TotemsPlayed.Increment();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }

}
