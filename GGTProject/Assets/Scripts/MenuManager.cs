using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PickBonus
{
    public class MenuManager : MonoBehaviour
    {
        public void pressedPlay()
        {
            Debug.Log("Loading scene 1");
            SceneManager.LoadScene(1);
        }

        public void pressedQuit()
        {
            Debug.Log("Exiting application...");
            Application.Quit();
        }
    }
}