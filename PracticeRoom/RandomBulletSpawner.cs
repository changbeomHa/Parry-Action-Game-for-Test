using UnityEngine;
using System.Collections;

public class RandomBulletSpawner : MonoBehaviour
{
    // �� �ڵ�� �� ������Ʈ���� ��� ����

    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform target; // Ÿ�� ������Ʈ
    public Transform firePoint; // �Ѿ� �߻� ��ġ
    public float minFireRate = 0.5f; // �ּ� �߻� �ֱ�
    public float maxFireRate = 2.0f; // �ִ� �߻� �ֱ�
    public float bulletSpeed = 10.0f; // �Ѿ� �ӵ�

    void Start()
    {
        StartCoroutine(FireBulletsRandomly());
    }

    IEnumerator FireBulletsRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minFireRate, maxFireRate);
            yield return new WaitForSeconds(waitTime);
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && target != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 direction = (target.position - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }
    }
}
