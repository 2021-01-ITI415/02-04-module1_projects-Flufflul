using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Prototype : MonoBehaviour
{
    [Header("Inspector: UI Elements")]
    public TextMeshProUGUI UI_textLevel;
    private int levelCurrent;
    private int levelLast;

    private int totalCoins;
    private GameObject[] coins;

    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        totalCoins = coins.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
