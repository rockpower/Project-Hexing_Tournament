using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public string mainMenu;

    public bool isPaused;
    public bool mainMenuPrompt;
    public bool quitPrompt;

    public GameObject pauseMenuCanvas;
    public GameObject mainMenuPromptCanvas;
    public GameObject quitPromptCanvas;

    // Update is called once per frame
    void Update() {

        if (!isPaused)
            return;

        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        //Main Menu Promt---------------------------------------------------------------------
        if (mainMenuPrompt)
        {
            mainMenuPromptCanvas.SetActive(true);
            pauseMenuCanvas.SetActive(false);
        }
        else
        {
            mainMenuPromptCanvas.SetActive(false);
        }

        //Exit to Desktop Prompt---------------------------------------------------------------
        if (quitPrompt)
        {
            quitPromptCanvas.SetActive(true);
            pauseMenuCanvas.SetActive(false);
        }
        else
        {
            quitPromptCanvas.SetActive(false);
        }

        //Pressing Esc Key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (mainMenuPrompt == true)
            {
                mainMenuPrompt = false;
                isPaused = true;
            }

            if (quitPrompt == true)
            {
                quitPrompt = false;
                isPaused = true;
            }
        }
    }

    //For onscreen Pause button
    public void Pause()
    {
        isPaused = !isPaused;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void MainMenuPrompt()
    {
        mainMenuPrompt = true;
    }

    public void yesMainMenu(string mainMenu)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);
    }

    public void noMainMenu()
    {
        mainMenuPrompt = false;
        pauseMenuCanvas.SetActive(true);
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
        pauseMenuCanvas.SetActive(true);
    }
}
