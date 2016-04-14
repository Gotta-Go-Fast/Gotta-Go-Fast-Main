using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{

    public float player1BulletSpeed;
    public float player2BulletSpeed;
    public Player player;
    public Player2 player2;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();

        if (player.shoot)
        {
            if (player.transform.localScale.x < 0)
            {
                player1BulletSpeed = -player1BulletSpeed;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(player1BulletSpeed, GetComponent<Rigidbody2D>().velocity.y);

            player.shoot = false;
        }

        if (player2.shoot)
        {
            if (player2.transform.localScale.x < 0)
            {
                player2BulletSpeed = -player2BulletSpeed;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(player2BulletSpeed, GetComponent<Rigidbody2D>().velocity.y);

            player2.shoot = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(player1BulletSpeed, GetComponent<Rigidbody2D>().velocity.y);
        //GetComponent<Rigidbody2D>().velocity = new Vector2(player2BulletSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == ("Player2"))
        {
            player2.paralyzed = true;
            Destroy(gameObject);
        }

        if (otherCollider.tag == ("Player1"))
        {
            player.paralyzed = true;
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }


}
