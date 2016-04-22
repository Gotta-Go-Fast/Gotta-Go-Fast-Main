using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int playerNumber;

    public float maxSpeed;
    public float speed;
    public float jumpPower;

    public bool grounded;
    public int doubleJump;
    private bool hasDoubleJumped;
    private bool jumpState;
    private bool oldJumpState;
    public bool paralyzed;

    public Rigidbody2D rbPlayer;
    private Animator animator;
    private Animator bombAnimator;

    public Transform firePoint;
    public GameObject laserBullet;
    public GameObject bomb;
    public bool shoot;
    public int shots;
    public int bulletSpeed;

    //Timers 
    public float paralyzedReset;
    public float paralyzedTimer;
    public float speedBoostTimer = 0;
    public float bombTimer;

    public bool activatedBomb;
    public bool bombUsed;
    public bool haveBomb = true;

    void Start()
    {
        bombTimer = 0.0f;

        bulletSpeed = 15;

        paralyzedReset = 0.6f;
        paralyzedTimer = paralyzedReset;

        maxSpeed = 6f;
        speed = 50f;
        jumpPower = 250f;
        shots = 100;

        doubleJump = 0;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();

        animator = gameObject.GetComponent<Animator>();

        bombAnimator = bomb.GetComponent<Animator>();
    }

    void Update()
    {

        if (activatedBomb)
        {
            bombTimer += Time.deltaTime;
        }

        if (bombTimer > 1.4f && bombTimer < 2.0f)
        {
            bombUsed = true;
            //activatedBomb = false;
            //bombTimer = 0;
        } 
        if (bombTimer < 1.4f)
        {
            bombUsed = false;
        } else if (bombTimer > 2.0f)
        {
            bombUsed = false;
            activatedBomb = false;
        }

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
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal" + playerNumber)));

        bombAnimator.SetBool("Activated", activatedBomb);

        if (Input.GetAxis("Horizontal" + playerNumber) < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal" + playerNumber) > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);


        //DoubleJump
        if (jumpState && !oldJumpState && !grounded && (doubleJump > 0) && !hasDoubleJumped)
        {
            hasDoubleJumped = true;

            rbPlayer.AddForce(Vector2.up * jumpPower);

            doubleJump -= 1;
        }


        //SingleJump
        if (jumpState && !oldJumpState && grounded)
        {
            hasDoubleJumped = false;

            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);

            rbPlayer.AddForce(Vector2.up * jumpPower);
        }

        //Shoot
        if (Input.GetButtonDown("Fire" + playerNumber) && shots > 0)
        {
            shoot = true;
            GameObject createBullet = (GameObject) Instantiate(laserBullet, firePoint.position, firePoint.rotation);

            shots -= 1;

            bulletSpeed = 15;
            if (transform.localScale.x < 0)
            {
                bulletSpeed = -bulletSpeed;
            }

            createBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, laserBullet.GetComponent<Rigidbody2D>().velocity.y);
            shoot = false;
        }

        //Drop Bomb
        if (Input.GetButtonDown("Fire" + playerNumber) && haveBomb)
        {
            bombTimer = 0.0f;
            GameObject dropBomb = (GameObject)Instantiate(bomb, new Vector2(firePoint.position.x, firePoint.position.y+0.3f), firePoint.rotation);
            activatedBomb = true;
            haveBomb = true;
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

        float h = Input.GetAxis("Horizontal" + playerNumber);

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

        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            paralyzed = true;
        }

        if (other.gameObject.CompareTag("BombPickUp"))
        {
            Destroy(other.gameObject);
            haveBomb = true;
        }

        //if (other.gameObject.CompareTag("Bomb"))
        //{
        //    if (bombUsed == true)
        //    {
        //        Destroy(other.gameObject);
        //        rbPlayer.AddForce(new Vector2(-1000, 0));
        //        //bombTimer = 0.0f;
        //        //activatedBomb = false;
        //    }
        //}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            if (bombUsed == true)
            {
                Destroy(other.gameObject);
                rbPlayer.AddForce(new Vector2(0, 150));
                //bombTimer = 0.0f;
                //activatedBomb = false;
            }
        }
    }

        //if (bombTimer > 1.6f)
        //{
        //    Destroy(other.gameObject);
        //    bombTimer = 0;
        //    activatedBomb = false;
        //}
}


