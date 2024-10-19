using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopterIdle : MonoBehaviour
{
    // 이동 범위와 속도
    public float amplitude = 1.0f; // 이동 범위
    public float speed = 1.0f; // 이동 속도

    private Vector3 startPosition;

    void Start()
    {
        // 초기 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // 새로운 위치 계산
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // 오브젝트 위치 갱신
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
