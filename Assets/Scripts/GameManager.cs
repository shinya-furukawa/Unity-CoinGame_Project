using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MainGameUI mainGameUI;
    public DropperController coinDropperController;
    const int RIGHT = 0, LEFT = 1;

    public float baseWidth;

    [SerializeField] int setCoinCount;
    [SerializeField] int coinCount;
    [SerializeField] int jackPotCount;

    public int CoinCount {get => coinCount; set => coinCount = value; }
    public int JackPotCount {get => jackPotCount; set => jackPotCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        mainGameUI.SetGameUI();
        coinDropperController.SetFirstCoin(setCoinCount);
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1") && coinCount > 0)
        {
            coinDropperController.DropCoin(GetInstantiatePosition());
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
}
