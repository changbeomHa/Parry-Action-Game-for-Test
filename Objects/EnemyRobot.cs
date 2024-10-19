using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyRobot : MonoBehaviour
{
    public ParticleSystem DestroyEffect;
    public CinemachineImpulseSource impulseSource;

    public bool isRotation;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotation)
        {
            if (targetObject != null)
            {
                // Ÿ���� ��ġ�� ���� ������Ʈ�� y ��ǥ�� ����
                Vector3 targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z);

                // Ÿ���� �ٶ󺸵��� ����
                transform.LookAt(targetPosition);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("parryable"))
        {
            StartCoroutine("DestoryThisObject");
        }
    }

    IEnumerator DestoryThisObject()
    {
        transform.localScale = Vector3.zero;
        impulseSource.GenerateImpulseWithVelocity(new Vector3(1, 1, 1));
        DestroyEffect.Play();
        yield return new WaitForSeconds(.6f);
        gameObject.SetActive(false);
    }
}
