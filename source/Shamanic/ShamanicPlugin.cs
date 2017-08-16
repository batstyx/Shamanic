using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using static Shamanic.EffectView;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace Shamanic
{
    public class ShamanicPlugin : IPlugin
    {
        private EffectView _View;

        public string Name => "Shamanic";
        public string Description => @"For use with the Shaman cards 'Thing from Below' and 'Snowfury Giant'.
Displays total Mana Crystals Overloaded and/or Totems played.

Uses the 'Overlay Player->Show spell counter' option to determine display behaviour.

Always - Display Overload and Totems for all games (any class).
Auto - Display Overload for 'Snowfury Giant' and Totems for 'Thing from Below'.
Never - Do not display.

**WARNING** Disabling the plugin during a game will reset the counters.";
        public string ButtonText => "Test Button Please Ignore";
        public string Author => "batstyx";
        public Version Version => new Version(0, 1, 1);
        public MenuItem MenuItem => null;

        public void OnButtonPress()
        {
            Debug.WriteLine("Shamanic IPlugin.OnButtonPress");
        }

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

            GameEvents.OnGameStart.Add(this.GameStart);
            GameEvents.OnInMenu.Add(this.InMenu);
        }

        public void OnUpdate()
        {
            if (_View.Visibility != System.Windows.Visibility.Visible) return;

            var showOverloadCounter = Helper.ShowOverloadCounter;
            var showTotemsCounter = Helper.ShowTotemsCounter;

            _View.SetLocation(76, 18);
            _View.CounterStyle = showOverloadCounter && showTotemsCounter ? CounterStyles.Full : (showOverloadCounter ? CounterStyles.Overload : (showTotemsCounter ? CounterStyles.Totems : CounterStyles.None));
        }

        public void OnUnload()
        {
            Debug.WriteLine("Shamanic IPlugin.OnUnload");
            CoreAPI.OverlayCanvas.Children.Remove(_View);
        }

        internal void GameStart()
        {
            Debug.WriteLine("Shamanic GameStart");
            _View.Visibility = System.Windows.Visibility.Visible;
        }

        internal void InMenu()
        {
            Debug.WriteLine("Shamanic InMenu");
            if (Config.Instance.HideInMenu)
            {
                _View.Visibility = System.Windows.Visibility.Hidden;
            }
        }

    }
}
