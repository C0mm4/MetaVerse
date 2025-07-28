using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using TopDownShooter;
using Unity.VisualScripting;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    protected Rigidbody2D body;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDirection { get { return moveDirection; } }

    protected Vector2 lookDirection;
    protected Vector2 LookDirection { get { return LookDirection; } }

    protected AnimationHandler animationHandler;

    protected bool isJumpPressed;
    protected bool canJump;
    [SerializeField]
    private float jumpTime;

    protected bool isFPressed;

    protected InteractionHandler interactionHandler;
    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animationHandler = GetComponent<AnimationHandler>();
        interactionHandler = GetComponentInChildren<InteractionHandler>();
    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleJump(); 
        HandleInteract();
    }

    protected virtual void FixedUpdate()
    {
        Movement(moveDirection);
    }

    private void Movement(Vector2 dir)
    {
        dir = dir * 5f;
        body.velocity = dir;

        animationHandler.Move(dir);
    }

    protected virtual void HandleAction()
    {

    }

    protected virtual void HandleJump()
    {
        if (isJumpPressed && canJump)
        {
            animationHandler.Jump();
            canJump = false;
            Invoke("ResetJump", jumpTime);
        }
    }

    protected virtual void HandleInteract()
    {
        if (isFPressed)
        {
            if(interactionHandler != null)
            {
                interactionHandler.OnInteraction?.Invoke();
            }
            isFPressed = false;
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isleft = Mathf.Abs(rotZ) > 90f;

        spriteRenderer.flipX = isleft;

    }

    void ResetJump()
    {
        canJump = true;
    }
}
