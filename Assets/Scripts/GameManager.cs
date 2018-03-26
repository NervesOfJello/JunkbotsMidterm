using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Code for this script makes use of heavy reference from the Unity Tanks Tutorial, Found below:
// https://unity3d.com/learn/tutorials/s/tanks-tutorial

public class GameManager : MonoBehaviour
{
    //fields necessary for the editor
    [SerializeField]
    private int roundsToWin = 3;
    [SerializeField]
    private float startDelay = 3f;
    [SerializeField]
    private float endDelay = 3f;
    [SerializeField]
    private CameraControl cameraControl;
    [SerializeField]
    private Text messageText;
    [SerializeField]
    private GameObject junkbotPrefab;
    [SerializeField]
    private BotManager[] bots;

    private int roundNumber;
    private WaitForSeconds StartRoundWait;
    private WaitForSeconds EndRoundWait;
    private BotManager roundWinner;
    private BotManager gameWinner;

	// Use this for initialization
	void Start () 
	{
        //set coroutines for their future calling
        StartRoundWait = new WaitForSeconds(startDelay);
        EndRoundWait = new WaitForSeconds(endDelay);

        SpawnAllBots();
        SetCameraTargets();

        StartCoroutine(GameLoop());
	}

    //spawns all junkbots and sets them up using the botmanager
    private void SpawnAllBots()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].Instance = Instantiate(junkbotPrefab, bots[i].SpawnPoint.position, bots[i].SpawnPoint.rotation) as GameObject;
            bots[i].PlayerNumber = i + 1;
            bots[i].Setup();
        }
    }

    //set camera targets to identify all living junkbots in the bots[] array then returns them to the cameraControl script
    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[bots.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            if (bots[i].junkbot.IsAlive)
            {
                targets[i] = bots[i].Instance.transform;
            }
        }

        cameraControl.Targets = targets;
    }

    //coroutine to manage the game loop
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        ResetAllBots();
        DisableBotControl();
        //cameraControl.SetStartPositionAndSize();
        roundNumber++;
        messageText.text = "ROUND " + roundNumber;
        yield return StartRoundWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableBotControl();
        messageText.text = string.Empty;
        while (!OneBotLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        DisableBotControl();
        roundWinner = null;
        roundWinner = GetRoundWinner();
        //check for round winner
        if (roundWinner != null)
        {
            roundWinner.Wins++;
        }
        //check for game winner
        gameWinner = GetGameWinner();
        string message = EndMessage();
        messageText.text = message;

        yield return EndRoundWait;
    }

    //return true if there is only one robot left
    private bool OneBotLeft()
    {
        int numBotsLeft = 0;

        for (int i = 0; i < bots.Length; i++)
        {
            if (bots[i].junkbot.IsAlive)
            {
                numBotsLeft++;
            }
        }

        return numBotsLeft <= 1;
    }

    //get whatever bot is alive and if none are, return null
    private BotManager GetRoundWinner()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            if (bots[i].junkbot.IsAlive)
            {
                return bots[i];
            }
        }

        return null;
    }

    private BotManager GetGameWinner()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            if (bots[i].Wins == roundsToWin)
            {
                return bots[i];
            }
        }

        return null;
    }

    private string EndMessage()
    {
        string message = "nobody wins";

        if (roundWinner != null)
        {
            message = roundWinner.ColoredPlayerText + " DEFEATED THEIR OPPONENT";
        }

        message += "\n\n";

        for (int i = 0; i < bots.Length; i++)
        { 
            message += bots[i].ColoredPlayerText + ": " + bots[i].Wins + " Points\n";
        }

        if (gameWinner != null)
        {
            message = gameWinner.ColoredPlayerText + " HAS EMERGED VICTORIOUS";
        }

        return message;
    }

    private void ResetAllBots()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].Reset();
        }
    }

    private void DisableBotControl()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].DisableControl();
        }
    }

    private void EnableBotControl()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].EnableControl();
        }
    }
}
