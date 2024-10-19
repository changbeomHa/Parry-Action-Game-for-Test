using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopterIdle : MonoBehaviour
{
    // �̵� ������ �ӵ�
    public float amplitude = 1.0f; // �̵� ����
    public float speed = 1.0f; // �̵� �ӵ�

    private Vector3 startPosition;

    void Start()
    {
        // �ʱ� ��ġ ����
        startPosition = transform.position;
    }

    void Update()
    {
        // ���ο� ��ġ ���
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // ������Ʈ ��ġ ����
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
