using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineStart : MonoBehaviour
{
    public GameObject timeline;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.SetActive(true);
        }
    }
}
