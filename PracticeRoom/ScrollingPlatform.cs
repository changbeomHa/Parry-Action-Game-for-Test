using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPlatform : MonoBehaviour
{
    public float speed = 2.0f; // �̵� �ӵ�
    public float resetPositionOffset = -10.0f; // ������Ʈ�� ���ġ�� ��ġ (���� ����)
    public float startPositionOffset = 10.0f; // ������Ʈ�� ���ġ�� ���� ��ġ (������ ����)
    public float objectSpacing = 2.0f; // ������Ʈ �� ����

    private Transform[] objects;

    private void Start()
    {
        // �ڽ� ������Ʈ���� �迭�� ����
        objects = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            objects[i] = transform.GetChild(i);
        }

        // �ʱ� ��ġ ����
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].localPosition = new Vector3(startPositionOffset + i * objectSpacing, objects[i].localPosition.y, objects[i].localPosition.z);
        }
    }

    private void Update()
    {
        // ��� �ڽ� ������Ʈ�� �θ��� ���� ��ǥ�� �������� �������� �̵�
        foreach (Transform obj in objects)
        {
            obj.localPosition += Vector3.left * speed * Time.deltaTime;

            // ������Ʈ�� ���� ���� �����ϸ� ������ ������ ���ġ
            if (obj.localPosition.x < resetPositionOffset)
            {
                float rightMostPositionX = GetRightMostPositionX();
                obj.localPosition = new Vector3(rightMostPositionX + objectSpacing, obj.localPosition.y, obj.localPosition.z);
            }
        }
    }

    // ���� �����ʿ� �ִ� ������Ʈ�� x ��ġ�� ��ȯ (���� ��ǥ ����)
    private float GetRightMostPositionX()
    {
        float rightMostPositionX = resetPositionOffset;
        foreach (Transform obj in objects)
        {
            if (obj.localPosition.x > rightMostPositionX)
            {
                rightMostPositionX = obj.localPosition.x;
            }
        }
        return rightMostPositionX;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
