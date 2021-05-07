using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PermanentUI : MonoBehaviour
{
    public int goldCoins = 0;
    public int coinCheckpoint = 0;
    public TextMeshProUGUI coinsText;
    public int maxHealth = 3;
    public int currentHealth;

    public static PermanentUI UI;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        currentHealth = maxHealth; 
        coinsText.text = goldCoins.ToString();

        if (!UI)
            UI = this;
        else
            Destroy(gameObject);
    }

    public void Reset()
    {
        goldCoins = coinCheckpoint;
        currentHealth = maxHealth;
        coinsText.text = goldCoins.ToString();
    }
}
