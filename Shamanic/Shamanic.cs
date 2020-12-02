using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Shamanic.Properties;
using Shamanic.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            GameEvents.OnGameStart.Add(GameStart);
            GameEvents.OnInMenu.Add(InMenu);
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
            view.OverloadEffect = tracker.Overload;
            view.TotemEffect = tracker.Totems;

            GameEvents.OnGameStart.Add(tracker.GameStart);
            
            return tracker;
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

            var showOverloadCounter = Helper.ShowOverloadCounter;
            var showTotemsCounter = Helper.ShowTotemsCounter;

            PlayerView.SetLocation(Settings.Default.PlayerTop, 100 - Settings.Default.PlayerLeft);
            PlayerView.CounterStyle = showOverloadCounter && showTotemsCounter ? CounterStyles.Full : (showOverloadCounter ? CounterStyles.Overload : (showTotemsCounter ? CounterStyles.Totems : CounterStyles.None));

            var showOpponentCounters = Helper.ShowOpponentCounters;

            OpponentView.SetLocation(Settings.Default.OpponentTop, 100 - Settings.Default.OpponentLeft);
            OpponentView.CounterStyle = showOpponentCounters ? CounterStyles.Full : CounterStyles.None;

        }

        public void Dispose()
        {
            CoreAPI.OverlayCanvas.Children.Remove(OpponentView);
            CoreAPI.OverlayCanvas.Children.Remove(PlayerView);
        }
    }
}
