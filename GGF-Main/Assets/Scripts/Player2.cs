using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour
{

    public float maxSpeed;
    public float speed;
    public float jumpPower;
    public float speedBoostTimer = 0;

    public bool grounded;
    public int doubleJump;
    private bool HasDoubleJumped;
    private bool jumpState;
    private bool oldJumpState;
    public bool paralyzed;
    public bool autoAimed;

    private Rigidbody2D rbPlayer;
    public Player player1;
    private Animator animator;

    public Transform firePoint;
    public GameObject laserBullet;
    public bool shoot;
    public int shots;
    public float paralyzedReset;
    public float paralyzedTimer;

    void Start()
    {
        paralyzedReset = 0.6f;
        paralyzedTimer = paralyzedReset;

        maxSpeed = 6f;
        speed = 50f;
        jumpPower = 250f;
        shots = 100;

        doubleJump = 0;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();

        animator = gameObject.GetComponent<Animator>();

        player1 = player1.GetComponent<Player>();
    }

    void Update()
    {
        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            maxSpeed = 10f;
        }

        if (speedBoostTimer <= 0)
        {
            maxSpeed = 6f;
        }

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal2")));

        if (Input.GetAxis("Horizontal2") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal2") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump2");


        //DoubleJump
        if (jumpState && !oldJumpState && !grounded && (doubleJump > 0) && !HasDoubleJumped)
        {
            HasDoubleJumped = true;

            rbPlayer.AddForce(Vector2.up * jumpPower);

            doubleJump -= 1;
        }


        //SingleJump
        if (jumpState && !oldJumpState && grounded)
        {
            HasDoubleJumped = false;

            rbPlayer.AddForce(Vector2.up * jumpPower);
        }

        //Shoot
        if (Input.GetButtonDown("Fire2") && shots > 0)
        {
            shoot = true;
            Instantiate(laserBullet, firePoint.position, firePoint.rotation);
            shots -= 1;
        }
    }

    void FixedUpdate()
    {
        if (paralyzed)
        {
            paralyzedTimer -= Time.deltaTime;
            rbPlayer.velocity = new Vector2(0.0f, rbPlayer.velocity.y);
            //rbPlayer.AddForce(Vector2.left * jumpPower);
            if (paralyzedTimer < 0)
            {
                paralyzed = false;
                paralyzedTimer = paralyzedReset;
            }
        }

        float h = Input.GetAxis("Horizontal2");

        if (!paralyzed)
        {
            rbPlayer.AddForce((Vector2.right * speed) * h);
        }


        if (rbPlayer.velocity.x > maxSpeed)
        {
            rbPlayer.velocity = new Vector2(maxSpeed, rbPlayer.velocity.y);
        }

        else if (rbPlayer.velocity.x < -maxSpeed)
        {
            rbPlayer.velocity = new Vector2(-maxSpeed, rbPlayer.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoubleJump"))
        {
            other.gameObject.SetActive(false);
            doubleJump += 1;
        }

        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            other.gameObject.SetActive(false);
            speedBoostTimer = 0.8f;
        }

        if (other.gameObject.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            shots += 1;
        }
    }
}
