using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamanic
{
    public interface IEffectConfig
    {
        string Name { get; }
        string[] Cards { get; }
        DisplayMode Player { get; }
        DisplayMode Opponent { get; }
        Predicate<Card> Condition { get; }
        Func<Card, int> Increment { get; }
    }
}
