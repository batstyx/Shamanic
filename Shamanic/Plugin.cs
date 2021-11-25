using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;
using Shamanic.Properties;
using Shamanic.Views;
using System;
using System.Windows.Controls;

namespace Shamanic
{
    public class Plugin : IPlugin
    {
        private Settings Settings;
        internal static EventManager Events { get; private set; }

        public string Name => LibraryInfo.Name;
        public string Description => Strings.Get("PluginDescription");
        public string ButtonText => Strings.Get("PluginButtonText");
        public string Author => "batstyx";
        public Version Version => LibraryInfo.Version;
        public MenuItem MenuItem { private set; get; }

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;

        private Shamanic Shamanic;

        public void OnLoad()
        {
            Settings = Settings.Default;

            MenuItem = new MenuItem { Header = Name };
            MenuItem.Click += (sender, args) => OnButtonPress();

            Events = new EventManager();

            GameEvents.OnGameStart.Add(Events.OnGameStart);
            GameEvents.OnInMenu.Add(Events.OnInMenu);

            GameEvents.OnPlayerPlay.Add(Events.OnPlayerPlay);
            GameEvents.OnPlayerCreateInPlay.Add(Events.OnPlayerCreateInPlay);

            GameEvents.OnOpponentPlay.Add(Events.OnOpponentPlay);
            GameEvents.OnOpponentCreateInPlay.Add(Events.OnOpponentCreateInPlay);

            Shamanic = new Shamanic();
        }

        public void OnUpdate() => Shamanic.Refresh();

        public void OnUnload()
        {
            if (Settings?.HasChanges ?? false) Settings.Save();

            Events?.Dispose();
            Events = null;

            Shamanic?.Dispose();
            Shamanic = null;
        }

        
    }
}
