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
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
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

    // SoundEffects
    public AudioSource vemEDu;
    public CalloutScript calloutScript;

    // Character Selection
    public GameObject characterBoxes;
    public CharacterCreation characterCreationPlayer1;
    public CharacterCreation characterCreationPlayer2;

    // Level Selection
    public int levelNumber;

    public Player player1;
    public Player player2;

    private void Awake()
    {
        mainMenuCanvas.enabled = true;
    }
    private void Start ()
    {
        // MainMenu Buttons
        //startGame = startGame.GetComponent<Button>();
        //options = options.GetComponent<Button>();
        //quitGame = quitGame.GetComponent<Button>();

        // OptionsMenu Buttons
        //toggleSound = toggleSound.GetComponent<Button>();
        //optionsBack = optionsBack.GetComponent<Button>();

        // CharacterSelection Buttons
        //levelSelection = levelSelection.GetComponent<Button>();
        //characterBack = characterBack.GetComponent<Button>();

        // LevelSelection Buttons
        //level1 = level1.GetComponent<Button>();
        //level2 = level2.GetComponent<Button>();
        //level3 = level3.GetComponent<Button>();
        //level4 = level4.GetComponent<Button>();
        //play = play.GetComponent<Button>();
        //levelBack = levelBack.GetComponent<Button>();

        // Canvases
        //mainMenuCanvas = mainMenuCanvas.GetComponent<Canvas>();
        //optionsCanvas = optionsCanvas.GetComponent<Canvas>();
        //characterMenuCanvas = characterMenuCanvas.GetComponent<Canvas>();
        //levelMenuCanvas = levelMenuCanvas.GetComponent<Canvas>();

        // Music
        //mainMenuMusic = mainMenuMusic.GetComponent<AudioSource>();

        // SoundEffects
        vemEDu = characterMenuCanvas.GetComponentInChildren<AudioSource>();
        calloutScript = GameObject.Find("CalloutScript").GetComponent<CalloutScript>();

        // Character
        //characterBoxes = characterBoxes.GetComponent<GameObject>();
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

        if (Input.GetKeyDown(KeyCode.G))
        {
            Play();
        }
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

        if (!mute)
            vemEDu.Play();
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
    }
    public void CharacterSelectionBack()
    {
        characterMenuCanvas.enabled = false;
        mainMenuCanvas.enabled = true;

        HideCharacterBoxes();
    }

    // Level Selection Menu
    public void Level1()
    {
        levelNumber = 1;
    }
    public void Level2()
    {
        levelNumber = 2;
    }
    public void Level3()
    {
        levelNumber = 3;
    }
    public void Level4()
    {
        levelNumber = 4;
    }

    public void Play()
    {
        mainMenuMusic.Pause();
        levelMenuCanvas.enabled = false;

        FindCharacters();
        LoadCharactersToNextScene();
        LoadLevel();
    }
    public void LevelSelectionBack()
    {
        levelMenuCanvas.enabled = false;
        characterMenuCanvas.enabled = true;

        ShowCharacterBoxes();

        if (!mute)
            vemEDu.Play();
    }

    // Moving characterboxes in and out from the screen
    private void ShowCharacterBoxes()
    {
        characterBoxes.transform.position = new Vector3(13, -1, 0);
    }
    private void HideCharacterBoxes()
    {
        characterBoxes.transform.position = new Vector3(-100, -1, 0);
    }

    public void FindCharacters()
    {
        player1 = characterCreationPlayer1.GetCharacter();
        player2 = characterCreationPlayer2.GetCharacter();

        player1.otherPlayer = player2;
        player2.otherPlayer = player1;
    }
    private void LoadCharactersToNextScene()
    {
        player1.active = false;
        player2.active = false;

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(characterBoxes);
    }
    private void LoadLevel()
    {
        if (levelNumber > 0 && levelNumber < 5)
        {
            Application.LoadLevel(levelNumber);
        }
        else
        {
            Application.LoadLevel(4);
        }
    }
}
