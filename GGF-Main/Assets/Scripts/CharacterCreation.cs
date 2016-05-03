using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCreation : MonoBehaviour {

	private List<GameObject> models;

    private Player player;

	// Default Index of the model
	private int selectionIndex = 0;

	private void Start () 
	{
		models = new List<GameObject> ();
		foreach (Transform t in transform) 
		{
			models.Add (t.gameObject);
			t.gameObject.SetActive (false);

		}

		models [selectionIndex].SetActive (true);
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
	}

    public Player GetCharacter()
    {
        player = models[selectionIndex].GetComponent<Player>();

        return player;
    }

}
