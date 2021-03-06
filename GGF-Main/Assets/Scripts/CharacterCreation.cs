﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCreation : MonoBehaviour {

	private List<GameObject> models;

    public Player player;

	// Default Index of the model
	public int selectionIndex;

    // SoundEffects
    private CalloutScript calloutScript;
    private MenuScript menuScript;
    public CharacterCreation otherCharacter;

	private void Start () 
	{
		models = new List<GameObject> ();
		foreach (Transform t in transform) 
		{
			models.Add (t.gameObject);
			t.gameObject.SetActive (false);
		}

		models [selectionIndex].SetActive (true);

        player = models[selectionIndex].GetComponent<Player>();

        // SoundEffects
        calloutScript = GameObject.Find("CalloutScript").GetComponent<CalloutScript>();

        menuScript = GameObject.Find("MenuScript").GetComponent<MenuScript>();
    }

	public void Select(int index)
	{
        if (index != otherCharacter.selectionIndex)
        {
            // If index is the same that we have we do nothing
            if (index == selectionIndex)
                return;

            // If index is outside array we do nothing
            if (index < 0 || index >= models.Count)
                return;

            models[selectionIndex].SetActive(false);
            selectionIndex = index;
            models[selectionIndex].SetActive(true);

            CharacterVoice(index);

            player = models[selectionIndex].GetComponent<Player>();
        }
	}

    public int SelectedCharacterIndex()
    {
        return selectionIndex;
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
