using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPropellerRotation : MonoBehaviour
{
    // ������Ʈ�� ȸ�� �ӵ� (degrees per second)
    public float rotationSpeed = 45f;

    void Update()
    {
        // y���� �������� ȸ��
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
