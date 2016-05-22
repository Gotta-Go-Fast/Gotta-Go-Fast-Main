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
    public bool gameOn;

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
    private Interface GUI;
    private PlayerFrame playerFrame;

    private void Awake()
    {
        active = true;

        calloutScript = GameObject.Find("CalloutScript").GetComponent<CalloutScript>();

        GUI = GameObject.Find("GUI").GetComponent<Interface>();
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
    }

    // Update
    private void Update()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));


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

            calloutScript.Doublejump();

            playerFrame.RemoveDoublejump();
        }
    }
    private void SpeedBoost()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && gotSpeedBoost)
        {
            speedBoostTimer += 2.0f;

            gotSpeedBoost = false;

            playerFrame.RemoveSpeedboost();

            calloutScript.Speedboost();
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
                calloutScript.PlayerFirstShot(otherPlayer);
            }
            if (shots == 1)
            {
                calloutScript.PlayerSecondShot(otherPlayer);
            }

            calloutScript.Shoot();

            shots -= 1;

            if (shots == 0)
            {
                gotShots = false;

                playerFrame.RemoveAmmo();
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

            dropBomb.GetComponent<Animator>().SetTrigger("Activate");
            bombTimer = 0;

            calloutScript.PlaceBomb();

            playerFrame.RemoveBomb();
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

        if (iMustGo)
        {
            speed = 3f;
        }

        // Cant move
        if (paralyzed)
        {
            paralyzedTimer -= Time.deltaTime;
            spriteRenderer.material.color = Color.cyan;

            rbPlayer.velocity = new Vector2(0.0f, rbPlayer.velocity.y);

            if (paralyzedTimer < 0 && grounded)
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
        PickUpBlink(other);

        CheckPoint1(other);
        CheckPoint2(other);

        OutOfBounds(other);
    }

    private void HitByBullet(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            paralyzed = true;

            calloutScript.PlayerHit();

            GUI.CalloutSorryNotSorry();
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

            playerFrame.Doublejump();

            calloutScript.PickupJump();
        }
    }
    private void PickUpSpeedBoost(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpeedBoost") && !gotPickup)
        {
            other.gameObject.SetActive(false);
            gotSpeedBoost = true;

            playerFrame.Speedboost();

            calloutScript.PickupBoost();
        }
    }
    private void PickUpAmmo(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ammo") && !gotPickup)
        {
            other.gameObject.SetActive(false);
            gotShots = true;
            shots = 2;

            playerFrame.Ammo();

            calloutScript.PickupShots();
        }
    }
    private void PickUpBomb(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb") && !gotPickup)
        {
            Destroy(other.gameObject);
            gotBomb = true;

            playerFrame.Bomb();

            calloutScript.PickupBomb();
        }
    }
    private void PickUpBlink(Collider2D other)
    {
        if (other.gameObject.CompareTag("Blink") && !gotPickup)
        {
            Destroy(other.gameObject);
            gotBomb = true;

            playerFrame.Blink();
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
    private void OutOfBounds(Collider2D other)
    {
        if (other.gameObject.CompareTag("Out"))
        {
            PlayerOutOfScreen();

            Debug.Log("KillZone");
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
        gotDoubleJump = false;
        gotSpeedBoost = false;
        gotBomb = false;
        gotShots = false;
        shots = 0;

        paralyzed = false;

        active = false;
        loser = false;
        winner = false;
        iMustGo = false;
        leader = false;

        winTimer = 0f;

        rbPlayer.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(1, 1, 1);

        playerFrame.ClearPickups();
    }

    public void ClearGUI()
    {
        playerFrame.ClearFrame();
        playerFrame.ClearPickups();
    }

    // Camera detection
    private void OnBecameInvisible()
    {
        if (gameOn)
            PlayerOutOfScreen();
    }

    public void GetPlayerFrame(PlayerFrame playerFrame)
    {
        this.playerFrame = playerFrame;
    }

    private void PlayerOutOfScreen()
    {

        if (!otherPlayer.iMustGo && !otherPlayer.loser && active)
        {
            loser = true;
            otherPlayer.winner = true;

            if (paralyzed)
            {
                calloutScript.ParalyzedLoss();

                spriteRenderer.material.color = Color.white;
            }
            else if (!paralyzed)
            {
                calloutScript.Losing();

                Debug.Log("Loser");
            }

            active = false;
            otherPlayer.active = false;
        }
    }

    public void GetPortrait(int index)
    {
        playerFrame.GetPortrait(index);
    }
}


