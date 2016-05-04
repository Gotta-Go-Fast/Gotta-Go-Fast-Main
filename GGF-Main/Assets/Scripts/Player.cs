using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public int bulletSpeed;
    public int checkPointsReached;

    public float speed;
    public float regularSpeed;
    public float maxSpeed;
    public float jumpPower;

    //Timers 
    public float paralyzedReset;
    public float paralyzedTimer;
    public float speedBoostTimer = 0;
    public float bombTimer;
    private float jumpTimer;
    private float winTimer;

    public bool active;
    public bool leader;
    public bool iMustGo;
    public bool winner;
    public bool loser;

    public bool activatedBomb;
    public bool bombUsed;

    public bool gotPickup;
    private bool gotBomb;
    private bool gotSpeedBoost;
    private bool speedBoost;
    private bool gotDoubleJump;
    private bool gotShots;
    private int shots;

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

    public CalloutScript calloutScript;

    private void Awake()
    {
        active = true;

        calloutScript = GameObject.Find("CalloutScript").GetComponent<CalloutScript>();
    }

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        bombTimer = 0;

        bulletSpeed = 15;

        paralyzedReset = 0.6f;
        paralyzedTimer = paralyzedReset;

        velocity = new Vector2(0, 0);
        speed = 8f;
        maxSpeed = 10f;
        regularSpeed = speed;
        jumpPower = 270f;
        jumpTimer = 0;
        shots = 0;

        gotDoubleJump = false;
        gotSpeedBoost = false;
        gotShots = false;
        gotBomb = false;

        airturn = false;

        checkPointsReached = 0;

        rbPlayer = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        bombAnimator = bomb.GetComponent<Animator>();
        goal = goal.GetComponent<EvilOverlordGoal>();
    }

    // Update
    private void Update()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal" + playerNumber)));

        bombAnimator.SetBool("Activated", activatedBomb);

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);

        if (active)
        {
            TurnToInputDirection();
            Jump();

            GotPickup();

            if (!iMustGo)
            {
                DoubleJump();
                SpeedBoost();
                Shoot();
                Bomb();
            }

            Winning();
        }
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
            speed = regularSpeed;
        }
    }

    // Pickups
    private void GotPickup()
    {
        if (gotBomb || gotShots || gotDoubleJump || gotSpeedBoost)
        {
            gotPickup = true;
        }
        else
        {
            gotPickup = false;
        }
    }
    private void DoubleJump()
    {
        if (jumpState && !oldJumpState && !grounded && gotDoubleJump)
        {
            rbPlayer.velocity = new Vector2(velocity.x, 0);

            rbPlayer.AddForce(Vector2.up * jumpPower);

            gotDoubleJump = false;
        }
    }
    private void SpeedBoost()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && gotSpeedBoost)
        {
            speedBoostTimer += 2.0f;

            gotSpeedBoost = false;
        }

        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            speed = maxSpeed;
        }

        if (speedBoostTimer <= 0)
        {
            speed = regularSpeed;
            speedBoostTimer = 0;
        }
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && shots > 0)
        {
            GameObject createBullet = (GameObject)Instantiate(laserBullet, firePoint.position, firePoint.rotation);

            bulletSpeed = 15;

            if (transform.localScale.x < 0)
            {
                bulletSpeed = -bulletSpeed;
            }

            createBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, laserBullet.GetComponent<Rigidbody2D>().velocity.y);

            if (shots == 2)
            {
                calloutScript.PlayerFirstShot();
            }
            if (shots == 1)
            {
                calloutScript.PlayerSecondShot(otherPlayer);
            }

            shots -= 1;

            if (shots == 0)
            {
                gotShots = false;
            }
        }
    }
    private void Bomb()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && gotBomb)
        {
            GameObject dropBomb = (GameObject)Instantiate(bomb, new Vector2(firePoint.position.x, firePoint.position.y + 0.3f), firePoint.rotation);
            gotBomb = false;
            activatedBomb = true;
            bombTimer = 0;
        }

        if (activatedBomb)
        {
            bombTimer += Time.deltaTime;

            if (bombTimer > 1.4f)
            {
                bombUsed = true;
            }

            if (bombTimer > 1.7f)
            {
                bombUsed = false;
            }

            if (bombTimer > 2.0f)
            {
                activatedBomb = false;
                bombTimer = 0;
            }
        }


    }

    private void Winning()
    {
        if (iMustGo)
        {
            rbPlayer.velocity = new Vector2(0, 10f);

            winTimer += Time.deltaTime;

            if (winTimer > 1f)
            {
                active = false;
                otherPlayer.active = false;

                winner = true;
            }
        }
    }

    // FixedUpdate
    private void FixedUpdate()
    {
        if (active)
        {
            Movement();
            SpeedLimit();
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        HitByBullet(other);
        HitByBomb(other);

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
    private void HitByBomb(Collider2D other)
    {
        if (other.gameObject.CompareTag("Explosion") && otherPlayer.bombUsed)
        {
            Destroy(other.gameObject);
            paralyzedTimer = 1;
            paralyzed = true;
            rbPlayer.velocity = new Vector2(0, 5);
        }
    }
    private void PickUpDoubleJump(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoubleJump") && !gotPickup)
        {
            other.gameObject.SetActive(false);
            gotDoubleJump = true;
        }
    }
    private void PickUpSpeedBoost(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpeedBoost") && !gotPickup)
        {
            other.gameObject.SetActive(false);
            gotSpeedBoost = true;
        }
    }
    private void PickUpAmmo(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ammo") && !gotPickup)
        {
            other.gameObject.SetActive(false);
            shots = 2;
        }
    }
    private void PickUpBomb(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb") && !gotPickup)
        {
            Destroy(other.gameObject);
            gotBomb = true;
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

    // Player collition intersects
    private void OnTriggerStay2D(Collider2D other)
    {
        CollidingBomb(other);
    }
    private void CollidingBomb(Collider2D other)
    {
        if (other.gameObject.CompareTag("Explosion") && otherPlayer.bombUsed)
        {
            paralyzedTimer = 1;
            paralyzed = true;
            rbPlayer.velocity = new Vector2(0, 5);

            Destroy(other.gameObject);
        }
    }

    public void Restart()
    {
        loser = false;
        winner = false;
        iMustGo = false;
        leader = false;

        winTimer = 0f;
    }

    // Camera detection
    private void OnBecameInvisible()
    {
        if (!otherPlayer.iMustGo && active)
        {
            active = false;
            otherPlayer.active = false;

            loser = true;
        }
    }
}


