using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    private void Awake()
    {
        sound.PlayDelayed(Random.Range(0f, .5f));
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(deathTimer());
    }

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
