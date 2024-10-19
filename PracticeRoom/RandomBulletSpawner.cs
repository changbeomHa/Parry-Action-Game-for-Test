using UnityEngine;
using System.Collections;

public class RandomBulletSpawner : MonoBehaviour
{
    // 이 코드는 본 프로젝트에서 사용 금지

    public GameObject bulletPrefab; // 총알 프리팹
    public Transform target; // 타겟 오브젝트
    public Transform firePoint; // 총알 발사 위치
    public float minFireRate = 0.5f; // 최소 발사 주기
    public float maxFireRate = 2.0f; // 최대 발사 주기
    public float bulletSpeed = 10.0f; // 총알 속도

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
