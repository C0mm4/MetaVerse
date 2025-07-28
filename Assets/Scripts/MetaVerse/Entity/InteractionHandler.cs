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

            // Remove old Interaction target
            if(closeInteractor != null)
            {
                OnInteraction -= closeInteractor.Interaction;
                closeInteractor.ExitTriggerRange();
            }

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
        textUI?.gameObject.SetActive(true);
    }

    public virtual void ExitTriggerRange()
    {
        textUI?.gameObject.SetActive(false);
    }
}
