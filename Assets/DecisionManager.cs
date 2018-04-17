using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour {
    public GameObject decisionContentPrefab;
    public GameObject DecisionPanel, DecisionCard, StringPanel, NumberPanel, TitlePanel, AskButton, InstructionText, DecisionResponse;

    Dropdown stringDropdown, numberDropdown, titleDropdown;
    InputField numberInput;

    GameManager gm;

    enum Panel { StringPanel, NumberPanel, TitlePanel};
    Panel selectedPanel;
    string selectedData;


	// Use this for initialization
	void Start () {
        stringDropdown = StringPanel.transform.GetChild(0).GetComponent<Dropdown>();
        numberDropdown = NumberPanel.transform.GetChild(0).GetComponent<Dropdown>();
        numberInput = NumberPanel.transform.GetChild(1).GetComponent<InputField>();
        titleDropdown = TitlePanel.transform.GetChild(0).GetComponent<Dropdown>();

        gm = GetComponent<GameManager>();
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

            //TODO: Populate dropdown menus here
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
                throw new Exception("Unexpected data type: " + pair.Value.GetType());
            }
        }

        //TODO: Populate title dropdown menus here
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
        DecisionResponse.SetActive(false);
    }

    public void ShowStringPanel(string key)
    {
        selectedPanel = Panel.StringPanel;
        selectedData = key;

        StringPanel.SetActive(true);
        NumberPanel.SetActive(false);
        TitlePanel.SetActive(false);

        AskButton.SetActive(true);
        InstructionText.SetActive(false);
    }

    public void ShowNumberPanel(string key)
    {
        selectedPanel = Panel.NumberPanel;
        selectedData = key;

        StringPanel.SetActive(false);
        NumberPanel.SetActive(true);
        TitlePanel.SetActive(false);

        AskButton.SetActive(true);
        InstructionText.SetActive(false);
    }

    public void ShowTitlePanel()
    {
        selectedPanel = Panel.TitlePanel;
        selectedData = "title";

        StringPanel.SetActive(false);
        NumberPanel.SetActive(false);
        TitlePanel.SetActive(true);

        AskButton.SetActive(true);
        InstructionText.SetActive(false);
    }

    public void Ask()
    {
        string oprtr = "equals";
        object guess = "";

        switch (selectedPanel)
        {
            case Panel.StringPanel:
                guess = stringDropdown.options[stringDropdown.value].text;
                break;

            case Panel.NumberPanel:
                guess = float.Parse(numberInput.text);
                switch (numberDropdown.value)
                {
                    case 0:
                        oprtr = "less than";
                        break;
                    case 1:
                        oprtr = "equals";
                        break;
                    case 2:
                        oprtr = "greater than";
                        break;
                }
                break;

            case Panel.TitlePanel:
                guess = titleDropdown.options[titleDropdown.value].text;
                break;
        }

        if (selectedPanel == Panel.TitlePanel && (string)guess == gm.ChosenCards[(gm.CurrentPlayerID + 1) % GameManager.NumberOfPlayers].title)
        {
            //TODO: Win condition?
        }
        else
        {
            bool correct;

            object data = gm.ChosenCards[(gm.CurrentPlayerID + 1) % GameManager.NumberOfPlayers].data[selectedData];

            switch (oprtr)
            {
                case "less than":
                    correct = (float)data < (float)guess;
                    break;
                case "equals":
                    correct = data == guess;
                    break;
                case "greater than":
                    correct = (float)data > (float)guess;
                    break;
                default:
                    throw new Exception("Unexpected operator name: " + oprtr);
            }

             

            string not = (correct) ? "IS" : "is NOT";

            DecisionResponse.GetComponentInChildren<Text>().text = "Your opponent's repsone: Their card's " + selectedData + " " + not + " " + oprtr + " " + guess;
            DecisionResponse.SetActive(true);
            DecisionPanel.SetActive(false);
            gm.nextTurnButton.SetActive(true);
        }
    }
}
