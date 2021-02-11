using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode { idle, playing, levelEnd }

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Set in Inspector")]
    public Text UI_txtLevel;
    public Text UI_txtShots;
    public Text UI_txtButton;
    public Vector3 castlePosition;
    public GameObject[] castles; 

    [Header("Set Dynamically")]
    public int level;
    public int maxLevel;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        
        level = 0;
        maxLevel = castles.Length;
        StartLevel();
    }

    void StartLevel() {
        if (castle != null) { Destroy(castle); }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos) { Destroy(pTemp); }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePosition;
        shotsTaken = 0;

        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI() {
        UI_txtLevel.text = "Level: " + (level + 1) + " / " + maxLevel;
        UI_txtShots.text = "Shots Taken: " + (shotsTaken);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing) && Goal.goalMet) {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel() {
        level++;
        if (level == maxLevel) { level = 0; }
        StartLevel();
    }

    public void SwitchView(string eView = "") {
        if (eView == "") { eView = UI_txtButton.text; }
        showing = eView;

        switch (showing) {
            case "Show Slingshot":
                FollowCam.POI = null;
                UI_txtButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                UI_txtButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                UI_txtButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired() { S.shotsTaken++; }
}
