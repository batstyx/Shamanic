using Hearthstone_Deck_Tracker.Annotations;
using Hearthstone_Deck_Tracker.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Shamanic.Views
{
    public partial class EffectView : UserControl, INotifyPropertyChanged
    {
        public EffectView()
        {
            InitializeComponent();
        }

        public void SetLocation(int PercentFromTop, int PercentFromRight)
        {
            Canvas.SetTop(this, Core.OverlayWindow.Height * PercentFromTop / 100);
            Canvas.SetRight(this, Core.OverlayWindow.Width * PercentFromRight / 100);
        }

        public Orientation Orientation { get => _Orientation; set => SetProperty(ref _Orientation, value); }
        private Orientation _Orientation;

        public ObservableCollection<Effect> Effects { get; } = new ObservableCollection<Effect>();

        public Visibility SomeVisibility { get => _SomeVisibility; private set => SetProperty(ref _SomeVisibility,value); }
        private Visibility _SomeVisibility = Visibility.Collapsed;

        public void RefreshVisibility()
        {
            var someVisibility = Visibility.Collapsed;
            foreach (var effect in Effects)
            {
                if (effect.Active)
                {
                    someVisibility = Visibility.Visible;
                    break;
                }
            }
            SomeVisibility = someVisibility;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action callback = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            callback?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        } 

        #endregion
    }
}
