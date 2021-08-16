using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainGameUI : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshPro holdCoinText, jackPotText, rouletteActionText;
    // Start is called before the first frame update
    public void SetGameUI()
    {
        rouletteActionText.text = "";
        holdCoinText.text = "$ " + gameManager.CoinCount;
        jackPotText.text = "$ " + gameManager.JackPotCount;
    }

    public void UpdateCoinCountText(int count)
    {
        holdCoinText.text = "$ " + count;
    }

    public void UpdateJackPotText(int count)
    {
        jackPotText.text = "$ " + count;
    }
}
