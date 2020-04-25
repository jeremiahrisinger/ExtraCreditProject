using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerController>().CollectCoin();
        //StartCoroutine(other.gameObject.GetComponent<PlayerController>().SlowDown());
        //Destroy(gameObject);
        gameObject.SetActive(false);
        Invoke("Activate",10f);
    }
    private void Activate()
    {
        gameObject.SetActive(true);
    }
}