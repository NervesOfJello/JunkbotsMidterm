using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private bool hasSeenCredits = false;

	// Use this for initialization
	void Start () 
	{
        DontDestroyOnLoad(gameObject);
	}
	
    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void onClickPlay()
    {
        if (hasSeenCredits)
        {
            SceneManager.LoadScene("ArenaLevel");
            Destroy(gameObject, 1);
        }
        else
        {
            hasSeenCredits = true;
            SceneManager.LoadScene("Instructions");
        }
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