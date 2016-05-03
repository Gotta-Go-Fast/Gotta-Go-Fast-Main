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
    public AudioSource mainMenuMusic;
    public static bool mute;

    public bool muteCheck;

    // Character Selection
    public GameObject characterBoxes;
    public CharacterCreation characterCreationPlayer1;
    public CharacterCreation characterCreationPlayer2;

    // Level Selection
    public int levelNumber;
    private Vector3 spawnPoint;

    public Player player1;
    public Player player2;


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
        mainMenuMusic = mainMenuMusic.GetComponent<AudioSource>();

        // Character
        characterBoxes = characterBoxes.GetComponent<GameObject>();

        // Level
        levelNumber = 1;

        mainMenuCanvas.enabled = true;
        optionsCanvas.enabled = false;
        characterMenuCanvas.enabled = false;
        levelMenuCanvas.enabled = false;
    }

    // Update
    private void Update()
    {
        muteCheck = mute;

        if (mute)
        {
            mainMenuMusic.Pause();
        }
        else if (!mute)
        {
            mainMenuMusic.UnPause();
        }

        Time.timeScale = 1;
    }

    // Main Menu

    public void StartGame()
    {
        // Temporary shortcut for testing.

        //Application.LoadLevel(1);


        // the original code.

        mainMenuCanvas.enabled = false;
        characterMenuCanvas.enabled = true;

        ShowCharacterBoxes();
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
        }
        else
        {
            mute = true;
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

        HideCharacterBoxes();
        FindCharacters();
    }
    public void CharacterSelectionBack()
    {
        characterMenuCanvas.enabled = false;
        mainMenuCanvas.enabled = true;

        HideCharacterBoxes();
    }


    // Level Selection Menu

    public void Play()
    {
        mainMenuMusic.Pause();
        levelMenuCanvas.enabled = false;

        FindCharacters();
        LoadCharactersToNextScene();
        Application.LoadLevel(1);        
    }
    public void LevelSelectionBack()
    {
        levelMenuCanvas.enabled = false;
        characterMenuCanvas.enabled = true;

        ShowCharacterBoxes();
    }


    // Moving characterboxes in and out from the screen
    private void ShowCharacterBoxes()
    {
        characterBoxes.transform.position = new Vector3(13, -1, 0);
    }
    private void HideCharacterBoxes()
    {
        characterBoxes.transform.position = new Vector3(-13, -1, 0);
    }

    public void FindCharacters()
    {
        player1 = characterCreationPlayer1.GetCharacter();
        player2 = characterCreationPlayer2.GetCharacter();
    }

    private void LoadCharactersToNextScene()
    {
        player1.transform.position = new Vector3(-14, 0, 0);
        player2.transform.position = new Vector3(-14, 0, 0);

        DontDestroyOnLoad(this);
        
        DontDestroyOnLoad(characterBoxes);
    }
}
