using FlappyPlane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityController
{

    // 키 입력 이벤트들
    private void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>();
        moveDirection = moveDirection.normalized;


        if (moveDirection.magnitude < .5f)
        {
        }
        else
        {
            lookDirection = moveDirection;
            lookDirection = lookDirection.normalized;
        }
    }
    private void OnJump(InputValue value)
    {
        isJumpPressed = value.isPressed;
        if (!isJumpPressed)
        {
            canJump = true;
        }
    }

    private void OnInteraction(InputValue value)
    {
        isFPressed = value.isPressed;
    }
} 
