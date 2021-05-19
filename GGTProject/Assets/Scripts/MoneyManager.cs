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
        float _balance = 0, _bet = 1, _winnings;

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

        public void AddFunds(float funds)
        {
            _balance += funds;
        }

        //Give true to increase bet, false to reduce bet.
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
        public int GetNewMultiplier()
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
    }
}