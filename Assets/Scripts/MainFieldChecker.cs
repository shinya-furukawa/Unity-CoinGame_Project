using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFieldChecker : MonoBehaviour
{
    private Transform coinField;

    private void Start()
    {
        coinField = GameObject.Find("CoinField").transform;
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Coin" || other.gameObject.tag == "10Coin")
        {
            other.transform.SetParent(coinField);
        }
    }
}
