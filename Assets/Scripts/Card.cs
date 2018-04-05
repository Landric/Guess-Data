﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {

    public GameObject ContentPrefab;

    public Board board;

    Dictionary<string, object> data;

	// Use this for initialization
	void Start () {
        board = transform.parent.parent.GetComponent<Board>();

        data = new Dictionary<string, object>();
        data.Add("Foo", "Bar");
        data.Add("Fizz", 3);


        float yMod = 0;

        foreach (KeyValuePair<string, object> pair in data)
        {
            GameObject content = Instantiate(ContentPrefab) as GameObject;
            content.transform.SetParent(transform.GetChild(0), false);
            content.transform.localPosition = new Vector3(0, yMod, -0.6f);
            yMod -= 0.1f;
            
            foreach (TextMesh t in content.GetComponentsInChildren<TextMesh>())
            {
                if(t.name == "Label")
                {
                    t.text = pair.Key;
                }
                else if (t.name == "Data")
                {
                    t.text = pair.Value.ToString();
                }
            }
        }

        if (board.PlayerID == 1)
        {
            ToggleMask();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Clicked()
    {
        ToggleMask();
        //StartCoroutine(FlipDown());
    }

    void ToggleMask()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }

    IEnumerator FlipDown()
    {
        Quaternion target = Quaternion.AngleAxis(-90, Vector3.right);
        while (transform.localRotation != target)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, target, 0.1f);
            yield return new WaitForSeconds(0.001f);
        }
    }
}
