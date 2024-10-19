using UnityEngine;
using System.Collections;

public class OrbitingObject : MonoBehaviour
{
    public Transform center; // �߽� ������Ʈ
    public Transform target; // �ٶ� Ÿ�� ������Ʈ
    public float radius = 5.0f; // ȸ�� �ݰ�
    public float rotationAngle = 90.0f; // ȸ���� ����
    public float normalSpeed = 30.0f; // ���� ȸ�� �ӵ� (��/��)
    public float slowSpeed = 15.0f; // ���� ȸ�� �ӵ� (��/��)
    public float slowDownDuration = 1.0f; // ���� ���·� ������ �ð�

    private float currentAngle = 0.0f; // ���� ����

    public void StartRotation()
    {
        StartCoroutine(RotateAroundCenter());
    }

    void Update()
    {
        // Ÿ���� �׻� �ٶ󺸵��� ����
        if (target != null)
        {
            transform.LookAt(target);
        }
    }

    IEnumerator RotateAroundCenter()
    {
        while (true)
        {
            // ���� �ӵ��� ȸ��
            yield return StartCoroutine(RotateWithEase(rotationAngle, normalSpeed));

            // ���� �ӵ��� ȸ��
            yield return StartCoroutine(RotateWithEase(rotationAngle, slowSpeed, slowDownDuration));

            // ���� �ӵ��� �ٽ� ȸ��
            yield return StartCoroutine(RotateWithEase(rotationAngle, normalSpeed));
        }
    }

    IEnumerator RotateWithEase(float angle, float speed, float? slowDuration = null)
    {
        float startAngle = currentAngle;
        float endAngle = startAngle + angle;
        float duration = angle / speed; // �־��� �ӵ��� ȸ���ϴµ� �ɸ��� �ð�
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = Mathf.SmoothStep(0, 1, t);
            currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            Vector3 offset = new Vector3(Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0, Mathf.Cos(currentAngle * Mathf.Deg2Rad)) * radius;
            transform.position = center.position + offset;

            yield return null;
        }

        // Ensure final position is exact
        currentAngle = endAngle % 360;
        Vector3 finalOffset = new Vector3(Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0, Mathf.Cos(currentAngle * Mathf.Deg2Rad)) * radius;
        transform.position = center.position + finalOffset;

        // Slow down for the specified duration if provided
        if (slowDuration.HasValue)
        {
            yield return new WaitForSeconds(slowDuration.Value);
        }
    }
}
