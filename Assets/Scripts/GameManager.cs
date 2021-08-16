using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public MainGameUI mainGameUI;
    public DropperController dropperController;
    [SerializeField] StageManager stageManager;
    [SerializeField] RouletteController rouletteController;
    const int RIGHT = 0, LEFT = 1;

    public float baseWidth;

    [SerializeField] int setCoinCount;
    [SerializeField] int coinCount;
    [SerializeField] int jackPotCount;
    public bool jackPotFlag = false;
    public int ballStockCount; 

    public int CoinCount {get => coinCount; set => coinCount = value; }
    public int JackPotCount {get => jackPotCount; set => jackPotCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        mainGameUI.SetGameUI();
        dropperController.DropCoins(setCoinCount);
    }

    private void Update() 
    {
        if(ballStockCount >= 3 && !jackPotFlag) StartJackPot();

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
        Debug.Log("Jack Pot OPEN");
        rouletteController.isActiveRoulette = false;

        var count = jackPotCount;
        jackPotCount = 0;
        UpdateCoinCount();

        await DeleteBall();
        await dropperController.GetRouletteCoin(count, mainGameUI.rouletteActionText);
        
        rouletteController.isActiveRoulette = true;
        jackPotFlag = false;
    }

    async public UniTask DeleteBall()
    {
        if(dropperController.BallDropPoint.transform.childCount != 0)
        {
            for(int i = dropperController.BallDropPoint.transform.childCount; i > 0; i--)
            {
                Destroy(dropperController.BallDropPoint.transform.GetChild(i-1).gameObject);
                ballStockCount = dropperController.BallDropPoint.transform.childCount - 1;
                await UniTask.Delay(1000);
            }
        }
    }
}
