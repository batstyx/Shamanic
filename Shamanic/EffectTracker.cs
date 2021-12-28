using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Shamanic.Effects;
using Shamanic.Properties;
using System;
using System.Collections.Generic;

namespace Shamanic
{
    internal class EffectTracker
    {        
        public IEnumerable<Effect> Effects { get; private set; }

        private IEnumerable<Effect> PlayList => Effects;
        private IEnumerable<Effect> CreateInPlayList;

        private Effect OverloadTotal { get; } = new Effect(new OverloadTotal());
        private Effect OverloadPlayed { get; } = new Effect(new OverloadPlayed());
        private Effect TotemsPlayed { get; } = new Effect(new TotemsPlayed());

        public EffectTracker()
        {
            Effects = new List<Effect>
            {
                OverloadTotal,
                OverloadPlayed,
                TotemsPlayed,
            };

            CreateInPlayList = new List<Effect>()
            {
                TotemsPlayed,
            };
        }

        internal void GameStart()
        {
            try
            {
#if DEBUG
                Log.Info("Shamanic GameStart");
#endif
                foreach (var effect in Effects)
                {
                    effect.Reset();
                }
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
                IncrementEffects(PlayList, card);
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
                IncrementEffects(CreateInPlayList, card);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        internal void IncrementEffects(IEnumerable<Effect> effects, Card card)
        {
            foreach (var effect in effects)
            {
                if (effect.Config.Condition(card))
                {
                    effect.Increment(effect.Config.Increment(card));
                }
            }
        }
    }

}
