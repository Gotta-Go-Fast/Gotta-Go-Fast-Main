using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngameMenuScript : MonoBehaviour
{
    // Players
    public Player player1;
    public Player player2;

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

    void Start()
    {
        // Players
        player1 = player1.GetComponent<Player>();
        player2 = player2.GetComponent<Player>();

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

        paused = false;

        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
        winCanvas.enabled = false;
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
        if (player1.winner)
        {
            backgroundMusic.Pause();
            pauseMusic.Pause();
            //winMusic.UnPause();

            winCanvas.enabled = true;
            paused = true;
        }

        else if (player2.winner)
        {
            backgroundMusic.Pause();
            pauseMusic.Pause();
            //winMusic.UnPause();

            winCanvas.enabled = true;
            paused = true;
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
        Application.LoadLevel(1);

        paused = false;

        player1.Restart();
        player2.Restart();
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

        player1.Restart();
        player2.Restart();
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
        Application.LoadLevel(Application.loadedLevel);

        paused = false;

        player1.Restart();
        player2.Restart();    
    }
}
