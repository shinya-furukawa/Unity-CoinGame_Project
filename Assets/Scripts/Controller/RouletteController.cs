using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class RouletteController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] StageManager stageManager;
    [SerializeField] DropperController dropperController;
    [SerializeField] GameObject rouletteObj;
    Rigidbody rigid;

    public bool isActiveRoulette = false, isStartRoulette = false;
    private TextMeshPro actionText;

    private void Start()
    {
        rigid = rouletteObj.GetComponent<Rigidbody>();
        actionText = gameManager.mainGameUI.rouletteActionText;
    }

    async private void FixedUpdate()
    {
        if(isActiveRoulette)
        {
            if(isStartRoulette) StartRoulette();
            else
            {
                if(rigid.angularVelocity.z < 0.15f && !gameManager.jackPotFlag)
                {
                    rigid.angularVelocity = Vector3.zero;
                    isActiveRoulette = false;
                    await StopAction(rouletteObj.transform.localEulerAngles.z);
                }
            }
        }
    }

    public void OnStartRoulette()
    {
        isActiveRoulette = true;
        isStartRoulette = true;
    }

    private void StartRoulette()
    {
        actionText.text = "Start Roulette";
        isStartRoulette = false;
        rigid.AddTorque(0, 0, Random.Range(5,11), ForceMode.Impulse);
    }

    async UniTask StopAction(float stopZ)
    {
        await SelectAction(stopZ);
        await UniTask.Delay(700);
        stageManager.isAction = false;
    }

    async UniTask SelectAction(float stopZ)
    {
        switch(stopZ)
        {
            case float n when n < 22.5f: Debug.Log("Black 1");   await RoulettePushCoin(100); break;
            case float n when n < 45.0f: Debug.Log("Red 2");     dropperController.DropBall(); actionText.text = "Get Ball!!";  break;
            case float n when n < 67.5f: Debug.Log("Green 3");   await RoulettePushCoin(3); break;
            case float n when n < 90.0f: Debug.Log("Red 4");     dropperController.DropBall(); actionText.text = "Get Ball!!"; break;
            case float n when n < 112.5f: Debug.Log("Green 5");  await RoulettePushCoin(5); break;
            case float n when n < 135.0f: Debug.Log("Red 6");    dropperController.DropBall(); actionText.text = "Get Ball!!"; break;
            case float n when n < 157.5f: Debug.Log("Green 7");  await RoulettePushCoin(7); break;
            case float n when n < 180.0f: Debug.Log("Red 8");    dropperController.DropBall(); actionText.text = "Get Ball!!"; break;
            case float n when n < 202.5f: Debug.Log("Black 9");  await RoulettePushCoin(50); break;
            case float n when n < 225.0f: Debug.Log("Red 10");   dropperController.DropBall(); actionText.text = "Get Ball!!"; break;
            case float n when n < 247.5f: Debug.Log("Green 11"); await RoulettePushCoin(11); break;
            case float n when n < 270.0f: Debug.Log("Red 12");   dropperController.DropBall(); actionText.text = "Get Ball!!"; break;
            case float n when n < 292.5f: Debug.Log("Green 13"); await RoulettePushCoin(13); break;
            case float n when n < 315.0f: Debug.Log("Red 14");   dropperController.DropBall(); actionText.text = "Get Ball!!"; break;
            case float n when n < 337.5f: Debug.Log("Green 15"); await RoulettePushCoin(15); break;
            case float n when n < 360.0f: Debug.Log("Red 16");   dropperController.DropBall(); actionText.text = "Get Ball!!"; break;
        }

        await UniTask.DelayFrame(1);
    }

    async public UniTask RoulettePushCoin(int count)
    {
        gameManager.JackPotCount += count;
        gameManager.UpdateCoinCount();
        actionText.text = "Get $ " + count + " Coin!!\n$ " + count;
        await dropperController.GetRouletteCoin(count, actionText);
    }
}
