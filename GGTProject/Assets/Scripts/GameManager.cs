using System;
using UnityEngine;
using TMPro;

namespace PickBonus
{
    public class GameManager : MonoBehaviour
    {
        //Set up singleton
        //-----------------------------------------------------------------------------
        private static GameManager _instance;
        public static GameManager Instance {get { return _instance; }}

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                _instance = this;
            }
        }
        //-----------------------------------------------------------------------------

        //Money Manager keeps track of finances
        MoneyManager money = new MoneyManager();

        //Chest object to spawn
        public GameObject Chest;

        //Numerical UI elements
        public TextMeshProUGUI BetText, BalanceText, WinningsText;
        float displayedWinnings;


        public void IncreaseBet()
        {
            money.ChangeBet(true);
            BetText.text = money.GetBet().ToString("c2");
        }

        public void DecreaseBet()
        {
            money.ChangeBet(false);
            BetText.text = money.GetBet().ToString("c2");
        }

        //Events
        public event Action OnPlay;
        public event Action OnPooper;

        private void Start()
        {
            OnPlay += RoundSetup;
            OnPooper += RoundEnd;
        }

        //Triggered by Play Button
        public void PlayGame()
        {
            if (money.PlaceBet())
            {
                OnPlay();
            }
        }

        public GameObject UI2Hide, Chests;
        void RoundSetup()
        {
            //Update displayed balance
            BalanceText.text = "Balance: " + money.GetBalance().ToString("c2");

            //Reset displayed Winnings
            displayedWinnings = 0f;
            WinningsText.text = "Winnings: " + displayedWinnings.ToString("c2");

            BetText.transform.position += new Vector3(0, -175 ,0);
            UI2Hide.SetActive(false);
            Chests.SetActive(true);
            for (int i = 0; i<Chests.transform.childCount; i++)
            {
                Chests.transform.GetChild(i).gameObject.SetActive(true);
            }
            //Move Bet value to clear up screenspace. Spawn chests.
        }

        public void OpenChest()
        {
            float f = money.NextWin();
            if (f != 0)
            {
                displayedWinnings += f;
                WinningsText.text = "Winnings: " + displayedWinnings.ToString("c2");
            }
            else
                OnPooper();
        }

        void RoundEnd()
        {
            Debug.Log("Round End");
            money.AddFunds(money.GetWinnings());
            BalanceText.text = "Balance: " + money.GetBalance().ToString("c2");

            BetText.transform.position += new Vector3(0, 175, 0);
            UI2Hide.SetActive(true);
            Chests.SetActive(false);
        }
    }
}