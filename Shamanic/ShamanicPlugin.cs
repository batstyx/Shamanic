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

        public string Name => LibraryInfo.Name;
        public string Description => Strings.Get("PluginDescription");
        public string ButtonText => Strings.Get("PluginButtonText");
        public string Author => "batstyx";
        public Version Version => LibraryInfo.Version;
        public MenuItem MenuItem => null;

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;

        public void OnLoad()
        {
            Debug.WriteLine("Shamanic IPlugin.OnLoad");
            
            _View = CreateView();
            CreateTracker(_View);

            _OpponentView = CreateView();
            CreateTracker(_OpponentView, false);

            GameEvents.OnGameStart.Add(this.GameStart);
            GameEvents.OnInMenu.Add(this.InMenu);
        }

        private EffectView CreateView()
        {
            var view = new EffectView();
            CoreAPI.OverlayCanvas.Children.Add(view);
            return view;
        }

        private static void TrackOpponent(EffectTracker tracker)
        {
            GameEvents.OnOpponentPlay.Add(tracker.Play);
            GameEvents.OnOpponentCreateInPlay.Add(tracker.CreateInPlay);
        }

        private static void TrackPlayer(EffectTracker tracker)
        {
            GameEvents.OnPlayerPlay.Add(tracker.Play);
            GameEvents.OnPlayerCreateInPlay.Add(tracker.CreateInPlay);
        }

        private EffectTracker CreateTracker(EffectView view, bool trackPlayer = true)
        {
            var tracker = new EffectTracker();
            view.OverloadEffect = tracker.Overload;
            view.TotemEffect = tracker.Totems;
            
            GameEvents.OnGameStart.Add(tracker.GameStart);
            if (trackPlayer)
                TrackPlayer(tracker);
            else
                TrackOpponent(tracker);

            return tracker;
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
