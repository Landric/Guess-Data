using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Response : MonoBehaviour {

    public float fadeAfter;
    public float fadeDelay;

    Image image;
    Text text1, text2;

    float timer;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        text1 = GetComponentsInChildren<Text>()[0];
        text2 = GetComponentsInChildren<Text>()[1];
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= fadeAfter)
        {
            StartCoroutine(FadeOut());
        }
	}

    private void OnEnable()
    {
        timer = 0;

        Color tmp = image.color;
        tmp.a = 1f;
        image.color = tmp;

        tmp = text1.color;
        tmp.a = 1f;
        text1.color = text2.color = tmp;
    }

    public void StartFade()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while(image.color.a >= 0.01f)
        {
            Color tmp = image.color;
            tmp.a -= 0.05f;
            image.color = tmp;

            tmp = text1.color;
            tmp.a -= 0.05f;
            text1.color = text2.color = tmp;

            yield return new WaitForSeconds(fadeDelay);
        }
        gameObject.SetActive(false);
    }
}
