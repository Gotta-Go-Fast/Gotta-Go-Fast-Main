using UnityEngine;
using System.Collections;

public class EvilOverlordCamera : MonoBehaviour
{
    public Player player1;
    public Player player2;

    private Vector2 playerDistance;
    private Vector2 cameraPosition;
    private Vector2 leaderPosition;
    private Vector2 cameraOffset;
    private Vector2 directionLosingPlayer;

    private bool followBoth;

    public float distance;
    private float maxDistance;

    private void Start()
    {
        maxDistance = 5;

        followBoth = true;

        cameraPosition = player1.position;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerDistance();

        if (followBoth)
        {
            CenterPosition();
        }

        else
        {
            FollowLeader();
        }

        UpdateCamera();
    }

    // Creating a vector between the two players, and calculating the distance to a float
    private void PlayerDistance()
    {
        if (player1.leader)
        {
            playerDistance = (player1.position - player2.position);
        }

        else
        {
            playerDistance = (player2.position - player1.position);
        }

        distance = Mathf.Sqrt((playerDistance.x * playerDistance.x) + (playerDistance.y * playerDistance.y));

        // Tells the camera to follow the leader if the players get to far apart
        if (distance > maxDistance)
        {
            followBoth = false;
        }
        
        else
        {
            followBoth = true;
        }
    }

    // Calculating camera offset to follow both players
    private void CenterPosition()
    {
        cameraOffset = playerDistance / 2;
    }

    // Calculating camera offset to follow the leader
    private void FollowLeader()
    {
        directionLosingPlayer = playerDistance / (Mathf.Sqrt((playerDistance.x * playerDistance.x) + (playerDistance.y * playerDistance.y)));

        cameraOffset = directionLosingPlayer * maxDistance;
    }

    private void UpdateCamera()
    {
        if (player1.leader)
        {
            cameraPosition = player1.position - cameraOffset;

            transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
        }

        else
        {
            cameraPosition = player2.position - cameraOffset;

            transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
        }
    }
}
