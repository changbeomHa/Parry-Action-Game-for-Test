using UnityEngine;
using System.Collections;

public class OrbitingObject : MonoBehaviour
{
    public Transform center; // 중심 오브젝트
    public Transform target; // 바라볼 타겟 오브젝트
    public float radius = 5.0f; // 회전 반경
    public float rotationAngle = 90.0f; // 회전할 각도
    public float normalSpeed = 30.0f; // 정상 회전 속도 (도/초)
    public float slowSpeed = 15.0f; // 느린 회전 속도 (도/초)
    public float slowDownDuration = 1.0f; // 느린 상태로 유지할 시간

    private float currentAngle = 0.0f; // 현재 각도

    public void StartRotation()
    {
        StartCoroutine(RotateAroundCenter());
    }

    void Update()
    {
        // 타겟을 항상 바라보도록 설정
        if (target != null)
        {
            transform.LookAt(target);
        }
    }

    IEnumerator RotateAroundCenter()
    {
        while (true)
        {
            // 정상 속도로 회전
            yield return StartCoroutine(RotateWithEase(rotationAngle, normalSpeed));

            // 느린 속도로 회전
            yield return StartCoroutine(RotateWithEase(rotationAngle, slowSpeed, slowDownDuration));

            // 정상 속도로 다시 회전
            yield return StartCoroutine(RotateWithEase(rotationAngle, normalSpeed));
        }
    }

    IEnumerator RotateWithEase(float angle, float speed, float? slowDuration = null)
    {
        float startAngle = currentAngle;
        float endAngle = startAngle + angle;
        float duration = angle / speed; // 주어진 속도로 회전하는데 걸리는 시간
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
