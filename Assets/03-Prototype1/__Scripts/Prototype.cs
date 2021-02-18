using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum P_GAMEMODE { idle, playing, levelEnd }

public class Prototype : MonoBehaviour
{
    [Header("Inspector: UI Elements")]
    public TextMeshProUGUI UI_textLevel;
    private int levelCurrent;
    private int levelLast;

    private int totalCoins;
    private GameObject[] coins;

    [Header("Inspector: Level Selection")]
    public GameObject[] worlds;
    private GameObject world;
    private P_GAMEMODE mode = P_GAMEMODE.idle;
    public static bool isNewWorld;

    // Start is called before the first frame update
    void Start()
    {
        levelCurrent = 0;
        levelLast = worlds.Length;

        GameObject go = GameObject.FindGameObjectWithTag("World");
        if (go != null) { Destroy(go); }
        InitializeWorld();
    }

    private void notNewWorld() { isNewWorld = false; }

    private void InitializeWorld() {
        if (world != null) { Destroy(world); }

        world = Instantiate<GameObject>(worlds[levelCurrent]);
        PlayerController.ResetCoins();
        PrototypeGoal.goalMet = false;

        coins = GameObject.FindGameObjectsWithTag("Coin");
        totalCoins = coins.Length;

        UpdateGUI();
        mode = P_GAMEMODE.playing;

        isNewWorld = true;
        Invoke("notNewWorld", 0.1f);
    }

    void UpdateGUI() {
        UI_textLevel.text = "World " + (levelCurrent+1) + " / " + levelLast;
    }

    // Update is called once per frame
    void Update()
    {
        if (PrototypeGoal.goalMet && mode == P_GAMEMODE.playing) {
            mode = P_GAMEMODE.levelEnd;
            Invoke("NextWorld", 3f);
        }
    }

    private void NextWorld() {
        levelCurrent++;
        if (levelCurrent >= levelLast) {
            levelCurrent = 0;
        }

        InitializeWorld();
    }
}
