using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;
using Shamanic.Properties;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using static Shamanic.EffectView;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace Shamanic
{
    public class ShamanicPlugin : IPlugin
    {
        private EffectView _View;
        private EffectView _OpponentView;

        public string Name => AssemblyName.Name;
        public string Description => Strings.Get("PluginDescription");
        public string ButtonText => Strings.Get("PluginButtonText");
        public string Author => "batstyx";

        public static readonly AssemblyName AssemblyName = Assembly.GetExecutingAssembly().GetName();
        public static readonly Version AssemblyVersion = AssemblyName.Version;
        public static readonly Version PluginVersion = new Version(AssemblyVersion.Major, AssemblyVersion.Minor, AssemblyVersion.Build);
        public Version Version => PluginVersion;
        public MenuItem MenuItem => null;

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;

        public void OnLoad()
        {
            Debug.WriteLine("Shamanic IPlugin.OnLoad");

            _View = new EffectView();
            CoreAPI.OverlayCanvas.Children.Add(_View);
            
            EffectTracker tracker = new EffectTracker();
            _View.OverloadEffect = tracker.Overload;
            _View.TotemEffect = tracker.Totems;
            
            GameEvents.OnGameStart.Add(tracker.GameStart);            
            GameEvents.OnPlayerPlay.Add(tracker.Play);
            GameEvents.OnPlayerCreateInPlay.Add(tracker.CreateInPlay);

            _OpponentView = new EffectView();
            CoreAPI.OverlayCanvas.Children.Add(_OpponentView);

            EffectTracker opponentTracker = new EffectTracker();
            _OpponentView.OverloadEffect = opponentTracker.Overload;
            _OpponentView.TotemEffect = opponentTracker.Totems;

            GameEvents.OnGameStart.Add(opponentTracker.GameStart);
            GameEvents.OnOpponentPlay.Add(opponentTracker.Play);
            GameEvents.OnOpponentCreateInPlay.Add(opponentTracker.CreateInPlay);

            GameEvents.OnGameStart.Add(this.GameStart);
            GameEvents.OnInMenu.Add(this.InMenu);
        }

        public void OnUpdate()
        {
            if (!(_View.Visibility == Visibility.Visible || _OpponentView.Visibility == Visibility.Visible)) return;

            if (CoreAPI.Game.IsInMenu & Config.Instance.HideInMenu) return;

            var showOverloadCounter = Helper.ShowOverloadCounter;
            var showTotemsCounter = Helper.ShowTotemsCounter;
            
            _View.SetLocation(Settings.Default.PlayerTop, 100 - Settings.Default.PlayerLeft);
            _View.CounterStyle = showOverloadCounter && showTotemsCounter ? CounterStyles.Full : (showOverloadCounter ? CounterStyles.Overload : (showTotemsCounter ? CounterStyles.Totems : CounterStyles.None));

            var showOpponentCounters = Helper.ShowOpponentCounters;

            _OpponentView.SetLocation(Settings.Default.OpponentTop, 100 - Settings.Default.OpponentLeft);
            _OpponentView.CounterStyle = showOpponentCounters ? CounterStyles.Full : CounterStyles.None;
        }

        public void OnUnload()
        {
            Debug.WriteLine("Shamanic IPlugin.OnUnload");
            if (Settings.Default.HasChanges) Settings.Default.Save();

            CoreAPI.OverlayCanvas.Children.Remove(_OpponentView);
            CoreAPI.OverlayCanvas.Children.Remove(_View);            
        }

        internal void GameStart()
        {
            Debug.WriteLine("Shamanic GameStart");
            _View.Visibility = Visibility.Visible;
            _OpponentView.Visibility = Visibility.Visible;
        }

        internal void InMenu()
        {
            Debug.WriteLine("Shamanic InMenu");
            _View.Visibility = Config.Instance.HideInMenu ? Visibility.Hidden : Visibility.Visible;
            _OpponentView.Visibility = Config.Instance.HideInMenu ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
