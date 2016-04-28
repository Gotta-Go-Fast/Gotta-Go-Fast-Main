using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngameMenuScript : MonoBehaviour
{


    // PauseMenu Buttons
    public Button resume;
    public Button restart;
    public Button options;
    public Button mainMenu;

    // OptionsMenu Buttons
    public Button toggleSound;
    public Button optionsBack;

    // Canvases
    public Canvas pauseCanvas;
    public Canvas optionsCanvas;

    // Music
    public AudioSource backgroundMusic;
    public AudioSource pauseMusic;

    public static bool paused;
    public bool muteCheck;

    void Start()
    {
        // MainMenu Buttons
        resume = resume.GetComponent<Button>();
        restart = restart.GetComponent<Button>();
        options = options.GetComponent<Button>();
        mainMenu = mainMenu.GetComponent<Button>();

        // OptionsMenu Buttons
        toggleSound = toggleSound.GetComponent<Button>();
        optionsBack = optionsBack.GetComponent<Button>();

        // Canvases
        pauseCanvas = pauseCanvas.GetComponent<Canvas>();
        optionsCanvas = optionsCanvas.GetComponent<Canvas>();

        // Music
        backgroundMusic = backgroundMusic.GetComponent<AudioSource>();
        pauseMusic = pauseMusic.GetComponent<AudioSource>();

        paused = false;

        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;


    }

    private void Update()
    {
        muteCheck = MenuScript.mute;


        // No music if muted
        if (muteCheck)
        {
            backgroundMusic.Pause();
            pauseMusic.Pause();
        }

        // Music if not muted and paused
        if (!muteCheck && paused)
        {
            pauseMusic.UnPause();
            backgroundMusic.Pause();
        }

        // Music if not muted and not paused
        if (!muteCheck && !paused)
        {
            backgroundMusic.UnPause();
            pauseMusic.Pause();
        }

        // Press "P" for pause
        if (Input.GetButton("Pause"))
        {
            paused = true;

            pauseCanvas.enabled = true;

            if (!muteCheck)
            {
                backgroundMusic.Pause();
                pauseMusic.UnPause();
            }
        }

        // Freezing time when paused
        if (paused)
        {
            Time.timeScale = 0;
        }

        if (!paused)
        {
            Time.timeScale = 1;
        }

    }

    // Pause Menu

    public void Resume()
    {
        pauseCanvas.enabled = false;
        paused = false;
    }

    public void Restart()
    {
        Application.LoadLevel(1);

        paused = false;
    }

    public void Options()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }

    public void MainMenu()
    {
        Application.LoadLevel(0);

        paused = false;
    }


    // Options Menu

    public void ToggleSound()
    {
        if (muteCheck)
        {
            MenuScript.mute = false;
        }
        else if (!muteCheck)
        {
            MenuScript.mute = true;
        }
    }

    public void OptionsBack()
    {
        optionsCanvas.enabled = false;
        pauseCanvas.enabled = true;
    }
}
