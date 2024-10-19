using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphaChanger : MonoBehaviour
{
    public Image image; // ���İ��� ������ �̹���
    public float duration = 1.0f; // ���İ��� ������ ���ϴµ� �ɸ��� �ð�

    void Start()
    {
        if (image != null)
        {
            StartCoroutine(ChangeAlpha());
        }
    }

    IEnumerator ChangeAlpha()
    {
        float targetAlpha = 0.156f; // 40 / 255
        float currentAlpha = 0f;
        float elapsedTime = 0f;
        bool increasing = true;

        while (true)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / duration);
                SetAlpha(alpha);
                yield return null;
            }

            elapsedTime = 0f;
            increasing = !increasing;
            currentAlpha = image.color.a;
            targetAlpha = increasing ? 0.156f : 0f;
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
