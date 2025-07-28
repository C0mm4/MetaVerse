using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace FlappyPlane
{

    public class Player : MonoBehaviour
    {
        Animator animator;
        Rigidbody2D body;

        public float flapForce = 6f;
        public float forwardSpeed = 3f;
        public bool isDead = false;
        float deathCooldown = 0f;

        bool isFlap = false;

        public bool godMode = false;

        public void Start()
        {
            animator = GetComponentInChildren<Animator>();
            body = GetComponent<Rigidbody2D>();

            if (animator == null)
            {
                Debug.LogError("Animator is Null!");
            }
            if (body == null)
            {
                Debug.LogError("Rigidbody is Null!");
            }
        }

        public void Update()
        {
            if (isDead)
            {
                if (deathCooldown <= 0)
                {
                    // 죽었을 때 입력 받으면 Restart
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        GameManager.Instance.RestartGame();
                    }
                }
                else
                {
                    deathCooldown -= Time.deltaTime;
                }
            }
            else
            {
                // 입력시 Flap
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    isFlap = true;
                }
            }
        }

        public void FixedUpdate()
        {
            if (isDead) return;

            Vector3 velocity = body.velocity;
            velocity.x = forwardSpeed;

            // Flap 시 상승 힘 추가
            if (isFlap)
            {
                velocity.y += flapForce;
                isFlap = false;
            }

            body.velocity = velocity;

            float angle = Mathf.Clamp(body.velocity.y * 10, -90, 90);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (godMode) return;

            if (isDead) return;

            isDead = true;
            deathCooldown = 1f;

            animator.SetBool("IsDie", true);
            GameManager.Instance.GameOver();
        }
    }
}
