using Hearthstone_Deck_Tracker.Annotations;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Shamanic
{
    public class Effect : INotifyPropertyChanged
    {
        public string Name { get; }
        public int Count { get; private set; }

        public Effect(string name)
        {
            Name = name;
            Count = 0;
            Debug.WriteLine("Shamanic Effect Create {0}: {1}", Name, Count);
        }      
        
        public int Increment(int byAmount = 1)
        {
            Count += byAmount;
            Debug.WriteLine("Shamanic Effect Increment {0}: {1}", Name, Count);
            OnPropertyChanged(nameof(Count));
            return Count;
        }


        public void Reset()
        {
            Count = 0;
            Debug.WriteLine("Shamanic Effect Reset {0}: {1}", Name, Count);
            OnPropertyChanged(nameof(Count));
        }

        public bool HasCount { get { return Count > 0; } }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
