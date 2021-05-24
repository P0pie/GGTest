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
        public event Action OnPick;
        public event Action OnPickEnd;
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
            OnPickEnd();
            //Move Bet value to clear up screenspace. Spawn chests.
        }

        public GameObject CoinPrefab, PooperPrefab;
        public Transform PickedChest, coinTarget;

        public void OpenChest()
        {
            OnPick();

            float f = money.NextWin();
            if (f != 0)
            {
                displayedWinnings += f;
                StartCoroutine(Open(true));

            }
            else
                StartCoroutine(Open(false)); //basically just calls RoundEnd
        }

        IEnumerator Open(bool isWin)
        {
            yield return new WaitForSeconds(1);
            GameObject g;
            if (isWin)
            {
                for (int i = 0; i < 10; i++)
                {
                 g = Instantiate(CoinPrefab, new Vector3((PickedChest.position.x + UnityEngine.Random.Range(-1f, 1f)), (PickedChest.position.y + UnityEngine.Random.Range(-1f, 1f)), PickedChest.position.z), PickedChest.rotation);
                Vector3[] v = new Vector3[2];
                v[0] = new Vector3(0, 5, -10);
                v[1] = coinTarget.position;
                g.transform.DOPath(v, 3, (PathType)1, (PathMode)1);
                }
                UIUpdate(WinningsText, "Winnings: ", displayedWinnings);
                OnPickEnd();
            }
            else
            {
                g = Instantiate(PooperPrefab, PickedChest.position, PickedChest.rotation);
                Vector3[] v = new Vector3[1];
                v[0] = cam.transform.position;
                g.transform.LookAt(cam.transform.position * -1);
                g.transform.DOPath(v, 3, (PathType)1, (PathMode)1);
                OnPooper();
            }

        }
       
        void RoundEnd()
        {
            StartCoroutine(GG());
            
        }

        IEnumerator GG()
        {
            yield return new WaitForSeconds(2);
            money.AddFunds(money.GetWinnings());
            UIUpdate(BalanceText, "Balance: ", money.GetBalance());
            WinningsText.text = "Previous Winnings: " + WinningsText.text;

            BetText.transform.position += new Vector3(0, 175, 0);
            UI2Hide.SetActive(true);

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

        public Camera cam;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Chest")
                    {
                        hit.transform.GetComponent<PickedScript>().Picked();
                    }

                }
            }
        }

    }
}