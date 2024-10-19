using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.VFX;

public class ParryableZone : MonoBehaviour
{
    public Transform playerTransform;
    public float bounceForce = 20f;

    public bool objectInTrigger = false;
    public CinemachineImpulseSource impulseSource;
    private GameObject objectToBounce;

    [Header("VFX")]
    [SerializeField] ParticleSystem ParryingEffect;


    private void Update()
    {
        if (objectToBounce != null)
        {
            if (objectToBounce.GetComponent<Bullet>().activate == false)
            {
                objectInTrigger = false;
                objectToBounce = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 오브젝트가 트리거 존에 들어왔을 때 감지
        if (other.CompareTag("parryable"))
        {
            objectInTrigger = true;
            objectToBounce = other.gameObject;        
        }


    }

    void OnTriggerExit(Collider other)
    {
        // 오브젝트가 트리거 존을 떠났을 때 감지
        if (other.CompareTag("parryable"))
        {
            objectInTrigger = false;
            objectToBounce = null;
        }
    }

    public void ParryObject()
    {
        Debug.Log("check");
        // 플레이어가 바라보는 방향으로 튕겨나가도록 설정
        Vector3 bounceDirection = playerTransform.forward;

        if (objectToBounce != null)
        {
            impulseSource.GenerateImpulseWithVelocity(bounceDirection);
            //impulseSource.m_ImpulseDefinition.m_AmplitudeGain = Mathf.Max(3, 1 * 2);
            ParryingEffect.Play();
            StartCoroutine(ParryingHit());
            objectToBounce.GetComponent<Bullet>().bulletRigidbody.velocity = playerTransform.forward * bounceForce;       
        }
    }


    public void LongParryObject(Vector3 parryDirection)
    {

        // 플레이어가 바라보는 방향으로 튕겨나가도록 설정
        Vector3 bounceDirection = playerTransform.forward;

        if (objectToBounce != null)
        {
            Debug.Log("check");

            impulseSource.GenerateImpulseWithVelocity(bounceDirection);
            //impulseSource.m_ImpulseDefinition.m_AmplitudeGain = Mathf.Max(3, 1 * 2);
            ParryingEffect.Play();
            StartCoroutine(ParryingHit());
            objectToBounce.GetComponent<Bullet>().bulletRigidbody.velocity = parryDirection * (bounceForce * .1f);
        }
    }

    IEnumerator ParryingHit()
    {
        yield return new WaitForSecondsRealtime(.05f);
        Time.timeScale = .1f;
        yield return new WaitForSecondsRealtime(.1f);
        Time.timeScale = 1f;
    }


}
