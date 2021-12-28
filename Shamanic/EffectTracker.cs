using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Shamanic.Effects;
using Shamanic.Properties;
using System;

namespace Shamanic
{
    internal class EffectTracker
    {        
        public Effect OverloadTotal { get; } = new Effect(new OverloadTotal());
        public Effect OverloadPlayed { get; } = new Effect(new OverloadPlayed());
        public Effect TotemsPlayed { get; } = new Effect(new TotemsPlayed());

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

                if (TotemsPlayed.Config.Condition(card))
                    TotemsPlayed.Increment(TotemsPlayed.Config.Increment(card));

                if (OverloadPlayed.Config.Condition(card))
                {
                    OverloadPlayed.Increment(OverloadPlayed.Config.Increment(card));
                    OverloadTotal.Increment(OverloadTotal.Config.Increment(card));
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

                if (TotemsPlayed.Config.Condition(card))
                    TotemsPlayed.Increment(TotemsPlayed.Config.Increment(card));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }

}
