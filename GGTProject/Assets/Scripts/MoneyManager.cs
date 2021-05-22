using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickBonus
{
    public class MoneyManager
    {
        //Preferably everything done in this class should be done server-side.
        //Also, preferably everything done here is taken care of by a Cyber Security specialist
        //Since the numerical values in use here are small, I'm just going to use floats.
        float _balance = 10.00f, _winnings = 0.00f;
        float[] GeneratedWinnings;
        int _bet = 1;
        
        //Inputs _bet to an enum to output actual desired bet float.
        public float GetBet()
        {
            switch (_bet)
            {
                case 0:
                    return 0.25f;
                case 1:
                    return 0.50f;
                case 2:
                    return 1.00f;
                default:
                    return 5.00f;
            }
        }

        //returns _balance
        public float GetBalance()
        {
            return _balance;
        }

        //returns _winnings
        public float GetWinnings()
        {
            return _winnings;
        }

        //Increases _balance by input float
        public void AddFunds(float funds)
        {
            _balance += funds;
        }

        //Returns true if successfully places Bet. Also sets new winnings.
        public bool PlaceBet()
        {
            if (GetBalance() >= GetBet())
            {
                _balance -= GetBet();
                _winnings = GetBet() * GetNewMultiplier();
                winningsIndex = 0;
                separatedWinnings = SeparateWinnings(_winnings);
                return true;
            }
            else
                return false;
        }

        //Takes argument true to increase bet, false to reduce bet.
        public void ChangeBet(bool isAdd)
        {
            if (isAdd && (_bet < 3))
            {
                _bet++;
            }
            else if (!isAdd && (_bet > 0))
            {
                _bet--;
            }
            else
            {
                Debug.Log("Error: Attempt to change bet to invalid value.");
            }
        }



        //Calculates win multiplier
        private int GetNewMultiplier()
        {
            float f = GetSecureNumberRange(0, 100);

            if (f <= 50)
            {
                return 0; //bust
            }
            else if (f <= 80)
            {
                return GetLowRange();
                //inclusive x, exclusive y
            }
            else if (f <= 95)
                return GetMidRange();
            else
                return GetHighRange();


            //Right now it's quick and dirty to get the values, but it can easily be fixed up and become scalable... Maybe I should fix up how the percents are calculated too... We'll cross that bridge later.

            float GetSecureNumberRange(float x, float y)
            {
                /*
                 * Should this calculation be done using such an insecure method? No.
                 * Am I going to use this method anyways; for the sake of a demo? Absolutely. 
                 */
                return Random.Range(x, y);
                //inclusive x, inclusive y
            }

            //1x-10x
            int GetLowRange()
            {
                return Random.Range((int)1, (int)11);
                //inclusive x, exclusive y
            }

            //12x, 16x, 24x, 32x, 48x, 64x
            int GetMidRange()
            {
                int i = Random.Range((int)0, (int)6);
                //inclusive x, exclusive y
                switch (i)
                {
                    case 0:
                        return 12;
                    case 1:
                        return 16;
                    case 2:
                        return 24;
                    case 3:
                        return 32;
                    case 4:
                        return 48;
                    default:
                        //i = 5
                        return 64;
                }
            }

            //100x - 500x
            int GetHighRange()
            {
                return Random.Range((int)1, (int)6) * 100;
                //inclusive x, exclusive y
            }
        }

        //Get Winnings Distribution
        private float[] SeparateWinnings(float totalWinnings)
        {
            float[] f = new float[8];
            int w = (int)totalWinnings * 4;
            float z = 0;

            for (int i = 0; i<8; i++)
            {
                if (w > 0 && i!=7)
                {
                    int y = Random.Range(1, w + 1);
                    f[i] = (float)y / 4;
                    w -= y;
                }
                else if (w > 0)
                {
                    f[i] = (float)w / 4;
                    w = 0;
                }
                else
                {
                    f[i] = 0;
                }
                z += f[i];
            }
            return f;
        }

        int winningsIndex = 0;
        float[] separatedWinnings;

        public float NextWin()
        {
            winningsIndex++;
            return separatedWinnings[winningsIndex - 1];
        }
    }
}