using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {


    // MainMenu Buttons
    public Button startGame;
    public Button options;
    public Button quitGame;

    // OptionsMenu Buttons
    public Button toggleSound;
    public Button optionsBack;

    // CharacterSelection Buttons
    public Button levelSelection;
    public Button characterBack;

    // LevelSelection Buttons
    public Button play;
    public Button levelBack;

    // Canvases
    public Canvas mainMenuCanvas;
    public Canvas optionsCanvas;
    public Canvas characterMenuCanvas;
    public Canvas levelMenuCanvas;

    // Music
    public AudioSource backgroundMusic;
    public bool mute;


    void Start ()
    {
        // MainMenu Buttons
        startGame = startGame.GetComponent<Button>();
        options = options.GetComponent<Button>();
        quitGame = quitGame.GetComponent<Button>();

        // OptionsMenu Buttons
        toggleSound = toggleSound.GetComponent<Button>();
        optionsBack = optionsBack.GetComponent<Button>();

        // CharacterSelection Buttons
        levelSelection = levelSelection.GetComponent<Button>();
        characterBack = characterBack.GetComponent<Button>();

        // LevelSelection Buttons
        play = play.GetComponent<Button>();
        levelBack = levelBack.GetComponent<Button>();

        // Canvases
        mainMenuCanvas = mainMenuCanvas.GetComponent<Canvas>();
        optionsCanvas = optionsCanvas.GetComponent<Canvas>();
        characterMenuCanvas = characterMenuCanvas.GetComponent<Canvas>();
        levelMenuCanvas = levelMenuCanvas.GetComponent<Canvas>();

        // Music
        backgroundMusic = backgroundMusic.GetComponent<AudioSource>();
        mute = false;

        mainMenuCanvas.enabled = true;
        optionsCanvas.enabled = false;
        characterMenuCanvas.enabled = false;
        levelMenuCanvas.enabled = false;
    }


    // Main Menu

    public void StartGame()
    {
        mainMenuCanvas.enabled = false;
        characterMenuCanvas.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        mainMenuCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }


    // Options Menu

    public void ToggleSound()
    {
        if (mute)
        {
            mute = false;
            backgroundMusic.Play();
        }
        else
        {
            mute = true;
            backgroundMusic.Stop();
        }
    }

    public void OptionsBack()
    {
        optionsCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }


    // Character Selection Menu

    public void LevelSelection()
    {
        characterMenuCanvas.enabled = false;
        levelMenuCanvas.enabled = true;
    }

    public void CharacterSelectionBack()
    {
        characterMenuCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }


    // Level Selection Menu

    public void Play()
    {
        Application.LoadLevel(1);
    }

    public void LevelSelectionBack()
    {
        levelMenuCanvas.enabled = false;
        characterMenuCanvas.enabled = true;
    }
}
