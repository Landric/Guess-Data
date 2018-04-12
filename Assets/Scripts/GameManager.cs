using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject turnMask;

    public TextAsset csvFile;

    const int NumberOfPlayers = 2;
    public int CurrentPlayerID = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public string GetData()
    {
        return csvFile.text;
    }

    public void NextTurn()
    {
        turnMask.SetActive(true);
        Camera.main.transform.parent.Rotate(new Vector3(0, 180, 0));
        foreach (Card card in FindObjectsOfType<Card>())
        {
            card.ToggleMask();
        }
    }
}
