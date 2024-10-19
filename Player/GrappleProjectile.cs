using System.Collections;
using UnityEngine;

public class GrappleProjectile : MonoBehaviour
{
    private Vector3 targetPoint;
    private float speed;
    private LayerMask layerMask;
    private GrappleHook grappleHook;
    private bool returning = false;
    private bool hitObject = false;
    private bool isMoving = true;
    public Transform playerTransform;

    public void Initialize(GrappleHook hook)
    {
        grappleHook = hook;
    }

    public void SetTarget(Vector3 target, float projectileSpeed, LayerMask mask)
    {
        targetPoint = target;
        targetPoint.y = playerTransform.position.y; // y�� ����
        speed = projectileSpeed;
        layerMask = mask;
        returning = false;
        hitObject = false;
        isMoving = true;
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    public void ReturnToPlayer()
    {
        targetPoint = playerTransform.position; // �÷��̾� ��ġ�� ���ư�
        returning = true;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPoint, step);
            newPosition.y = playerTransform.position.y; // y�� ����
            transform.position = newPosition;

            if (!returning)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (targetPoint - transform.position).normalized, out hit, step, layerMask))
                {
                    grappleHook.StartGrapple(hit.point);
                    hitObject = true; // �浹 �� �̵� ����
                    isMoving = false;
                }
                else if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
                {
                    ReturnToPlayer(); // ��ǥ ������ ���������� �浹���� �ʾ��� ��� �÷��̾�� ���ư�
                }
            }
            else if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                grappleHook.ResetGrapple();
                gameObject.SetActive(false);
                isMoving = false;
            }
        }
    }
}
