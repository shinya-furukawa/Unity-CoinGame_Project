using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStopperChecker : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private int count = 0;
    private BoxCollider col;

    private void Start() {
        col = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Ball" && col.isTrigger)
        {
            count++;
            gameManager.ballStockCount--;

            if(count == gameManager.StartJackPotCount)
            {
                col.isTrigger = false;
                count = 0;
            }
        }
    }
}
