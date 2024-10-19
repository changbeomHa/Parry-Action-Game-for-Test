using UnityEngine;

public class SmoothRandomMovementTarget : MonoBehaviour
{
    public float rangeX = 5.0f; // x축으로의 이동 범위
    public float rangeZ = 5.0f; // z축으로의 이동 범위
    public float speed = 5.0f; // 이동 속도
    public float changeTargetInterval = 2.0f; // 타겟 변경 간격

    private Vector3 targetPosition; // 현재 타겟 위치
    private Vector3 startPosition; // 시작 위치
    private float journeyLength; // 이동 거리
    private float startTime; // 이동 시작 시간
    private Vector3 initialPosition; // 초기 위치

    void Start()
    {
        initialPosition = transform.position;
        SetRandomTargetPosition();
    }

    void Update()
    {
        // 시간 비율 계산 (0에서 1 사이)
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        // Ease-in-out 보간을 사용하여 위치 계산
        transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0, 1, fractionOfJourney));

        // 타겟 위치에 도달했거나 일정 시간이 지나면 새로운 타겟 위치 설정
        if (fractionOfJourney >= 1.0f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(initialPosition.x - rangeX, initialPosition.x + rangeX);
        float randomZ = Random.Range(initialPosition.z - rangeZ, initialPosition.z + rangeZ);
        startPosition = transform.position;
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
        journeyLength = Vector3.Distance(startPosition, targetPosition);
        startTime = Time.time;
    }
}
