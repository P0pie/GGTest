using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickBonus
{
    public class PickedScript : MonoBehaviour
    {
        private Transform Lid, Hinge;
        public BoxCollider myCollider;
        bool rotate = false, open = false, closing = false;


        private void Start()
        {
            GameManager.Instance.OnPick += DisableCollider;
            GameManager.Instance.OnPickEnd += EnableCollider;
            Lid = transform.GetChild(0);
            Hinge = transform.GetChild(1);
        }

        private void OnEnable()
        {
            rotate = false;
            open = false;
            closing = false;
            EnableCollider();
        }

        private void Update()
        {
            if (rotate)
                StartCoroutine(LeanForward());
            if (open)
                StartCoroutine(OpenLid());
            if (closing)
                StartCoroutine(Close());
        }

        public void Picked()
        {
            rotate = true;
            GameManager.Instance.PickedChest = transform;
            GameManager.Instance.OpenChest();
            Close();
        }

        private IEnumerator LeanForward()
        {
            transform.Rotate(0, 0, (80 * Time.deltaTime));
            yield return new WaitForSeconds(.5f);
            rotate = false;
            open = true;
        }

        private IEnumerator OpenLid()
        {
            Lid.RotateAround(Hinge.position, Vector3.left, -80 * Time.deltaTime);
            yield return new WaitForSeconds(1);
            open = false;
            closing = true;
        }

        public IEnumerator Close()
        {
            
            yield return new WaitForSeconds(1);
            transform.localEulerAngles = new Vector3(0, -90, 0);
            Lid.localPosition = new Vector3(0, 1, 0);
            Lid.localEulerAngles = new Vector3(90, 0, 0);
            closing = false;
            gameObject.SetActive(false);
        }

        public void DisableCollider()
        {
            myCollider.enabled = false;
        }

        public void EnableCollider()
        {
            myCollider.enabled = true;
        }

    }
}