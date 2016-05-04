using UnityEngine;
using System.Collections;

public class EvilOverlordGoal : MonoBehaviour
{

    private Player player1;
    private Player player2;

    public Transform checkPoint1;
    public Transform checkPoint2;

    public Vector2 spawnPoint;
    public Vector2 nextCheckPoint;
    public Vector2 positionCheckPoint1;
    public Vector2 positionCheckPoint2;
    public Vector2 goalPosition;

    private Vector2 toGoalPlayer1;
    private Vector2 toGoalPlayer2;

    public bool winner;

    public float distancePlayer1;
    public float distancePlayer2;

    public int checkPointsReached;
    public static int numberOfCheckPoints;

    public MenuScript menuScript;

    // Use this for initialization
    private void Awake()
    {
        menuScript = FindObjectOfType<MenuScript>();

        player1 = menuScript.player1.GetComponent<Player>();
        player2 = menuScript.player2.GetComponent<Player>();

        player1.goal = FindObjectOfType<EvilOverlordGoal>();
        player2.goal = FindObjectOfType<EvilOverlordGoal>();

        NumberOfCheckPoints();
    }

    void Start()
    {
        goalPosition = new Vector2(transform.position.x, transform.position.y);

        checkPoint1 = checkPoint1.GetComponent<Transform>();
        checkPoint2 = checkPoint2.GetComponent<Transform>();

        positionCheckPoint1 = new Vector2(checkPoint1.position.x, checkPoint1.position.y);
        positionCheckPoint2 = new Vector2(checkPoint2.position.x, checkPoint2.position.y);

        toGoalPlayer1 = new Vector2(0, 0);
        toGoalPlayer2 = new Vector2(0, 0);
        distancePlayer1 = 0;
        distancePlayer2 = 0;
    }

    private void NumberOfCheckPoints()
    {
        checkPointsReached = 0;

        if (menuScript.levelNumber == 1)
        {
            numberOfCheckPoints = 0;
        }
        if (menuScript.levelNumber == 2)
        {
            numberOfCheckPoints = 0;
        }
        if (menuScript.levelNumber == 3)
        {
            numberOfCheckPoints = 0;
        }
        if (menuScript.levelNumber == 4)
        {
            numberOfCheckPoints = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        FindNextCheckPoint();
        Distances();

        if (!player1.iMustGo && !player2.iMustGo)
        {
            Leader();
        }
    }

    private void FindNextCheckPoint()
    {
        if (checkPointsReached == 0)
        {
            nextCheckPoint = positionCheckPoint1;
        }

        if (checkPointsReached == 1)
        {
            nextCheckPoint = positionCheckPoint2;
        }

        if (checkPointsReached == numberOfCheckPoints)
        {
            nextCheckPoint = goalPosition;
        }
    }
    private void Distances()
    {
        toGoalPlayer1 = (nextCheckPoint - player1.position);

        distancePlayer1 = Mathf.Sqrt((toGoalPlayer1.x * toGoalPlayer1.x) + (toGoalPlayer1.y * toGoalPlayer1.y));


        toGoalPlayer2 = (nextCheckPoint - player2.position);

        distancePlayer2 = Mathf.Sqrt((toGoalPlayer2.x * toGoalPlayer2.x) + (toGoalPlayer2.y * toGoalPlayer2.y));
    }
    private void Leader()
    {
        if (player1.checkPointsReached > player2.checkPointsReached)
        {
            player1.leader = true;
            player2.leader = false;
        }

        if (player1.checkPointsReached < player2.checkPointsReached)
        {
            player2.leader = true;
            player1.leader = false;
        }

        if (player1.checkPointsReached == player2.checkPointsReached)
        {
            if (distancePlayer1 < distancePlayer2)
            {
                player1.leader = true;
                player2.leader = false;
            }

            else
            {
                player2.leader = true;
                player1.leader = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") && !player2.iMustGo)
        {
            player1.iMustGo = true;
        }

        else if (other.gameObject.CompareTag("Player2") && !player1.iMustGo)
        {
            player2.iMustGo = true;
        }
    }
}
