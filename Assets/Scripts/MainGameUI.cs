using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    public GameManager gameManager;
    public Text holdCoinText;
    public Text jackPotText;
    // Start is called before the first frame update
    public void SetGameUI()
    {
        holdCoinText.text = "所持コイン：" + gameManager.CoinCount + " 枚";
        jackPotText.text = "JackPot：" + gameManager.JackPotCount + " 枚";
    }

    public void UpdateCoinCountText(int count)
    {
        holdCoinText.text = "所持コイン：" + count + " 枚";
    }

    public void UpdateJackPotText(int count)
    {
        jackPotText.text = "JackPot：" + count + " 枚";
    }
}
