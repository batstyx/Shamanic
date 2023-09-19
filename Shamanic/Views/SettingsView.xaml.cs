using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Utility;
using MahApps.Metro.Controls;
using Shamanic.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

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

        public IEnumerable<DisplayMode> PlayerCounterDisplayModes => DisplayModes;
        public IEnumerable<DisplayMode> OpponentCounterDisplayModes => DisplayModes.Except(new[] { DisplayMode.Card });

        public class GroupHeader
        {
            public string Title { get; set; }
            public ICommand Command { get; set; }
        }
        public GroupHeader PlayerPositionGroup { get; } = new GroupHeader
        {
            Title = Strings.Get(nameof(Properties.Resources.SettingsPositionTitle)),
            Command = new Command(Settings.Default.ResetPlayerPosition)
        };
        public GroupHeader OpponentPositionGroup { get; } = new GroupHeader
        {
            Title = Strings.Get(nameof(Properties.Resources.SettingsPositionTitle)),
            Command = new Command(Settings.Default.ResetOpponentPosition)
        };
        public GroupHeader PlayerDisplayGroup { get; } = new GroupHeader
        {
            Title = Strings.Get(nameof(Properties.Resources.SettingsDisplayTitle)),
            Command = new Command(Settings.Default.ResetPlayerDisplay)
        };
        public GroupHeader OpponentDisplayGroup { get; } = new GroupHeader
        {
            Title = Strings.Get(nameof(Properties.Resources.SettingsDisplayTitle)),
            Command = new Command(Settings.Default.ResetOpponentDisplay)
        };



        public SettingsView()
        {
            InitializeComponent();
        }
    }
}
