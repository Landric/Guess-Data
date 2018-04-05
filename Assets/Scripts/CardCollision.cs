using UnityEngine;
using System.Collections;

public class CardCollision : MonoBehaviour {

    Card card;
    int ownerID;

    GameManager gm;
    DisplayManager displayManager;

    // Use this for initialization
    void Start () {
        card = transform.parent.GetComponent<Card>();
        ownerID = card.board.PlayerID;

        gm = GameObject.Find("Managers").GetComponent<GameManager>();
        displayManager = GameObject.Find("Managers").GetComponent<DisplayManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMouseDown()
    {
        card.Clicked();
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if (ownerID == gm.CurrentPlayerID)
        {
            Debug.Log("This is my card!");
        }
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
