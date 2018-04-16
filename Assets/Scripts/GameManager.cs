using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject turnMask;
    public GameObject nextTurnButton;

    public TextAsset csvFile;

    const int NumberOfPlayers = 2;
    public int CurrentPlayerID = 0;

    public Card[] ChosenCards;



    public enum TurnState { CHOOSE_CARD, QUESTION, FLIP, READY}

    public TurnState turnState;

    // Use this for initialization
    void Start () {
        //ChosenCards = new Card[NumberOfPlayers];
        turnState = TurnState.CHOOSE_CARD;
        StepTurn();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            GetComponent<DisplayManager>().SetDecisionPanel();
        }
    }

    public string GetData()
    {
        return csvFile.text;
    }

    public void NextTurn()
    {
        turnMask.SetActive(true);
        //TODO: Set camera position programatically
        Camera.main.transform.localPosition = new Vector3(0, 6, -10);
        Camera.main.transform.localRotation = Quaternion.Euler(50, 0, 0);
        Camera.main.transform.parent.Rotate(new Vector3(0, 180, 0));
        foreach (Card card in FindObjectsOfType<Card>())
        {
            card.ToggleMask();
        }

        CurrentPlayerID = (CurrentPlayerID + 1) % NumberOfPlayers;

        turnState = (ChosenCards[CurrentPlayerID] == null) ? TurnState.CHOOSE_CARD : TurnState.QUESTION;

        StepTurn();
    }

    public void StepTurn()
    {
        switch (turnState)
        {
            case TurnState.CHOOSE_CARD:
                nextTurnButton.SetActive(true);
                //No need to change turn state as this will happen when NextTurn() is called
                break;
            case TurnState.QUESTION:
                GameObject.Find("DecisionPanel").SetActive(false);
                GameObject.Find("Next Turn Button").SetActive(true);
                break;
            case TurnState.FLIP:
                //Should never be stepped at this point
                //They should press the "Next Turn" button instead
                throw new System.Exception("Trying to StepTurn() while turnState is TurnState.FLIP; how did this happen?!");
                break;
            
            //Not using READY state yet as there is nothing forcing a player to select all the correct cards;
            //(i.e. they are currently free to "miss" them - or even flip the correct card!!)    
            /*
            case TurnState.READY:
                NextTurn();
                break;
            */
        }
    }
}
