using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interface : MonoBehaviour {

    private Canvas canvas;

    private Player player1;
    private Player player2;

    public PlayerFrame blueFrame;
    public PlayerFrame redFrame;

    private Text callout;
    private string clear;
    private string calloutSorry;

    private float timer;
    private float timerReset;
    private bool reset;

    private void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();

        callout = gameObject.GetComponentInChildren<Text>();

        timerReset = 3f;
        timer = timerReset;

        clear = "";
        calloutSorry = "Sorry, not sorry!";
    }

    private void Update()
    {
        if (reset)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = timerReset;
                reset = false;

                callout.text = clear;
            }
        }
    }

    public void FindPlayers(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;

        player1.GetPlayerFrame(blueFrame);
        player2.GetPlayerFrame(redFrame);
    }

    public void CalloutSorryNotSorry()
    {
        callout.text = calloutSorry;

        timer = timerReset;
        reset = true;
    }
}
