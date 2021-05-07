using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance;  }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    MoneyManager money = new MoneyManager();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){


        }
    }
}