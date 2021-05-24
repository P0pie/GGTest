using System;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

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
            UIUpdate(BetText, "", money.GetBet());
        }

        public void DecreaseBet()
        {
            money.ChangeBet(false);
            UIUpdate(BetText, "", money.GetBet());
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
            UIUpdate(BalanceText, "Balance: ", money.GetBalance());

            //Reset displayed Winnings
            displayedWinnings = 0f;
            UIUpdate(WinningsText, "Winnings: ", displayedWinnings);

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
                UIUpdate(WinningsText, "Winnings: ", displayedWinnings);
            }
            else
                OnPooper();
        }

        void RoundEnd()
        {
            Debug.Log("Round End");
            money.AddFunds(money.GetWinnings());
            UIUpdate(BalanceText, "Balance: ", money.GetBalance());
            WinningsText.text = "Previous " + WinningsText.text;

            BetText.transform.position += new Vector3(0, 175, 0);
            UI2Hide.SetActive(true);
            Chests.SetActive(false);
        }

        void UIUpdate(TextMeshProUGUI destination, string Prefix, float newNum)
        {
            DOVirtual.Float(pullFloatfromString(destination.text), newNum, 1f, (v) => destination.text = v.ToString("c2"));
        }

        float pullFloatfromString(string s)
        {
            string newString = "";
            foreach (char c in s)
            {
                if (Char.IsNumber(c) || c == '.')
                {
                    newString += c;
                }
            }
            return float.Parse(newString);
        }

    }
}