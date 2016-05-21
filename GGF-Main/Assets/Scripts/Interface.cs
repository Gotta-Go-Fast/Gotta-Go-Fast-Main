using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

    private Canvas canvas;

    private GameObject blueFrame;
    private GameObject redFrame;

    private void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();

        blueFrame = GameObject.Find("BluePlayerFrame").GetComponent<GameObject>();
        redFrame = GameObject.Find("RedPlayerFrame").GetComponent<GameObject>();
    }


    public void Doublejump(int playernumber)
    {
        if (playernumber == 1)
        {

        }
        else
        {

        }
    }
    public void Speedboost(int playernumber)
    {
        if (playernumber == 1)
        {

        }
        else
        {

        }
    }
    public void Ammo(int playernumber)
    {
        if (playernumber == 1)
        {

        }
        else
        {

        }
    }
    public void Bomb(int playernumber)
    {
        if (playernumber == 1)
        {

        }
        else
        {

        }
    }
    public void Blink(int playernumber)
    {
        if (playernumber == 1)
        {

        }
        else
        {

        }
    }    
}
