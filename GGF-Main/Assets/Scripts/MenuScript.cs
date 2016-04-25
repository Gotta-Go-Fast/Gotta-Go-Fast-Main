using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Button startGame;

    public Canvas mainCanvas;
    public Canvas charCanvas;



    // Use this for initialization
    void Start ()
    {
        startGame = startGame.GetComponent<Button>();

        mainCanvas = mainCanvas.GetComponent<Canvas>();

        charCanvas = charCanvas.GetComponent<Canvas>();


        mainCanvas.enabled = true;
        charCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {

	}

    public void StartPress()
    {
        mainCanvas.enabled = false;
        charCanvas.enabled = true;
    }


}
