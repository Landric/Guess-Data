﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

    public GameObject displayContentPrefab, decisionContentPrefab;

    GameObject cardPanel, decisionPanel;


	// Use this for initialization
	void Start () {
        cardPanel = GameObject.Find("CardPanel");
        decisionPanel = GameObject.Find("DecisionPanel");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitCardPanel(Card initCard)
    {
        float yMod = 0f;
        foreach (KeyValuePair<string, object> pair in initCard.data)
        {
            GameObject content = Instantiate(displayContentPrefab);
            content.transform.SetParent(cardPanel.transform, false);
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 100f + yMod);

            yMod += 30f;

            content.transform.Find("Label").GetComponent<Text>().text = pair.Key;
        }
    }

    public void UpdateCardPanel(Card card)
    {
        //If the only child is "Title", make sure to instantiate the data fields
        if(cardPanel.transform.childCount <= 1)
        {
            InitCardPanel(card);
        }
        
        //Then update the fields for the current card
        foreach (Transform child in cardPanel.transform)
        {
            if (child.name == "Title")
            {
                child.GetComponent<Text>().text = card.title;
            }
            else
            {
                child.Find("Data").GetComponent<Text>().text = card.data[child.Find("Label").GetComponent<Text>().text].ToString();
            }
        }

        //Then set all children as active
        foreach (Transform child in cardPanel.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void ClearCardPanel()
    {
        foreach (Transform child in cardPanel.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void InitDecisionPanel(Card initCard)
    {
        float yMod = 65f;

        Transform decisionCardPanel = decisionPanel.transform.Find("Card");
        foreach (KeyValuePair<string, object> pair in initCard.data)
        {
            GameObject content = Instantiate(decisionContentPrefab);
            content.transform.SetParent(decisionCardPanel, false);
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, yMod);

            yMod -= 35f;

            content.transform.Find("Label").GetComponent<Text>().text = pair.Key;

            //TODO: Attach listeners to buttons here
        }
    }

    public void SetDecisionPanel()
    {
        //If the only child is "Title", make sure to instantiate the data fields
        if (decisionPanel.transform.Find("Card").childCount <= 1)
        {
            InitDecisionPanel(FindObjectOfType<GameManager>().ChosenCards[0]);
        }

        decisionPanel.transform.Find("StringPanel").gameObject.SetActive(false);
        decisionPanel.transform.Find("NumberPanel").gameObject.SetActive(false);
        decisionPanel.transform.Find("TitlePanel").gameObject.SetActive(false);
        decisionPanel.transform.Find("InstructionText").gameObject.SetActive(false);

        decisionPanel.SetActive(true);
    }
}
