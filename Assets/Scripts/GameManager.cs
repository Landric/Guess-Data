using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject turnMask;

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
                GameObject.Find("Next Turn Button").SetActive(true);
                turnState = TurnState.READY;
                break;
            case TurnState.QUESTION:
                break;
            case TurnState.FLIP:
                break;
            case TurnState.READY:
                NextTurn();
                break;
        }
    }
}
