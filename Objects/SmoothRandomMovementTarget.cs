using UnityEngine;

public class SmoothRandomMovementTarget : MonoBehaviour
{
    public float rangeX = 5.0f; // x�������� �̵� ����
    public float rangeZ = 5.0f; // z�������� �̵� ����
    public float speed = 5.0f; // �̵� �ӵ�
    public float changeTargetInterval = 2.0f; // Ÿ�� ���� ����

    private Vector3 targetPosition; // ���� Ÿ�� ��ġ
    private Vector3 startPosition; // ���� ��ġ
    private float journeyLength; // �̵� �Ÿ�
    private float startTime; // �̵� ���� �ð�
    private Vector3 initialPosition; // �ʱ� ��ġ

    void Start()
    {
        initialPosition = transform.position;
        SetRandomTargetPosition();
    }

    void Update()
    {
        // �ð� ���� ��� (0���� 1 ����)
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        // Ease-in-out ������ ����Ͽ� ��ġ ���
        transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0, 1, fractionOfJourney));

        // Ÿ�� ��ġ�� �����߰ų� ���� �ð��� ������ ���ο� Ÿ�� ��ġ ����
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
