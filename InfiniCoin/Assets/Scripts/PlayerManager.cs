using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText();
    }

    private void Update()
    {
        
    }

    private void UpdateCoinText()
    {
        if (coinText != null) // Without this check, Start calls before coinText is assigned (Though it's serialized?) Resulting in Critical error despite working
        {
            coinText.text = coinsCollected.ToString();
        }        
    }

    public void AddToScore()
    {
        coinsCollected++;
        UpdateCoinText();
    }
}
