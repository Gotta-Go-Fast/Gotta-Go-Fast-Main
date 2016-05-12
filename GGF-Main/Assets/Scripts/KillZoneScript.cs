using UnityEngine;
using System.Collections;

public class KillZoneScript : MonoBehaviour {

    private MenuScript menuScript;

    private void Awake()
    {
        menuScript = FindObjectOfType<MenuScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            menuScript.player1.loser = true;

        }

        if (other.gameObject.CompareTag("Player2"))
        {
            menuScript.player2.loser = true;
        }
    }
}
