using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using MahApps.Metro.Controls;
using Shamanic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Shamanic.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : ScrollViewer
    {
        private static Flyout _Flyout;
        public static Flyout Flyout => _Flyout ?? (_Flyout = CreateSettingsFlyout());
        private static Flyout CreateSettingsFlyout()
        {
            var settings = new Flyout
            {
                Position = Position.Left,
                Header = Strings.Get("SettingsTitle"),
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
        }

        private void ButtonResetPlayerPosition_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Settings.Default.ResetPlayerPosition();
        }

        private void ButtonResetOpponentPosition_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Settings.Default.ResetOpponentPosition();
        }
    }
}
