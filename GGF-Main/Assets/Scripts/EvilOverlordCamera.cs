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

    private Vector3 cameraV2toV3;
    private Vector3 velocity;

    private bool followBoth;
    private bool followWinner;

    private float distance;
    private float maxDistance;
    private float smoothTime;

    private void Start()
    {
        velocity = Vector3.zero;

        maxDistance = 2f;
        smoothTime = 0.2f;

        followBoth = true;

        cameraPosition = player1.position;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerDistance();

        if (player1.iMustGo || player2.iMustGo)
        {
            FollowWinner();
        }

        if (followBoth && !followWinner)
        {
            CenterPosition();
        }

        else if (!followBoth && !followWinner)
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
        if (distance > (maxDistance * 2))
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

    private void FollowWinner()
    {
        followWinner = true;

        cameraOffset.x = 0;
        cameraOffset.y = 0;
    }

    private void UpdateCamera()
    {
        if (player1.leader)
        {
            cameraPosition = player1.position - cameraOffset;
        }

        else
        {
            cameraPosition = player2.position - cameraOffset;
        }

        cameraV2toV3.x = cameraPosition.x;
        cameraV2toV3.y = cameraPosition.y;
        cameraV2toV3.z = -10f;

        transform.position = Vector3.SmoothDamp(transform.position, cameraV2toV3, ref velocity, smoothTime);

        //transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
    }
}
