using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour {

    public GameObject displayContentPrefab;

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
        foreach (KeyValuePair<string, object> data in initCard.data)
        {
            GameObject content = Instantiate(displayContentPrefab);
            content.transform.SetParent(cardPanel.transform, true);
            content.transform.localPosition = new Vector3(0f, 150f + yMod);

            yMod += 30f;
        }
    }

    public void UpdateCardPanel(Card card)
    {
        if(cardPanel.transform.childCount <= 1)
        {
            InitCardPanel(card);
        }
        else
        {

        }

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
}
