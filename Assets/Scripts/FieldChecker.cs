using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldChecker : MonoBehaviour
{
    public enum FieldPosition
    {
        Pusher,
        DropCenter,
        DropRight,
        DropLeft,
        CheckBonus,
        CheckCounter,
        CoinField
    }

    [SerializeField] FieldPosition fieldPostition;
    [SerializeField] DropFieldContorller dropFieldContorller;
    [SerializeField] CoinDropperController coinDropperController;

    private Transform pusherCoinField, coinField;

    private void Start()
    {
        pusherCoinField = GameObject.Find("PusherCoinField").transform;
        coinField = GameObject.Find("CoinField").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin" || other.gameObject.tag == "10Coin")
        {
            switch(fieldPostition)
            {
                case FieldPosition.CheckBonus:
                    coinDropperController.DropBunusCoin();
                    break;

                case FieldPosition.CheckCounter:
                    break;

                case FieldPosition.Pusher:
                    other.transform.SetParent(pusherCoinField);
                    break;

                case FieldPosition.CoinField:
                    other.transform.SetParent(coinField);
                    break;

                case FieldPosition.DropCenter: case FieldPosition.DropRight: case FieldPosition.DropLeft:
                    Debug.Log("destroy");
                    dropFieldContorller.DestroyCoin(other.gameObject, fieldPostition);
                    break;
            }
        }
    }
}
