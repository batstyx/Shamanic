using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using MahApps.Metro.Controls;
using Shamanic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Shamanic
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : ScrollViewer
    {
        private static Flyout _flyout;
        public static Flyout Flyout
        {
            get
            {
                if (_flyout == null)
                {
                    _flyout = CreateSettingsFlyout();
                }
                return _flyout;
            }
        }

        private static Flyout CreateSettingsFlyout()
        {
            var settings = new Flyout
            {
                Position = Position.Left,
                Header = Properties.Resources.SettingsTitle,
                Content = new SettingsView()
            };
            Panel.SetZIndex(settings, 100);
            Core.MainWindow.Flyouts.Items.Add(settings);
            return settings;
        }

        public static IEnumerable<DisplayMode> DisplayModes = Enum.GetValues(typeof(DisplayMode)).Cast<DisplayMode>();

        public IEnumerable<DisplayMode> TotemsCounterDisplayModes = DisplayModes;
        public IEnumerable<DisplayMode> OverloadCounterDisplayModes = DisplayModes;
        public IEnumerable<DisplayMode> OpponentCounterDisplayModes = new[] { DisplayMode.Always, DisplayMode.Shaman, DisplayMode.Class, DisplayMode.Never };

        public SettingsView()
        {
            InitializeComponent();

            ComboBoxTotems.ItemsSource = TotemsCounterDisplayModes;
            ComboBoxOverload.ItemsSource = OverloadCounterDisplayModes;
            ComboBoxOpponent.ItemsSource = OpponentCounterDisplayModes;

            Settings.Default.PropertyChanged += (sender, e) => {
                Settings.Default.Save();
                };
        }
    }
}
