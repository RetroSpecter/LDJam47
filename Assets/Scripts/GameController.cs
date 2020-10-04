﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public int currArea;
    public Dictionary<int, Area> areas;
    public HashSet<string> events;

    public Rocket rocket;
    public int partsCollected;

    [Space()]
    public GameObject titleScreen;
    public GameObject rocketQuest;

    // Start is called before the first frame update
    void Awake() {
        if (Instance == null) {
            Instance = this;
            this.currArea = 1;
            this.areas = new Dictionary<int, Area>();
            this.events = new HashSet<string>();
        }
    }

    public void RegisterArea(int num, Area area) {
        this.areas[num] = area;
    }

    public Area GetArea(int area) {
        return areas[area];
    }

    public Area GetCurrArea() {
        return Instance.areas[Instance.currArea];
    }

    public void ProgressToNextArea() {
        if(areas.Count == 2)
            titleScreen.SetActive(false);

        rocketQuest.SetActive(true);

        this.GetCurrArea().PlayerLeft();
        this.currArea = (this.currArea % areas.Count) + 1;
        this.GetCurrArea().PlayerEntered();
    }

    public void AddEvent(string e) {
        events.Add(e);
    }

    public bool CheckForEvent(string e) {
        return events.Contains(e);
    }

    public void CollectedPart(RocketPart part) {
        this.partsCollected++;
        this.rocket.CollectPart(part);
    }
}
