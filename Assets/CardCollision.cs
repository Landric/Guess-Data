using UnityEngine;
using System.Collections;

public class CardCollision : MonoBehaviour {

    Card card;

	// Use this for initialization
	void Start () {
        card = transform.parent.GetComponent<Card>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMouseDown()
    {
        card.Clicked();
    }
}
