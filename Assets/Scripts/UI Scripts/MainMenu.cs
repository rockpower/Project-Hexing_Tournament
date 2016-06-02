using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public string startScene;
    public GameObject quitPromptCanvas;
    public bool quitPrompt;
	
	// Update is called once per frame
	void Update ()
    {
        if (quitPrompt)
        {
            quitPromptCanvas.SetActive(true);
        }
        else
        {
            quitPromptCanvas.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPrompt = !quitPrompt;
        }
    }

    public void StartGame(string startScene)
    {
        //Application.LoadLevel(startScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(startScene);
    }

    public void ExitPrompt()
    {
        quitPrompt = true;
    }

    public void yesQuit()
    {
        Application.Quit();
    }

    public void noQuit()
    {
        quitPrompt = false;
    }
}
