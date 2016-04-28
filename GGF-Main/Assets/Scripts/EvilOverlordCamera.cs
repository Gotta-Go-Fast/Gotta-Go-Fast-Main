using UnityEngine;
using System.Collections;

public class EvilOverlordCamera : MonoBehaviour
{
    public Player player1;
    public Player player2;

    private Vector2 cameraPosition;

    private bool followBoth;

    private float maxDistance;

    private void Start()
    {
        maxDistance = 100;

        followBoth = true;

        cameraPosition = new Vector2(player1.transform.position.x, player1.transform.position.y);
    }

    // Update is called once per frame
    private void Update()
    {
        //cameraPosition.x = 
        
        //cameraPosition.y = 

        //transform.position = cameraPosition;
    }
}
