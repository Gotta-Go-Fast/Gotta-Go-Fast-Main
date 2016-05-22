using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

    private Canvas canvas;

    private Player player1;
    private Player player2;

    public PlayerFrame blueFrame;
    public PlayerFrame redFrame;

    private void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
    }

    public void FindPlayers(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;

        player1.GetPlayerFrame(blueFrame);
        player2.GetPlayerFrame(redFrame);
    }
}
