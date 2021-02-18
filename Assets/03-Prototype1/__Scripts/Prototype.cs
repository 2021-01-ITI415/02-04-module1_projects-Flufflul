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

    [Header("Inspector: Worlds")]
    public GameObject[] worlds;
    private GameObject world;

    // Start is called before the first frame update
    void Start()
    {
        levelCurrent = 0;
        levelLast = worlds.Length;

        InitializeWorld();
    }

    private void InitializeWorld() {
        if (world != null) { Destroy(world); }

        world = Instantiate<GameObject>(worlds[levelCurrent]);
        PlayerController.ResetCoins();
        PrototypeGoal.goalMet = false;

        coins = GameObject.FindGameObjectsWithTag("Coin");
        totalCoins = coins.Length;
    }

    void UpdateGUI() {
        UI_textLevel.text = "World " + (levelCurrent+1) + " / " + levelLast;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
