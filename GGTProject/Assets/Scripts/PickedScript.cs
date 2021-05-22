using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickBonus
{
    public class PickedScript : MonoBehaviour
    {
        public void Picked()
        {
            GameManager.Instance.OpenChest();
            this.gameObject.SetActive(false);
        }
    }
}