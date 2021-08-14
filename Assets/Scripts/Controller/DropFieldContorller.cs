using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFieldContorller : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public void DestroyCoin(GameObject obj, FieldChecker.FieldPosition dropPostion)
    {
        if(obj.tag == "Coin" || obj.tag == "10Coin")
        {
            if(dropPostion == FieldChecker.FieldPosition.DropCenter)
            {
                gameManager.CoinCount += obj.GetComponent<Coin>().CoinCount;
                gameManager.UpdateCoinCount();
            }

            if(dropPostion == FieldChecker.FieldPosition.DropRight || dropPostion == FieldChecker.FieldPosition.DropLeft)
            {
                gameManager.JackPotCount += obj.GetComponent<Coin>().CoinCount;
                gameManager.UpdateCoinCount();
            }
        }

        if(obj.tag == "Ball")
        {
            Debug.Log("Ball Destroy!!");
        }
        
        Destroy(obj);
    }
}
