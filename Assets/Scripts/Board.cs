using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Board : MonoBehaviour {

    public GameObject CardPrefab;

    public int PlayerID;

    float BOARD_THICKNESS = 0.2f;

    float BOARD_PADDING = 0.2f;

    float CARD_X_PADDING = 0.1f;
    float CARD_Y_PADDING = 0.1f;

    float CARD_X_SCALE;
    float CARD_Y_SCALE;

    


    // Use this for initialization
    void Start () {

        CARD_X_SCALE = CardPrefab.transform.GetChild(0).localScale.x;
        CARD_Y_SCALE = CardPrefab.transform.GetChild(0).localScale.y;

        GameManager gm = FindObjectOfType<GameManager>();
        Queue<string> dataRows = new Queue<string>(gm.GetData().Split('\n'));
        string[] headers = dataRows.Dequeue().Split(',');

        //Calculate optimal layout
        int rows = (int)Mathf.Sqrt(dataRows.Count);
        int columns = (int)Mathf.Ceil(dataRows.Count / (float)rows);

        //Resize board to fit
        Transform boardVisual = transform.GetChild(0);
        boardVisual.localScale = new Vector3((BOARD_PADDING * 2) + columns * (CARD_X_SCALE + CARD_X_PADDING) + CARD_X_PADDING, BOARD_THICKNESS, (BOARD_PADDING * 2) + rows * (CARD_Y_SCALE + CARD_Y_PADDING) + CARD_Y_PADDING);
        transform.localPosition = new Vector3(0f, -0.1f, -4f);
        

        float yMod = -(boardVisual.localScale.z / 2) + BOARD_PADDING + CARD_Y_PADDING + CARD_Y_SCALE;
        for (int y = 0; y < rows; y++)
        {
            float xMod = -(boardVisual.localScale.x / 2) + BOARD_PADDING + CARD_X_PADDING + (CARD_X_SCALE / 2);

            for (int x = 0; x < columns; x++)
            {
                if(dataRows.Count <= 0)
                {
                    break;
                }
                GameObject cardGO = Instantiate(CardPrefab) as GameObject;
                cardGO.transform.SetParent(transform.GetChild(1), false);
                cardGO.transform.localPosition = new Vector3(xMod, 0, yMod);
                xMod += CARD_X_SCALE + CARD_X_PADDING;


                Card card = cardGO.GetComponent<Card>();

                string[] cardData = dataRows.Dequeue().Split(',');
                card.title = cardData[0];
                cardGO.GetComponentInChildren<TextMesh>().text = card.title;
                card.data = new Dictionary<string, object>();
                for (int i = 1; i < headers.Length; i++)
                {
                    
                    object data;
                    try
                    {
                        data = float.Parse(cardData[i]);
                    }
                    catch
                    {
                        data = cardData[i];
                    }
                    card.data.Add(headers[i], data);
                }
            }

            yMod += CARD_Y_SCALE + CARD_Y_PADDING;
            if (dataRows.Count <= 0)
            {
                break;
            }
        }

        //Position chosen card
        Transform slot = transform.Find("ChosenCardSlot");
        slot.localPosition = new Vector3(slot.localPosition.x, slot.localPosition.y, -boardVisual.localScale.z / 2);

        //TODO: Allow use to pick their card?
        //TODO: Tie this chosen card into the game manager
        //TODO: Create the card at random
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
