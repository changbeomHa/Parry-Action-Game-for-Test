using System.Collections;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float grappleRange = 10f;  // 그랩 훅의 최대 사거리
    public float grappleSpeed = 10f;  // 플레이어가 그랩 포인트로 끌려가는 기본 속도
    public float projectileSpeed = 20f; // 그랩 프로젝타일의 속도
    public LayerMask grappleLayerMask; // 그랩할 수 있는 오브젝트를 필터링할 레이어 마스크
    public GameObject projectile; // 그랩 프로젝타일 오브젝트
    public LineRenderer lineRenderer; // 로프를 시각화하기 위한 라인 렌더러

    private bool isGrappling = false;
    private Vector3 grapplePoint;
    private float pullStartTime;
    public GameObject weapon;
    public ParticleSystem shotEffect;

    public Animator anim;

    [SerializeField] GrappleProjectile grappleProjectile;

    void Start()
    {
        grappleProjectile.Initialize(this);
        projectile.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShootProjectileReady());
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ResetGrapple();
        }

        if (isGrappling)
        {
            float timeSinceStarted = Time.time - pullStartTime;
            float currentSpeed = grappleSpeed * (1 + timeSinceStarted); // 속도 증가 효과
            float step = currentSpeed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, grapplePoint, step);
            newPosition.y = transform.position.y; // y축 고정
            transform.position = newPosition;

            if (Vector3.Distance(transform.position, grapplePoint) < 0.1f)
            {
                isGrappling = false;
                lineRenderer.enabled = false;
                projectile.SetActive(false);
            }
        }

        if (projectile.activeSelf)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, projectile.transform.position);
        }
    }

    IEnumerator ShootProjectileReady()
    {
        weapon.SetActive(false);
        anim.SetTrigger("Shot");
        yield return new WaitForSeconds(.2f);
        shotEffect.Play();
        ShootProjectile();
        //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(projectile.transform.position.x, projectile.transform.position.y, projectile.transform.position.z ));
        //transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    void ShootProjectile()
    {
        if (projectile.activeSelf) return;

        Vector3 targetPoint = GetMouseWorldPosition();
        float distance = Vector3.Distance(transform.position, targetPoint);

        if (distance > grappleRange)
        {
            Vector3 direction = (targetPoint - transform.position).normalized;
            targetPoint = transform.position + direction * grappleRange;
        }

        projectile.transform.position = transform.position;
        projectile.SetActive(true);
        grappleProjectile.SetTarget(targetPoint, projectileSpeed, grappleLayerMask);

        lineRenderer.enabled = true;
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position); // xz 평면
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    public void StartGrapple(Vector3 hitPoint)
    {
        grapplePoint = hitPoint;
        grapplePoint.y = transform.position.y; // y축 고정
        isGrappling = true;
        pullStartTime = Time.time;
        grappleProjectile.StopMovement(); // 총알의 이동을 멈춤
    }

    public void ResetGrapple()
    {
        isGrappling = false;
        lineRenderer.enabled = false;
        projectile.SetActive(false);
        weapon.SetActive(true);
    }

    public void ProjectileReturning()
    {
        grappleProjectile.ReturnToPlayer();
    }
}
