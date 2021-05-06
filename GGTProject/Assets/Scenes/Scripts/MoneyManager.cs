using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager
{
    //Preferably everything done in this class should be done server-side.
    //Also, preferably everything done here is taken care of by a Cyber Security specialist

    public int GetNewMultiplier()
    {
        float f = GetSecureNumberRange(0,100);

        if (f <= 50)
        {
            return 0;
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
    }

    float GetSecureNumberRange(float x, float y){
        /*
         * Should this calculation be done using such an insecure method? No.
         * Am I going to use this method anyways; for the sake of a demo? Absolutely. 
         */
        return Random.Range(x,y);
        //inclusive x, inclusive y
    }

    int GetLowRange()
    {
        return Random.Range((int)1, (int)10);
    }
    int GetMidRange()
    {
        int i = Random.Range((int)0, (int)5);
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

    int GetHighRange()
    {
        return Random.Range((int)1, (int)5) * 100;
    }
}