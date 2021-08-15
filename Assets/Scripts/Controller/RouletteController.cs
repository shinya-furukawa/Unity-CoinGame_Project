using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RouletteController : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] DropperController dropperController;
    [SerializeField] GameObject rouletteObj;
    Rigidbody rigid;

    public bool isActiveRoulette = false, isStartRoulette = false;

    private void Start()
    {
        rigid = rouletteObj.GetComponent<Rigidbody>();
    }

    async private void FixedUpdate()
    {
        if(isActiveRoulette)
        {
            if(isStartRoulette) StartRoulette();
            else
            {
                if(rigid.angularVelocity.z < 0.15f)
                {
                    Debug.Log("Stop Roulette");
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
        Debug.Log("Start Roulette");
        isStartRoulette = false;
        rigid.AddTorque(0, 0, Random.Range(5,11), ForceMode.Impulse);
    }

    async UniTask StopAction(float stopZ)
    {
        Debug.Log("Action");
        await SelectAction(stopZ);
        Debug.Log("Action End");
        await UniTask.Delay(2000);
        stageManager.isAction = false;
    }

    async UniTask SelectAction(float stopZ)
    {
        switch(stopZ)
        {
            case float n when n < 22.5f: Debug.Log("Black 1");   break;
            case float n when n < 45.0f: Debug.Log("Red 2"); break;
            case float n when n < 67.5f: Debug.Log("Green 3"); dropperController.DropCoins(3);  break;
            case float n when n < 90.0f: Debug.Log("Red 4"); dropperController.DropBall(); break;
            case float n when n < 112.5f: Debug.Log("Green 5"); dropperController.DropCoins(5);  break;
            case float n when n < 135.0f: Debug.Log("Red 6");    break;
            case float n when n < 157.5f: Debug.Log("Green 7"); dropperController.DropCoins(7);  break;
            case float n when n < 180.0f: Debug.Log("Red 8");    break;
            case float n when n < 202.5f: Debug.Log("Black 9");  break;
            case float n when n < 225.0f: Debug.Log("Red 10");   break;
            case float n when n < 247.5f: Debug.Log("Green 11"); dropperController.DropCoins(11); break;
            case float n when n < 270.0f: Debug.Log("Red 12"); dropperController.DropBall(); break;
            case float n when n < 292.5f: Debug.Log("Green 13"); dropperController.DropCoins(13); break;
            case float n when n < 315.0f: Debug.Log("Red 14");   break;
            case float n when n < 337.5f: Debug.Log("Green 15"); dropperController.DropCoins(15); break;
            case float n when n < 360.0f: Debug.Log("Red 16");   break;
        }

        await UniTask.DelayFrame(1);
    }
}
