using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class DropperController : MonoBehaviour
{
    [SerializeField] GameObject coin_prefab, coin_10_prefab, ball_prefab;
    [SerializeField] Transform escapeField, coinField, rouletteCoinPoint, ballDropPoint;

    public Transform BallDropPoint {get => ballDropPoint; }

    Vector3 startPosition;

    [SerializeField] float speed;
    [SerializeField] float amplitude;

    public void ShotCoin(Vector3 shotPosision)
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

    public void DropCoins(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var coin = Instantiate(coin_prefab, new Vector3(Random.Range(-4,5), 20, Random.Range(-1,-13)), Quaternion.identity);
            coin.transform.SetParent(escapeField);
        }
    }

    public void DropBall()
    {
        var coin = Instantiate(ball_prefab, new Vector3(Random.Range(-4,5), 20, Random.Range(-1,-13)), Quaternion.identity);
        coin.transform.SetParent(escapeField);
    }

    public void DropStockBall()
    {
        var ball = Instantiate(ball_prefab, ballDropPoint);
    }

    async public UniTask GetRouletteCoin(int count, TextMeshPro actionText)
    {
        var coinCount = count;
        for(int i = 0; i < count; i++)
        {
            var coin = Instantiate(coin_prefab, rouletteCoinPoint);
            coin.transform.SetParent(escapeField);
            var coin_r = coin.GetComponent<Rigidbody>();
            coin_r.AddForce(Vector3.left * Random.Range(300,500));

            coinCount--;
            actionText.text = "Get $ " + count + " Coin!!\n$ " + coinCount;
            
            await UniTask.Delay(75);
        }
    }

}
