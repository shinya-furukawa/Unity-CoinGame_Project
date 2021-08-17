using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public MainGameUI mainGameUI;
    public DropperController dropperController;
    [SerializeField] StageManager stageManager;
    [SerializeField] RouletteController rouletteController;
    [SerializeField] GameObject ballStopper, secondBallStopper;
    const int RIGHT = 0, LEFT = 1;

    public float baseWidth;

    [SerializeField] int setCoinCount;
    [SerializeField] int coinCount;
    [SerializeField] int jackPotCount, startJackPotCount;
    public bool jackPotFlag = false;
    public int ballStockCount; 

    public int CoinCount {get => coinCount; set => coinCount = value; }
    public int JackPotCount {get => jackPotCount; set => jackPotCount = value; }
    public int StartJackPotCount {get => startJackPotCount; set => startJackPotCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        mainGameUI.SetGameUI();
        dropperController.DropCoins(setCoinCount);
    }

    private void Update() 
    {
        if(ballStockCount >= startJackPotCount && !jackPotFlag) StartJackPot();

        if(Input.GetButtonDown("Fire1") && coinCount > 0)
        {
            dropperController.ShotCoin(GetInstantiatePosition());
            coinCount--;
            UpdateCoinCount();
        }
    }

    Vector3 GetInstantiatePosition()
    {
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 5, -17);
    }

    public void UpdateCoinCount()
    {
        mainGameUI.UpdateCoinCountText(coinCount);
        mainGameUI.UpdateJackPotText(jackPotCount);
    }

    async public void StartJackPot()
    {
        jackPotFlag = true;
        mainGameUI.rouletteActionText.text = "Jack Pot Chance!!";
        rouletteController.isActiveRoulette = false;
        await UniTask.Delay(5000);

        var count = jackPotCount;
        jackPotCount = 0;
        UpdateCoinCount();

        DropBallJackPotField();
        await MoveJackPotField(count);
        
        rouletteController.isActiveRoulette = true;
        jackPotFlag = false;
    }

    public void DropBallJackPotField()
    {
        ballStopper.GetComponent<BoxCollider>().isTrigger = true;
    }

    async public UniTask MoveJackPotField(int count)
    {
        var camera = GameObject.Find("Main Camera");
        Debug.Log("move camera");
        camera.transform.DOMoveY(-30,10);

        await UniTask.Delay(10000);
        Debug.Log("start jack pot");
        
        secondBallStopper.GetComponent<BoxCollider>().isTrigger = true;
        await UniTask.Delay(3000);
        secondBallStopper.GetComponent<BoxCollider>().isTrigger = false;

        camera.transform.DOMoveY(13,10);
        await UniTask.Delay(10000);
        
        await dropperController.GetRouletteCoin(count, mainGameUI.rouletteActionText);
    }
}
