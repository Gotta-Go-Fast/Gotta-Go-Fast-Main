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
    public bool mute;

    public bool paused;


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
        pauseMusic.Stop();
        mute = false;

        paused = false;

        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButton("Pause"))
        {
            paused = true;

            pauseCanvas.enabled = true;
            backgroundMusic.Stop();
            pauseMusic.Play();
        }

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

        pauseMusic.Stop();
        backgroundMusic.Play();
    }

    public void Restart()
    {
        Application.LoadLevel(1);

        paused = false;

        pauseMusic.Stop();
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

        pauseMusic.Stop();
    }


    // Options Menu

    public void ToggleSound()
    {
        if (mute)
        {
            mute = false;

            pauseMusic.Play();
        }
        else
        {
            mute = true;

            pauseMusic.Stop();
        }
    }

    public void OptionsBack()
    {
        optionsCanvas.enabled = false;
        pauseCanvas.enabled = true;
    }
}
