using UnityEngine;

public class ClothWind : MonoBehaviour
{
    public WindZone windZone;
    private Cloth cloth;
    private ClothSphereColliderPair[] originalColliders;

    void Start()
    {
        cloth = GetComponent<Cloth>();

        if (cloth == null)
        {
            Debug.LogError("Cloth component not found on " + gameObject.name);
            return;
        }

        originalColliders = cloth.sphereColliders;
    }

    void Update()
    {
        if (windZone != null && cloth != null)
        {
            Vector3 windDirection = windZone.transform.forward;
            float windStrength = windZone.windMain;

            // Add turbulence effect
            windStrength += Mathf.PerlinNoise(Time.time, 0.0f) * windZone.windTurbulence;

            Vector3 windForce = windDirection * windStrength;
            ApplyWindToCloth(windForce);
        }
    }

    void ApplyWindToCloth(Vector3 windForce)
    {
        // Convert windForce to local space if needed
        Vector3 localWindForce = transform.InverseTransformDirection(windForce);

        for (int i = 0; i < cloth.vertices.Length; i++)
        {
            // Add wind force to each vertex of the cloth
            cloth.externalAcceleration = localWindForce;
        }
    }
}
