using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AutoBombing : MonoBehaviour
{
    public ParticleSystem DestroyEffect;
    public CinemachineImpulseSource impulseSource;

    private Material objectMaterial;
    private Color originalColor;
    public Color warningColor = Color.red;
    public float warningInterval = 0.5f; // ���� ���� ����
    private bool isWarning = false;
    public float pushDistance = 2f; // �о�� �Ÿ�
    public float pushDuration = 0.5f; // �о�� �ð�
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ�� ��Ƽ������ �������� ���� ������ �����մϴ�.
        objectMaterial = GetComponent<Renderer>().material;
        originalColor = objectMaterial.color;
        StartWarning();
        StartCoroutine(ChangeColor());
    }

    public void StartWarning()
    {
        if (!isWarning)
        {
            isWarning = true;
            
        }
    }

    // ��� ���� �Լ�
    public void StopWarning()
    {
        if (isWarning)
        {
            isWarning = false;
            StopCoroutine(ChangeColor());
            objectMaterial.color = originalColor;
        }
    }

    private IEnumerator ChangeColor()
    {
        while (isWarning)
        {
            objectMaterial.color = warningColor; // ���������� ����
            yield return new WaitForSeconds(warningInterval); // ���

            objectMaterial.color = originalColor; // ���� �������� ����
            yield return new WaitForSeconds(warningInterval); // ���
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DestoryThisObject(other.gameObject));  
        }

    }

    IEnumerator DestoryThisObject(GameObject other)
    {
        yield return new WaitForSeconds(count);
        transform.localScale = Vector3.zero;
        impulseSource.GenerateImpulseWithVelocity(new Vector3(1, 1, 1));
        DestroyEffect.Play();

        StartCoroutine(PushObject(other.transform));

        yield return new WaitForSeconds(.6f);
        gameObject.SetActive(false);
    }
    private IEnumerator PushObject(Transform obj)
    {
        Vector3 startPosition = obj.position;
        Vector3 pushDirection = (obj.position - transform.position).normalized;
        Vector3 targetPosition = startPosition + pushDirection * pushDistance;

        float elapsedTime = 0f;

        while (elapsedTime < pushDuration)
        {
            obj.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / pushDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.position = targetPosition;
        obj.GetComponent<StarterAssets.ThirdPersonController>().Falldown();
    }

}
