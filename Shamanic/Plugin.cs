using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;
using Shamanic.Properties;
using Shamanic.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using static Shamanic.Views.EffectView;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace Shamanic
{
    public class Plugin : IPlugin
    {
        public string Name => LibraryInfo.Name;
        public string Description => Strings.Get("PluginDescription");
        public string ButtonText => Strings.Get("PluginButtonText");
        public string Author => "batstyx";
        public Version Version => LibraryInfo.Version;
        public MenuItem MenuItem => null;

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;

        private Shamanic Shamanic;

        public void OnLoad() => Shamanic = new Shamanic();

        public void OnUpdate() => Shamanic.Refresh();

        public void OnUnload()
        {
            if (Settings.Default.HasChanges) Settings.Default.Save();

            Shamanic?.Dispose();
            Shamanic = null;
        }

        
    }
}
