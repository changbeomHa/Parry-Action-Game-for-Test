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
        targetPoint.y = playerTransform.position.y; // y축 고정
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
        targetPoint = playerTransform.position; // 플레이어 위치로 돌아감
        returning = true;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPoint, step);
            newPosition.y = playerTransform.position.y; // y축 고정
            transform.position = newPosition;

            if (!returning)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (targetPoint - transform.position).normalized, out hit, step, layerMask))
                {
                    grappleHook.StartGrapple(hit.point);
                    hitObject = true; // 충돌 시 이동 멈춤
                    isMoving = false;
                }
                else if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
                {
                    ReturnToPlayer(); // 목표 지점에 도달했지만 충돌하지 않았을 경우 플레이어로 돌아감
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
