using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCreation : MonoBehaviour {

	private List<GameObject> models;

    private Player player;

	// Default Index of the model
	private int selectionIndex = 0;


    // SoundEffects
    private CalloutScript calloutScript;

	private void Start () 
	{
		models = new List<GameObject> ();
		foreach (Transform t in transform) 
		{
			models.Add (t.gameObject);
			t.gameObject.SetActive (false);

		}

		models [selectionIndex].SetActive (true);


        // SoundEffects
        calloutScript = GameObject.Find("CalloutScript").GetComponent<CalloutScript>();
	}

	private void Update()
	{
		
	}

	public void Select(int index)
	{
		// If index is the same that we have we do nothing
		if (index == selectionIndex)
			return;

		// If index is outside array we do nothing
		if (index < 0 || index >= models.Count)
			return;

		models [selectionIndex].SetActive (false);
		selectionIndex = index;
		models[selectionIndex].SetActive(true);

        CharacterVoice(index);
	}

    public Player GetCharacter()
    {
        player = models[selectionIndex].GetComponent<Player>();

        return player;
    }

    private void CharacterVoice(int index)
    {
        if (index == 0)
        {
            calloutScript.RabbitVoice();
        }

        if (index == 1)
        {
            calloutScript.KnifeGuyVoice();
        }
        if (index == 2)
        {
            calloutScript.VampireVoice();
        }
        if (index == 3)
        {
            calloutScript.LucasVoice();
        }
        if (index == 4)
        {
            calloutScript.PelleVoice();
        }
        if (index == 5)
        {
            calloutScript.BlastoiseVoice();
        }

    }

}
