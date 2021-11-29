using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Shamanic.Properties;
using Shamanic.Views;
using System;
using System.Windows;
using static Shamanic.Views.EffectView;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace Shamanic
{
    public class Shamanic : IDisposable
    {
        private readonly EffectView PlayerView;
        private readonly EffectView OpponentView;
        
        public Shamanic()
        {
            PlayerView = CreateView();
            TrackPlayer(CreateTracker(PlayerView));

            OpponentView = CreateView();
            TrackOpponent(CreateTracker(OpponentView));

            Plugin.Events.GameStart += GameStart;
            Plugin.Events.InMenu += InMenu;
        }

        private static EffectView CreateView()
        {
            var view = new EffectView();
            CoreAPI.OverlayCanvas.Children.Add(view);
            return view;
        }
                
        private static EffectTracker CreateTracker(EffectView view)
        {
            var tracker = new EffectTracker();
            view.OverloadTotalEffect = tracker.OverloadTotal;
            view.OverloadPlayedEffect = tracker.OverloadPlayed;
            view.TotemsPlayedEffect = tracker.TotemsPlayed;

            GameEvents.OnGameStart.Add(tracker.GameStart);
            
            return tracker;
        }

        private static void TrackOpponent(EffectTracker tracker)
        {
            Plugin.Events.OpponentPlay += tracker.Play;
            Plugin.Events.OpponentCreateInPlay += tracker.CreateInPlay;
        }

        private static void TrackPlayer(EffectTracker tracker)
        {
            Plugin.Events.PlayerPlay += tracker.Play;
            Plugin.Events.PlayerCreateInPlay += tracker.CreateInPlay;
        }

        internal void GameStart()
        {
            PlayerView.Visibility = Visibility.Visible;
            OpponentView.Visibility = Visibility.Visible;
        }

        internal void InMenu()
        {
            PlayerView.Visibility = Config.Instance.HideInMenu ? Visibility.Hidden : Visibility.Visible;
            OpponentView.Visibility = Config.Instance.HideInMenu ? Visibility.Hidden : Visibility.Visible;
        }

        internal void Refresh()
        {
            if (!(PlayerView.Visibility == Visibility.Visible || OpponentView.Visibility == Visibility.Visible)) return;

            if (CoreAPI.Game.IsInMenu & Config.Instance.HideInMenu) return;

            PlayerView.SetLocation(Settings.Default.PlayerTop, 100 - Settings.Default.PlayerLeft);

            PlayerView.OverloadPlayedEffect.Active = Helper.ShowOverloadPlayedCounter;
            PlayerView.OverloadTotalEffect.Active = Helper.ShowOverloadTotalCounter;
            PlayerView.TotemsPlayedEffect.Active = Helper.ShowTotemsPlayedCounter;
            PlayerView.RefreshVisibility();

            OpponentView.SetLocation(Settings.Default.OpponentTop, 100 - Settings.Default.OpponentLeft);

            var showOpponentCounters = Helper.ShowOpponentCounters;
            OpponentView.OverloadPlayedEffect.Active = showOpponentCounters;
            OpponentView.OverloadTotalEffect.Active = showOpponentCounters;
            OpponentView.TotemsPlayedEffect.Active = showOpponentCounters;
            OpponentView.RefreshVisibility();
        }

        public void Dispose()
        {
            CoreAPI.OverlayCanvas.Children.Remove(OpponentView);
            CoreAPI.OverlayCanvas.Children.Remove(PlayerView);
        }
    }
}
