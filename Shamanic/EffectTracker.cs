using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Shamanic.Effects;
using Shamanic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shamanic
{
    internal class EffectTracker
    {        
        public IEnumerable<Effect> Effects { get; private set; }
        private IEnumerable<Effect> PlayList => Effects.Where(e => e.Config.IncrementOn.HasFlag(IncrementOn.Play));
        private IEnumerable<Effect> CreateInPlayList => Effects.Where(e => e.Config.IncrementOn.HasFlag(IncrementOn.CreateInPlay));

        public EffectTracker()
        {
            Effects = new List<Effect>
            {
                new Effect(new OverloadTotal()),
                new Effect(new OverloadPlayed()),
                new Effect(new TotemsPlayed()),
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
