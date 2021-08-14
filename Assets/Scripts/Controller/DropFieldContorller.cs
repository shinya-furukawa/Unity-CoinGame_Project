using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFieldContorller : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public void DestroyCoin(GameObject coinObj, FieldChecker.FieldPosition dropPostion)
    {
        if(dropPostion == FieldChecker.FieldPosition.DropCenter)
        {
            gameManager.CoinCount += coinObj.GetComponent<Coin>().CoinCount;
            gameManager.UpdateCoinCount();
        }

        if(dropPostion == FieldChecker.FieldPosition.DropRight || dropPostion == FieldChecker.FieldPosition.DropLeft)
        {
            gameManager.JackPotCount += coinObj.GetComponent<Coin>().CoinCount;
            gameManager.UpdateCoinCount();
        }
        Destroy(coinObj);
    }
}
