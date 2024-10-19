using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ParryingSystem : MonoBehaviour
{
    [Header("클릭 관련 변수")]
    private bool isPointerDown = false;
    public bool isLongClick = false;
    private float pointerDownTimer = 0;
    public float requiredHoldTime = .2f; // 롱클릭으로 간주되는 시간 (1초)
    private Coroutine longClickCoroutine;

    [SerializeField] GameObject blackFilter;
    [SerializeField] MaterialSwitcher theMS;
    [SerializeField] Animator anim;

    [Header("패링 관련")]
    [SerializeField] Canvas parryingDirectionGuide;
    [SerializeField] ParryableZone thePZ;
    Vector3 parryingDirection;
    Quaternion parryingRotation;

    private void Start()
    {
        Cursor.visible = true; // 마우스 커서를 보이게 설정
        Cursor.lockState = CursorLockMode.None; // 커서를 화면 안에서 자유롭게 이동 가능하도록 설정
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPointerDown = true;
            pointerDownTimer = 0;
            isLongClick = false;
            if (thePZ.objectInTrigger)
            {
                thePZ.ParryObject();
                anim.SetTrigger("ShortParry");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isLongClick)
            {
                
            }
            isPointerDown = false;
            pointerDownTimer = 0;

            if (isLongClick)
            {
                Time.timeScale = 1f;
                if (thePZ.objectInTrigger)
                {
                    anim.SetTrigger("LongParry");
                    thePZ.LongParryObject(parryingDirection - transform.position);
                }
                else
                {
                    anim.SetTrigger("CancelParry");
                }
                isLongClick = false;
                theMS.RestoreMaterials();
                blackFilter.transform.DOScale(new Vector3(0f, 0f, 0f), .2f);
                parryingDirectionGuide.gameObject.SetActive(false);
                Invoke("FadeOutFilter", 0.2f);
            }
        }

        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= requiredHoldTime && !isLongClick)
            {
                anim.SetTrigger("LongParryReady");
                isLongClick = true;
                LongClick();               
            }
            UpdateArrowDirection();
        }
    }

    private void LongClick()
    {
        blackFilter.gameObject.SetActive(true);
        blackFilter.transform.DOScale(new Vector3(1.5f, 1.5f, 0.2f), .16f);
        theMS.ChangeMaterials();
        Time.timeScale = .2f;
        parryingDirectionGuide.gameObject.SetActive(true);
   
    }
    void UpdateArrowDirection()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            parryingDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        parryingRotation = Quaternion.LookRotation(parryingDirection - transform.position);
        parryingDirectionGuide.transform.rotation = Quaternion.Lerp(parryingRotation, parryingDirectionGuide.transform.rotation, 0f);
    }
    void FadeOutFilter()
    {
        blackFilter.SetActive(false);
    }


}
