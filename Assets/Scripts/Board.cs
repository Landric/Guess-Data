using UnityEngine;
using System.Collections;

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

        int number = 12; //Replace with LoadCSVData() etc.
        
        //Calculate optimal layout
        int rows = (int)Mathf.Sqrt(number);
        int columns = (int)Mathf.Ceil(number / (float)rows);

        //Resize board to fit
        Transform boardVisual = transform.GetChild(0);
        boardVisual.localScale = new Vector3((BOARD_PADDING * 2) + columns * (CARD_X_SCALE + CARD_X_PADDING) + CARD_X_PADDING, BOARD_THICKNESS, (BOARD_PADDING * 2) + rows * (CARD_Y_SCALE + CARD_Y_PADDING) + CARD_Y_PADDING);
        transform.localPosition = new Vector3(0f, -0.1f, -4f);

        //Instantiate cards
        float yMod = -(boardVisual.localScale.z / 2) + BOARD_PADDING + CARD_Y_PADDING + CARD_Y_SCALE;
        for (int y = 0; y < rows; y++)
        {
            float xMod = -(boardVisual.localScale.x / 2) + BOARD_PADDING + CARD_X_PADDING + (CARD_X_SCALE / 2);

            for (int x = 0; x < columns; x++)
            {
                GameObject cardGO = Instantiate(CardPrefab) as GameObject;
                cardGO.transform.SetParent(transform.GetChild(1), false);
                cardGO.transform.localPosition = new Vector3(xMod, 0, yMod);
                xMod += CARD_X_SCALE + CARD_X_PADDING;
                cardGO.GetComponentInChildren<TextMesh>().text = x.ToString() + "," + y.ToString();
            }

            yMod += CARD_Y_SCALE + CARD_Y_PADDING;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
