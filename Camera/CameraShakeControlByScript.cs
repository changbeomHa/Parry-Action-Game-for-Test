using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeControlByScript : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;
	public float shakeDuration_setting = 0.5f; // 이 스크립트를 반복 사용할 때 초기화할 기본 값

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	public bool shakeStart;

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeStart)
		{
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = shakeDuration_setting;
				camTransform.localPosition = originalPos;
				shakeStart = false;
			}
		}
	}

	public void ShakeStart()
    {
		shakeStart = true;
    }
}
