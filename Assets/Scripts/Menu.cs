using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private bool hasSeenCredits = false;
    [SerializeField]
    private GameObject soundTrackObject;

	// Use this for initialization
	void Start () 
	{
        if (GameObject.Find("Soundtrack(Clone)") == null)
        {
            Instantiate(soundTrackObject);
        }
	}
	
    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToGame()
    {
        Destroy(GameObject.Find("Soundtrack(Clone)"));
        SceneManager.LoadScene("TwoPlayerArena");
    }

    public void ToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    //quit if it's a build, stop if it's in the editor
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}