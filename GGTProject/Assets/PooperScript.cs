using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooperScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(killMe());
    }

    IEnumerator killMe()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
