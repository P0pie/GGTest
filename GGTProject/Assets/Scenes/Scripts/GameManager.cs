using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    MoneyManager money = new MoneyManager();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            float m = 0;
            float n = 0;
            for (int i = 0; i < 100000; i++)
            {
                m += money.GetNewMultiplier();
                n += 1;
        }
            Debug.Log(m / n);
        }
    }
}
