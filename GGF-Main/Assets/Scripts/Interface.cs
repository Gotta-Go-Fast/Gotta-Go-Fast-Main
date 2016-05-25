using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interface : MonoBehaviour {

    public Canvas canvas;

    private Player player1;
    private Player player2;

    public PlayerFrame blueFrame;
    public PlayerFrame redFrame;

    private Color player1color;
    private Color player2color;

    private Text callout;
    private string clear;

    private string sorry;
    private string tookTheLead;
    private string winner;

    private string blastoise;
    private string asuka;
    private string lucas;
    private string vampire;
    private string varulv;
    private string knifeguy;

    public string player1Name;
    public string player2Name;

    private float timer;
    private float timerReset;
    private bool reset;

    private void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();

        callout = gameObject.GetComponentInChildren<Text>();

        timerReset = 3f;
        timer = timerReset;

        player1color = Color.blue;
        player2color = Color.red;

        clear = "";

        tookTheLead = "took the lead!";
        sorry = "Sorry, not sorry!";
        winner = "is the winner!";

        blastoise = "Blastoise ";
        asuka = "Asuka ";
        lucas = "Vålnaden ";
        vampire = "Den sinnesyk vampire ";
        varulv = "Varulven ";
        knifeguy = "Mr.August ";
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
                callout.color = Color.white;
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
    public void GetPlayerNames(int playerNumber, int characterIndex)
    {
        // Set Name for player1
        if (playerNumber == 1)
        {
            if (characterIndex == 0)
            {
                player1Name = varulv;
            }
            if (characterIndex == 1)
            {
                player1Name = knifeguy;
            }
            if (characterIndex == 2)
            {
                player1Name = vampire;
            }
            if (characterIndex == 3)
            {
                player1Name = lucas;
            }
            if (characterIndex == 4)
            {
                player1Name = asuka;
            }
            if (characterIndex == 5)
            {
                player1Name = blastoise;
            }
        }

        // Set Name for player2
        else if (playerNumber == 2)
        {
            if (characterIndex == 0)
            {
                player2Name = varulv;
            }
            if (characterIndex == 1)
            {
                player2Name = knifeguy;
            }
            if (characterIndex == 2)
            {
                player2Name = vampire;
            }
            if (characterIndex == 3)
            {
                player2Name = lucas;
            }
            if (characterIndex == 4)
            {
                player2Name = asuka;
            }
            if (characterIndex == 5)
            {
                player2Name = blastoise;
            }
        }
    }

    public void CalloutSorryNotSorry()
    {
        callout.color = Color.white;

        callout.text = sorry;

        timer = timerReset;
        reset = true;
    }

    public void CalloutTakingTheLead(int playerNumber)
    {
        if (playerNumber == 1)
        {
            callout.color = player1color;

            callout.text = player1Name + tookTheLead;

            timer = timerReset;
            reset = true;
        }
        else
        {
            callout.color = player2color;

            callout.text = player2Name + tookTheLead;

            timer = timerReset;
            reset = true;
        }
    }

    public void CalloutWinner(int playerNumber)
    {
        if (playerNumber == 1)
        {
            callout.color = player1color;

            callout.text = player1Name + winner;

            timer = timerReset;
            reset = true;
        }
        else
        {
            callout.color = player2color;

            callout.text = player2Name + winner;

            timer = timerReset;
            reset = true;
        }
    }

    public void Clear()
    {
        reset = false;
        timer = timerReset;
        callout.text = "";
    }
}
