using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickBonus{
    public class GameManager : MonoBehaviour
    {
        //Set up singleton pattern
        //-----------------------------------------------------------------------------
        private static GameManager _instance;

        public static GameManager Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        //-----------------------------------------------------------------------------

        //Money Manager keeps track of finances
        MoneyManager money = new MoneyManager();

        public event Action OnAddBet;
        public event Action OnReduceBet;
        public event Action OnPlay;
        public event Action OnPick;
    }
}