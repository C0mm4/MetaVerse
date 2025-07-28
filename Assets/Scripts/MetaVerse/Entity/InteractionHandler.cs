using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField]
    private InteractionHandler closeInteractor;

    [SerializeField]
    protected TextUI textUI;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactor"))
        {
            var targetHandler = collision.GetComponent<InteractionHandler>();

            // 이전 InteractionHandler 정보 제거
            if(closeInteractor != null)
            {
                OnInteraction -= closeInteractor.Interaction;
                closeInteractor.ExitTriggerRange();
            }

            // InteractionHandler 정보 추가
            closeInteractor = targetHandler;
            OnInteraction += targetHandler.Interaction;
            targetHandler.EnterTriggerRange();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactor"))
        {
            var targetHandler = collision.GetComponent<InteractionHandler>();
            if(targetHandler == closeInteractor)
            {
                // InteractionHandler가 현재 저장된 객체일 시 해당 정보 삭제
                OnInteraction -= targetHandler.Interaction;
                targetHandler.ExitTriggerRange();
                closeInteractor = null;
            }
        }
    }

    public Action OnInteraction;

    public virtual void Interaction()
    {
    }

    public virtual void EnterTriggerRange()
    {
        // 상호작용 UI 오브젝트 설정
        textUI?.gameObject.SetActive(true);
    }

    public virtual void ExitTriggerRange()
    {
        // 상호작용 UI 오브젝트 설정
        textUI?.gameObject.SetActive(false);
    }
}
