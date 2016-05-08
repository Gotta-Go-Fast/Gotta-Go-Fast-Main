using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngameMenuScript : MonoBehaviour
{
    // Players
    public Transform spawnPoint;

    private Player player1;
    private Player player2;

    // PauseMenu Buttons
    public Button resume;
    public Button restart;
    public Button options;
    public Button mainMenu;

    // OptionsMenu Buttons
    public Button toggleSound;
    public Button optionsBack;

    // WinMenu Buttons
    public Button winRestart;

    // Canvases
    public Canvas pauseCanvas;
    public Canvas optionsCanvas;
    public Canvas winCanvas;

    // Music
    public AudioSource backgroundMusic;
    public AudioSource pauseMusic;
    public AudioSource winMusic;

    public static bool paused;
    public bool muteCheck;

    private float loseTimer;

    public MenuScript menuScript;
    public EvilOverlordCamera evilOverlordCamera;

    // Countdown
    private float countDownTimer;
    public bool countDown;

    private CalloutScript calloutScript;

    private void Awake()
    {
        menuScript = FindObjectOfType<MenuScript>();
        spawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        evilOverlordCamera = GameObject.Find("Main Camera").GetComponent<EvilOverlordCamera>();
        calloutScript = GameObject.Find("CalloutScript").GetComponent<CalloutScript>();

        // Players
        player1 = menuScript.player1.GetComponent<Player>();
        player2 = menuScript.player2.GetComponent<Player>();

        player1.transform.position = spawnPoint.position;
        player2.transform.position = spawnPoint.position;

        paused = false;
        ActivateCountdown();

        backgroundMusic.volume = 0.2f;
    }
    private void Start()
    {
        // MainMenu Buttons
        resume = resume.GetComponent<Button>();
        restart = restart.GetComponent<Button>();
        options = options.GetComponent<Button>();
        mainMenu = mainMenu.GetComponent<Button>();

        // OptionsMenu Buttons
        toggleSound = toggleSound.GetComponent<Button>();
        optionsBack = optionsBack.GetComponent<Button>();

        // WinMenu Buttons
        winRestart = winRestart.GetComponent<Button>();

        // Canvases
        pauseCanvas = pauseCanvas.GetComponent<Canvas>();
        optionsCanvas = optionsCanvas.GetComponent<Canvas>();
        winCanvas = winCanvas.GetComponent<Canvas>();

        // Music
        backgroundMusic = backgroundMusic.GetComponent<AudioSource>();
        pauseMusic = pauseMusic.GetComponent<AudioSource>();
        winMusic = winMusic.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Pause();
        Mute();
        Win();
        Lose();
    }

    private void Pause()
    {
        // Press "P" for pause
        if (Input.GetButton("Pause") && !winCanvas.enabled)
        {
            paused = true;

            pauseCanvas.enabled = true;
            winCanvas.enabled = false;

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
    private void Mute()
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
    }
    private void Win()
    {
        if ((player1.winner || player2.winner) && (player1.active && player2.active))
        {
            backgroundMusic.Pause();
            pauseMusic.Pause();
            //winMusic.UnPause();

            winCanvas.enabled = true;
            paused = true;

            player1.active = false;
            player2.active = false;

            calloutScript.Applause();
        }
    }
    private void Lose()
    {
        if (player1.loser || player2.loser)
        {
            if (loseTimer < 1f)
            {
                loseTimer += Time.deltaTime;
            }

            else
            {
                backgroundMusic.Pause();
                pauseMusic.Pause();
                //winMusic.UnPause();

                winCanvas.enabled = true;
                paused = true;
            }
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
        pauseCanvas.enabled = false;
        RestartLevel();
    }
    public void Options()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }
    public void MainMenu()
    {
        pauseCanvas.enabled = false;
        paused = false;

        DestroyObjects();

        Application.LoadLevel(0);
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
    
    // Win Menu
    public void WinRestart()
    {
        winCanvas.enabled = false;
        RestartLevel();
    }
    public void WinMainMenu()
    {
        winCanvas.enabled = false;
        paused = false;

        DestroyObjects();

        Application.LoadLevel(0);
    }

    // Restarts Level
    private void RestartLevel()
    {
        evilOverlordCamera.SetCameraPosition();

        paused = false;

        ActivateCountdown();

        player1.Restart();
        player2.Restart();

        calloutScript.Restart();

        Application.LoadLevel(Application.loadedLevel);
    }
    private void ActivateCountdown()
    {
        countDownTimer = 3f;
        countDown = true;
    }
    private void DestroyObjects()
    {
        DestroyObject(menuScript.player1.gameObject);
        DestroyObject(menuScript.player2.gameObject);
        DestroyObject(menuScript);
    }

    // Countdown
    public void CountDown()
    {
        countDown = false;

        player1.active = true;
        player2.active = true;

        calloutScript.Go();
    }
}
