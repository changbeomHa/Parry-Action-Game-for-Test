using System.Collections;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float grappleRange = 10f;  // �׷� ���� �ִ� ��Ÿ�
    public float grappleSpeed = 10f;  // �÷��̾ �׷� ����Ʈ�� �������� �⺻ �ӵ�
    public float projectileSpeed = 20f; // �׷� ������Ÿ���� �ӵ�
    public LayerMask grappleLayerMask; // �׷��� �� �ִ� ������Ʈ�� ���͸��� ���̾� ����ũ
    public GameObject projectile; // �׷� ������Ÿ�� ������Ʈ
    public LineRenderer lineRenderer; // ������ �ð�ȭ�ϱ� ���� ���� ������

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
            float currentSpeed = grappleSpeed * (1 + timeSinceStarted); // �ӵ� ���� ȿ��
            float step = currentSpeed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, grapplePoint, step);
            newPosition.y = transform.position.y; // y�� ����
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
        Plane plane = new Plane(Vector3.up, transform.position); // xz ���
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    public void StartGrapple(Vector3 hitPoint)
    {
        grapplePoint = hitPoint;
        grapplePoint.y = transform.position.y; // y�� ����
        isGrappling = true;
        pullStartTime = Time.time;
        grappleProjectile.StopMovement(); // �Ѿ��� �̵��� ����
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
