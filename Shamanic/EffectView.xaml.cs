using Hearthstone_Deck_Tracker.Annotations;
using Hearthstone_Deck_Tracker.API;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Shamanic
{
    public partial class EffectView : UserControl, INotifyPropertyChanged
    {
        private bool _forceShow;

        private CounterStyles _CounterStyle;

        public EffectView()
        {
            InitializeComponent();
        }

        public void SetLocation(int PercentFromTop, int PercentFromRight)
        {
            Canvas.SetTop(this, Core.OverlayWindow.Height * PercentFromTop / 100);
            Canvas.SetRight(this, Core.OverlayWindow.Width * PercentFromRight / 100);
        }

        private Effect _totemEffect = null;
        public Effect TotemEffect
        {
            get { return _totemEffect; }
            set
            {
                if (value == _totemEffect)
                    return;
                _totemEffect = value;
                OnPropertyChanged();
            }
        }

        private Effect _overloadEffect = null;
        public Effect OverloadEffect
        {
            get { return _overloadEffect; }
            set
            {
                if (value == _overloadEffect)
                    return;
                _overloadEffect = value;
                OnPropertyChanged();
            }
        }

        public CounterStyles CounterStyle
        {
            get { return _CounterStyle; }
            set
            {
                if (value == _CounterStyle)
                    return;
                _CounterStyle = value;
                OnPropertyChanged(nameof(OverloadVisibility));
                OnPropertyChanged(nameof(TotemsVisibility));
                OnPropertyChanged(nameof(SomeVisibility));
            }
        }

        public Visibility OverloadVisibility => _forceShow || (CounterStyle & CounterStyles.Overload) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility TotemsVisibility => _forceShow || (CounterStyle & CounterStyles.Totems) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SomeVisibility => OverloadVisibility == Visibility.Visible || TotemsVisibility == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public void ForceShow(bool force)
        {
            _forceShow = force;
            OnPropertyChanged(nameof(OverloadVisibility));
            OnPropertyChanged(nameof(TotemsVisibility));
            OnPropertyChanged(nameof(SomeVisibility));
        }

        public enum CounterStyles
        {
            None,
            Overload,
            Totems,
            Full
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
