using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace TopDownShooter
{
    public class PlayerController : BaseController
    {
        private Camera mainCamera;
        GameManager gameManager;

        public void Init(GameManager instance)
        {
            gameManager = instance;
            mainCamera = Camera.main;
        }


        protected override void HandleAction()
        {


        }

        public override void Death()
        {
            base.Death();
            gameManager.GameOver();
        }

        private void OnMove(InputValue inputValue)
        {
            movementDirection = inputValue.Get<Vector2>();
            movementDirection = movementDirection.normalized;
        }

        void OnLook(InputValue inputValue)
        {
            Vector2 mousePos = inputValue.Get<Vector2>();
            Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            lookDirection = (worldPos - (Vector2)transform.position);

            if (lookDirection.magnitude < .5f)
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;
            }
        }

        void OnFire(InputValue inputValue)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }


            isAttacking = inputValue.isPressed;
        }
    }

}