using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public int doubleJump;
    public int shots;
    public int bulletSpeed;
    public int bombs;
    public int checkPointsReached;

    public float maxSpeed;
    public float speed;
    public float jumpPower;

    //Timers 
    public float paralyzedReset;
    public float paralyzedTimer;
    public float speedBoostTimer = 0;
    public float bombTimer;
    private float jumpTimer;


    public bool leader;
    public bool activatedBomb;
    public bool bombUsed;
    public bool shoot;
    public bool grounded;
    public bool paralyzed;
    private bool hasDoubleJumped;
    private bool jumpState;
    private bool oldJumpState;
    private bool airturn;

    public EvilOverlordGoal goal;

    public Vector2 position;
    public Vector2 velocity;

    public Player otherPlayer;
    public Transform firePoint;
    public GameObject laserBullet;
    public GameObject bomb;
    public Rigidbody2D rbPlayer;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Animator bombAnimator;

    //public BombController bombController;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        bombTimer = 0;

        bulletSpeed = 15;

        paralyzedReset = 0.6f;
        paralyzedTimer = paralyzedReset;

        velocity = new Vector2(0, 0);
        maxSpeed = 6f;
        speed = 10f;
        jumpPower = 250f;
        jumpTimer = 0;
        shots = 0;
        bombs = 0;

        airturn = false;

        checkPointsReached = 0;

        doubleJump = 0;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        bombAnimator = bomb.GetComponent<Animator>();
        goal = goal.GetComponent<EvilOverlordGoal>();
    }


    // Update
    void Update()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal" + playerNumber)));

        bombAnimator.SetBool("Activated", activatedBomb);

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);

        TurnToInputDirection();
        Jump();
        DoubleJump();
        SpeedBoost();
        Shoot();
        Bomb();
    }

    private void TurnToInputDirection()
    {
        if (!paralyzed)
        {
            if (Input.GetAxis("Horizontal" + playerNumber) < -0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Input.GetAxis("Horizontal" + playerNumber) > 0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    private void Jump()
    {
        if (jumpState && !oldJumpState && grounded && !paralyzed)
        {
            hasDoubleJumped = false;

            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
            velocity = rbPlayer.velocity;

            rbPlayer.AddForce(Vector2.up * jumpPower);
        }

        if (grounded)
        {
            airturn = false;
            speed = 10f;
        }
    }

    // Pickups
    private void DoubleJump()
    {
        if (jumpState && !oldJumpState && !grounded && (doubleJump > 0) && !hasDoubleJumped)
        {
            hasDoubleJumped = true;

            rbPlayer.AddForce(Vector2.up * jumpPower);

            doubleJump -= 1;
        }
    }
    private void SpeedBoost()
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
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && shots > 0)
        {
            shoot = true;
            GameObject createBullet = (GameObject)Instantiate(laserBullet, firePoint.position, firePoint.rotation);

            shots -= 1;

            bulletSpeed = 15;
            if (transform.localScale.x < 0)
            {
                bulletSpeed = -bulletSpeed;
            }

            createBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, laserBullet.GetComponent<Rigidbody2D>().velocity.y);
            shoot = false;
        }
    }
    private void Bomb()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && bombs > 0)
        {
            bombTimer = 0.0f;
            GameObject dropBomb = (GameObject)Instantiate(bomb, new Vector2(firePoint.position.x, firePoint.position.y + 0.3f), firePoint.rotation);
            activatedBomb = true;
            bombs--;
        }

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
        }
        else if (bombTimer > 2.0f)
        {
            bombUsed = false;
            activatedBomb = false;
        }
    }

    // FixedUpdate
    void FixedUpdate()
    {
        Movement();

        SpeedLimit();
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal" + playerNumber);

        // Cant move
        if (paralyzed)
        {
            paralyzedTimer -= Time.deltaTime;
            spriteRenderer.material.color = Color.cyan;

            rbPlayer.velocity = new Vector2(0.0f, rbPlayer.velocity.y);

            if (paralyzedTimer < 0)
            {
                paralyzed = false;
                paralyzedTimer = paralyzedReset;
            }
        }

        // Add input to force movement
        else if (!paralyzed)
        {
            velocity.x = (speed * h);
            velocity.y = rbPlayer.velocity.y;

            rbPlayer.velocity = velocity;

            spriteRenderer.material.color = Color.white;
        }

        if (!grounded && !airturn)
        {
            if (velocity.x != speed * h)
            {
                airturn = true;
                speed = 6f;
            }
        }
    }
    private void SpeedLimit()
    {
        if (rbPlayer.velocity.x > maxSpeed)
        {
            rbPlayer.velocity = new Vector2(maxSpeed, rbPlayer.velocity.y);
        }

        else if (rbPlayer.velocity.x < -maxSpeed)
        {
            rbPlayer.velocity = new Vector2(-maxSpeed, rbPlayer.velocity.y);
        }
    }


    // Player colliding with interactables
    void OnTriggerEnter2D(Collider2D other)
    {
        HitByBullet(other);

        PickUpDoubleJump(other);
        PickUpSpeedBoost(other);
        PickUpAmmo(other);
        PickUpBomb(other);

        CheckPoint1(other);
        CheckPoint2(other);

    }

    private void HitByBullet(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            paralyzed = true;
        }
    }
    private void PickUpDoubleJump(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoubleJump"))
        {
            other.gameObject.SetActive(false);
            doubleJump += 1;
        }
    }
    private void PickUpSpeedBoost(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            other.gameObject.SetActive(false);
            speedBoostTimer = 0.8f;
        }
    }
    private void PickUpAmmo(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            shots += 1;
        }
    }
    private void PickUpBomb(Collider2D other)
    {
        if (other.gameObject.CompareTag("BombPickUp"))
        {
            Destroy(other.gameObject);
            bombs++;
        }
    }

    private void CheckPoint1(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint1") && checkPointsReached == 0)
        {
            checkPointsReached++;

            if (goal.checkPointsReached == 0)
            {
                goal.checkPointsReached = 1;
            }
        }
    }
    private void CheckPoint2(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint2") && checkPointsReached == 1)
        {
            checkPointsReached++;

            if (goal.checkPointsReached == 1)
            {
                goal.checkPointsReached = 2;
            }
        }
    }

    // Player collition stay
    private void OnTriggerStay2D(Collider2D other)
    {
        CollidingBomb(other);
        CollidingPlayer(other);

    }

    private void CollidingBomb(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            if (otherPlayer.bombUsed == true)
            {
                Destroy(other.gameObject);
                paralyzedTimer = 1;
                paralyzed = true;
                rbPlayer.AddForce(new Vector2(0, 150));
                //bombTimer = 0.0f;
                //activatedBomb = false;
            }
        }
    }
    private void CollidingPlayer(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player" + playerNumber))
        {

        }
    }
}


