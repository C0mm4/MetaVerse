using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Animations;


namespace TopDownShooter
{
    public class MeleeWeaponHandler : WeaponHandler
    {
        [Header("Melee Attack Info")]
        public Vector2 collideBoxSize = Vector2.one;

        [SerializeField]
        TrailRenderer TrailRenderer;
        protected override void Start()
        {
            base.Start();
            collideBoxSize = collideBoxSize * WeaponSize;
            TrailRenderer.gameObject.SetActive(false);
        }

        public override void Attack()
        {
            base.Attack();
            TrailRenderer.gameObject.SetActive(true);
            Invoke("AttackTrailEnd", 1 / Speed);
            RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
                collideBoxSize, 0, Vector2.zero, target);

            if (hit.collider != null)
            {
                ResourceController resource = hit.collider.GetComponent<ResourceController>();
                if (resource != null)
                {
                    resource.ChangeHealth(-Power);
                    if (IsOnKnockBack)
                    {
                        BaseController baseController = hit.collider.GetComponent<BaseController>();
                        if (baseController != null)
                        {
                            baseController.ApplyKnockback(transform, KnockBackPower, KnockBackTime);
                        }
                    }
                }
            }
        }

        public override void Rotate(bool isLeft)
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        public void AttackTrailEnd()
        {
            TrailRenderer.gameObject.SetActive(false);
        }
    }


}