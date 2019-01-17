using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWarningBehaviour : MonoBehaviour
{

    Text bannerText;
    public float fadeOutTime;
    void Start()
    {
        bannerText = this.gameObject.GetComponent<Text>();
        bannerText.color = Color.white;
        bannerText.text = "Find the researched Ship";
        StartCoroutine(FadeOutRoutine());

    }

    public void SetText(string s, Color color)
    {
        bannerText.color = color;
        bannerText.text = s;
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    {
        Text text = GetComponent<Text>();
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
        bannerText.text = "";
    }

}
