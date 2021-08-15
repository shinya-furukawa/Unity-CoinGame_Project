using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperController : MonoBehaviour
{
    [SerializeField] GameObject coin_prefab, coin_10_prefab, ball_prefab;
    [SerializeField] Transform escapeField, coinField;

    Vector3 startPosition;

    [SerializeField] float speed;
    [SerializeField] float amplitude;

    public void DropCoin(Vector3 shotPosision)
    {
        var coin = Instantiate(coin_prefab, shotPosision, Quaternion.identity);
        coin.transform.rotation = Quaternion.Euler(-35, 0,0);
        coin.transform.SetParent(escapeField);
        var coin_r = coin.GetComponent<Rigidbody>();
        coin_r.AddForce(transform.forward * 800);
    }

    public void DropBunusCoin()
    {
        var coin = Instantiate(coin_10_prefab, new Vector3(Random.Range(-4,5), 20, Random.Range(-1,-13)), Quaternion.identity);
        coin.transform.SetParent(escapeField);
    }

    public void SetFirstCoin(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var coin = Instantiate(coin_prefab, new Vector3(Random.Range(-4,5), 20, Random.Range(-1,-13)), Quaternion.identity);
            coin.transform.SetParent(escapeField);
        }
    }
}
