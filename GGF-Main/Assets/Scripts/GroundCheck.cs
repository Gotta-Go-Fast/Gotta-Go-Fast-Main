using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platforms"))
        {
            player.grounded = true;
        }

        //if (player.playerNumber == 1)
        //{
        //    if (other.gameObject.CompareTag("Platforms"))
        //    {
        //        player.grounded = true;
        //    }
        //}
        //else
        //{
        //    if (other.gameObject.CompareTag("Platforms"))
        //    {
        //        player.grounded = true;
        //    }
        //}
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platforms"))
        {
            player.grounded = true;
        }

        //if (player.playerNumber == 1)
        //{
        //    if (!other.gameObject.CompareTag("Player2"))
        //    {
        //        player.grounded = true;
        //    }
        //}
        //else
        //{
        //    if (!other.gameObject.CompareTag("Player"))
        //    {
        //        player.grounded = true;
        //    }
        //}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platforms"))
        {
            player.grounded = false;
        }

        //if (player.playerNumber == 1)
        //{
        //    if (!other.gameObject.CompareTag("Player2"))
        //    {
        //        player.grounded = false;
        //    }
        //}
        //else
        //{
        //    if (!other.gameObject.CompareTag("Player"))
        //    {
        //        player.grounded = false;
        //    }
        //}
    }
}
