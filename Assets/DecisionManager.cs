using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour {
    public GameObject decisionContentPrefab;
    public GameObject DecisionPanel, DecisionCard, StringPanel, NumberPanel, TitlePanel, AskButton, InstructionText;

    string selectedData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitDecisionPanel(Card initCard)
    {
        float yMod = 65f;

        foreach (KeyValuePair<string, object> pair in initCard.data)
        {
            GameObject content = Instantiate(decisionContentPrefab);
            content.transform.SetParent(DecisionCard.transform, false);
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, yMod);

            yMod -= 35f;

            content.transform.Find("Label").GetComponent<Text>().text = pair.Key;

            //TODO: Attach listeners to buttons here
            if (pair.Value is string)
            {
                content.GetComponent<Button>().onClick.AddListener(() => ShowStringPanel(pair.Key));
            }
            else if (pair.Value is float)
            {
                content.GetComponent<Button>().onClick.AddListener(() => ShowNumberPanel(pair.Key));
            }
            else
            {
                throw new System.Exception("Unexpected data type: " + pair.Value.GetType());
            }

        }
    }

    public void OpenPanel()
    {
        //If the only child is "Title", make sure to instantiate the data fields
        if (DecisionCard.transform.childCount <= 1)
        {
            InitDecisionPanel(FindObjectOfType<GameManager>().ChosenCards[0]);
        }

        StringPanel.SetActive(false);
        NumberPanel.SetActive(false);
        TitlePanel.SetActive(false);

        AskButton.SetActive(false);
        InstructionText.SetActive(true);

        DecisionPanel.SetActive(true);
    }

    public void ShowStringPanel(string key)
    {
        selectedData = key;

        StringPanel.SetActive(true);
        NumberPanel.SetActive(false);
        TitlePanel.SetActive(false);

        AskButton.SetActive(true);
        InstructionText.SetActive(false);
    }

    public void ShowNumberPanel(string key)
    {
        selectedData = key;

        StringPanel.SetActive(false);
        NumberPanel.SetActive(true);
        TitlePanel.SetActive(false);

        AskButton.SetActive(true);
        InstructionText.SetActive(false);
    }

    public void ShowTitlePanel()
    {
        selectedData = "title";

        StringPanel.SetActive(false);
        NumberPanel.SetActive(false);
        TitlePanel.SetActive(true);

        AskButton.SetActive(true);
        InstructionText.SetActive(false);
    }

    public void Ask()
    {

    }
}
