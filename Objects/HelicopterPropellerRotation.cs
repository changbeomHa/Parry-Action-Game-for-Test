using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPropellerRotation : MonoBehaviour
{
    // 오브젝트의 회전 속도 (degrees per second)
    public float rotationSpeed = 45f;

    void Update()
    {
        // y축을 기준으로 회전
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
