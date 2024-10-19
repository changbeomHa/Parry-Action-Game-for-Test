using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPlatform : MonoBehaviour
{
    public float speed = 2.0f; // 이동 속도
    public float resetPositionOffset = -10.0f; // 오브젝트가 재배치될 위치 (왼쪽 기준)
    public float startPositionOffset = 10.0f; // 오브젝트가 재배치될 시작 위치 (오른쪽 기준)
    public float objectSpacing = 2.0f; // 오브젝트 간 간격

    private Transform[] objects;

    private void Start()
    {
        // 자식 오브젝트들을 배열에 저장
        objects = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            objects[i] = transform.GetChild(i);
        }

        // 초기 위치 설정
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].localPosition = new Vector3(startPositionOffset + i * objectSpacing, objects[i].localPosition.y, objects[i].localPosition.z);
        }
    }

    private void Update()
    {
        // 모든 자식 오브젝트를 부모의 로컬 좌표계 기준으로 왼쪽으로 이동
        foreach (Transform obj in objects)
        {
            obj.localPosition += Vector3.left * speed * Time.deltaTime;

            // 오브젝트가 왼쪽 끝에 도달하면 오른쪽 끝으로 재배치
            if (obj.localPosition.x < resetPositionOffset)
            {
                float rightMostPositionX = GetRightMostPositionX();
                obj.localPosition = new Vector3(rightMostPositionX + objectSpacing, obj.localPosition.y, obj.localPosition.z);
            }
        }
    }

    // 가장 오른쪽에 있는 오브젝트의 x 위치를 반환 (로컬 좌표 기준)
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
