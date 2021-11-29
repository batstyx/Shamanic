using Hearthstone_Deck_Tracker.Annotations;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shamanic
{
    public class Effect : INotifyPropertyChanged
    {
        public string Name { get; }
        public int Count { get => _Count; private set => SetProperty(ref _Count, value); }
        private int _Count;
        public bool Active { get => _Active; set => SetProperty(ref _Active, value); }
        private bool _Active;

        public Effect(string name)
        {
            Name = name;
            Count = 0;
            Log.Debug($"Shamanic Effect Create {Name}: {Count}");
        }      
        
        public int Increment(int byAmount = 1)
        {
            Log.Debug($"Shamanic Effect Increment {Name}: {Count}+{byAmount}");
            Count += byAmount;
            return Count;
        }


        public void Reset()
        {
            Count = 0;
            Log.Debug($"Shamanic Effect Reset {Name}: {Count}");
        }

        public bool HasCount { get { return Count > 0; } }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion
    }
}
