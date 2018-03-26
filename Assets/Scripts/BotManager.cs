using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for this script makes use of heavy reference from the Unity Tanks Tutorial, Found below:
// https://unity3d.com/learn/tutorials/s/tanks-tutorial

//this is [I'm pretty sure] essentially a controller script for the junkbots, allowing them to spawn, reset, etc

    [Serializable]
public class BotManager
{
    //variables
    public Color PlayerColor;
    public Transform SpawnPoint;

    [HideInInspector]
    public int PlayerNumber; //the number that determines which input axis controls the junkbot
    [HideInInspector]
    public string ColoredPlayerText;
    [HideInInspector]
    public GameObject Instance; //the specific BotManager instance attached to a given junkbot
    [HideInInspector]
    public int Wins; //the number of wins this specific player has
    //reference to the scripts on the instance of the junkbot
    [HideInInspector]
    public Junkbot junkbot; 

    //functions
    //This script sets everything up for the junkbot and connects it to the player number
    public void Setup()
    {
        //set the reference
        junkbot = Instance.GetComponent<Junkbot>();

        ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerColor) + ">PLAYER " + PlayerNumber + "</color>";

        junkbot.PlayerNumber = PlayerNumber;
    }

    public void DisableControl()
    {
        junkbot.IsControlEnabled = false;
        
    }

    public void EnableControl()
    {
        junkbot.IsControlEnabled = true;
        junkbot.IsAlive = true;
        junkbot.ResetShots();
    }

    public void Reset()
    {
        //reset bot position to spawn
        Instance.transform.position = SpawnPoint.position;
        Instance.transform.rotation = SpawnPoint.rotation;

        //reset its activity
        Instance.SetActive(false);
        Instance.SetActive(true);
    }
}
