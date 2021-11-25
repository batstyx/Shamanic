using Hearthstone_Deck_Tracker.Annotations;
using Hearthstone_Deck_Tracker.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Shamanic.Views
{
    public partial class EffectView : UserControl, INotifyPropertyChanged
    {
        private bool _ForceShow;

        public EffectView()
        {
            InitializeComponent();
        }

        public void SetLocation(int PercentFromTop, int PercentFromRight)
        {
            Canvas.SetTop(this, Core.OverlayWindow.Height * PercentFromTop / 100);
            Canvas.SetRight(this, Core.OverlayWindow.Width * PercentFromRight / 100);
        }

        private Effect _TotemsPlayedEffect = null;
        public Effect TotemsPlayedEffect
        {
            get => _TotemsPlayedEffect;
            set => SetProperty(ref _TotemsPlayedEffect, value);
        }

        private Effect _OverloadPlayedEffect = null;
        public Effect OverloadPlayedEffect
        {
            get => _OverloadPlayedEffect;
            set => SetProperty(ref _OverloadPlayedEffect, value);
        }

        private Effect _OverloadTotalEffect = null;
        public Effect OverloadTotalEffect
        {
            get => _OverloadTotalEffect;
            set => SetProperty(ref _OverloadTotalEffect, value);
        }

        private CounterStyles _CounterStyle;
        public CounterStyles CounterStyle
        {
            get => _CounterStyle;
            set => SetProperty(ref _CounterStyle, value, callback: RefreshVisibility);
        }

        public Visibility OverloadPlayedVisibility => _ForceShow || (CounterStyle & CounterStyles.OverloadPlayed) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility OverloadTotalVisibility => _ForceShow || (CounterStyle & CounterStyles.OverloadTotal) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility TotemsVisibility => _ForceShow || (CounterStyle & CounterStyles.Totems) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SomeVisibility => OverloadPlayedVisibility == Visibility.Visible
            | OverloadTotalVisibility == Visibility.Visible
            | TotemsVisibility == Visibility.Visible 
            ? Visibility.Visible : Visibility.Collapsed;

        private void RefreshVisibility()
        {
            OnPropertyChanged(nameof(OverloadPlayedVisibility));
            OnPropertyChanged(nameof(OverloadTotalVisibility));
            OnPropertyChanged(nameof(TotemsVisibility));
            OnPropertyChanged(nameof(SomeVisibility));
        }

        public void ForceShow(bool force)
        {
            _ForceShow = force;
            RefreshVisibility();
        }

        public enum CounterStyles
        {
            None,
            OverloadPlayed = 1,
            OverloadTotal = 2,
            Totems = 4,
            Full = 7
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
