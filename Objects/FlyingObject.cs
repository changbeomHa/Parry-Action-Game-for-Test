using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingObject : MonoBehaviour
{
    public GameObject standardObject;
    private Rigidbody rb;
    private Vector3 offsetAddition;
    public float maximumSpeed = 40f;
    public float accelerationPerSecond = 8f;
    public float idleRotationSpeed = 5f;
    [Header("Offsets")]
    public Vector3 targetOffset = new Vector3(0, 10, 20); // (0, 5, 10)
    public float offsetRandomizeMagnitude = 5f;
    public float randomSwayMagnitude = 2f;
    private Vector3 determinedRandomSphere;




    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        determinedRandomSphere = Random.insideUnitSphere.normalized;
        determinedRandomSphere.x = 0;
        StartCoroutine(RandomizeOffsetAdditionRoutine());

    }

    IEnumerator RandomizeOffsetAdditionRoutine()
    {
        while (true)
        {
            offsetAddition = Random.insideUnitSphere.normalized * randomSwayMagnitude;
            offsetAddition.x = 0;
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {      
        Vector3 newPosition = transform.position;
        newPosition.x = 0f;
        transform.position = newPosition;
        rb.transform.LookAt(standardObject.transform.position);

    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = ((standardObject.transform.position + targetOffset + determinedRandomSphere * offsetRandomizeMagnitude + offsetAddition) - transform.position).normalized * maximumSpeed;

        rb.velocity = Vector3.MoveTowards(rb.velocity, targetVelocity, Time.deltaTime * accelerationPerSecond);
    }


}
