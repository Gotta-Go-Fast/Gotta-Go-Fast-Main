using UnityEngine;
using System.Collections;

public class EvilOverlordGoal : MonoBehaviour {

    public Player player1;
    public Player player2;

    private Vector2 goalPosition;

    private Vector2 toGoalPlayer1;
    private Vector2 toGoalPlayer2;

    private float distancePlayer1;
    private float distancePlayer2;


	// Use this for initialization
	void Start ()
    {
        goalPosition = new Vector2(transform.position.x, transform.position.y);

        toGoalPlayer1 = new Vector2(0, 0);
        toGoalPlayer2 = new Vector2(0, 0);

        distancePlayer1 = 0;
        distancePlayer2 = 0;

        player1.leader = true;
        player2.leader = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Distances();
        Leader();
	}

    private void Distances()
    {
        toGoalPlayer1.x = (goalPosition.x - player1.transform.position.x);
        toGoalPlayer1.y = (goalPosition.y - player1.transform.position.y);

        distancePlayer1 = Mathf.Sqrt((toGoalPlayer1.x * toGoalPlayer1.x) + (toGoalPlayer1.y * toGoalPlayer1.y));


        toGoalPlayer2.x = (goalPosition.x - player2.transform.position.x);
        toGoalPlayer2.y = (goalPosition.y - player2.transform.position.y);

        distancePlayer2 = Mathf.Sqrt((toGoalPlayer2.x * toGoalPlayer2.x) + (toGoalPlayer2.y * toGoalPlayer2.y));

    }

    private void Leader()
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
