using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnMask : MonoBehaviour {

    public float switchTime;
    private float timeLeft;
    private Text timerText;

	// Use this for initialization
	void Start () {
        timerText = transform.GetChild(2).GetComponent<Text>();
	}

    private void OnEnable()
    {
        timeLeft = switchTime;
    }

    // Update is called once per frame
    void Update () {

        timeLeft -= Time.deltaTime;

        if (Input.anyKey)
        {
            timeLeft = 0f;
        }

        if (timeLeft <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            timerText.text = Mathf.RoundToInt(timeLeft).ToString();
        }
    }
}
