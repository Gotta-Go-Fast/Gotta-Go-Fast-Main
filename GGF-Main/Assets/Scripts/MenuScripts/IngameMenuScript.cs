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
    public bool soundTrack;


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
        mute = false;
        soundTrack = true;

        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
    }

    private void Update()
    {


    }

    // Pause Menu

    public void Resume()
    {
        pauseCanvas.enabled = false;

        pauseMusic.Stop();
        backgroundMusic.Play();
    }

    public void Restart()
    {
        Application.LoadLevel(1);
    }

    public void Options()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }


    // Options Menu

    public void ToggleSound()
    {
        if (mute)
        {
            mute = false;
        }
        else
        {
            mute = true;
        }
    }

    public void OptionsBack()
    {
        optionsCanvas.enabled = false;
        pauseCanvas.enabled = true;
    }
}
