using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace Shamanic
{
    internal class EventManager
    {
        public delegate void GameEventHandler();
        public delegate void CardEventHandler(Card card);

        private bool ValidGameInProgress = false;

        public EventManager() { }

        public event GameEventHandler GameStart;
        public void OnGameStart()
        {
            if (CoreAPI.Game.CurrentGameMode == GameMode.Battlegrounds) return;

            ValidGameInProgress = true;

            GameEventHandler handler = GameStart;
            handler?.Invoke();
        }

        private void OnCardEventHandler(Card card, CardEventHandler handler)
        {
            if (card != null && ValidGameInProgress)
            {
                CardEventHandler localhandler = handler;
                localhandler?.Invoke(card);
            }
        }

        public event CardEventHandler PlayerPlay;
        public void OnPlayerPlay(Card card) => OnCardEventHandler(card, PlayerPlay);

        public event CardEventHandler PlayerCreateInPlay;
        public void OnPlayerCreateInPlay(Card card) => OnCardEventHandler(card, PlayerCreateInPlay);

        public event CardEventHandler OpponentPlay;
        public void OnOpponentPlay(Card card) => OnCardEventHandler(card, OpponentPlay);

        public event CardEventHandler OpponentCreateInPlay;
        public void OnOpponentCreateInPlay(Card card) => OnCardEventHandler(card, OpponentCreateInPlay);

        public event GameEventHandler InMenu;
        public void OnInMenu()
        {
            ValidGameInProgress = false;
            GameEventHandler handler = InMenu;
            handler?.Invoke();
        }

        public void Dispose()
        {
            GameStart = null;
            InMenu = null;

            PlayerPlay = null;
            PlayerCreateInPlay = null;

            OpponentPlay = null;
            OpponentCreateInPlay = null;
        }
    }
}
