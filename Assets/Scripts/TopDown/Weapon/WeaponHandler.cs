using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter
{
    public class WeaponHandler : MonoBehaviour
    {
        [Header("Attack Info")]
        [SerializeField]
        private float delay = 1f;
        public float Delay { get { return delay; } set { delay = value; } }

        [SerializeField]
        private float weaponSize = 1;
        public float WeaponSize { get { return weaponSize; } set { weaponSize = value; } }

        [SerializeField]
        private float power = 1f;
        public float Power { get { return power; } set { power = value; } }

        [SerializeField]
        private float speed = 1f;
        public float Speed { get { return speed; } set { speed = value; } }

        [SerializeField]
        private float attackRange = 10f;
        public float AttackRange { get { return attackRange; } set { attackRange = value; } }

        public LayerMask target;

        [Header("Knock Back Info")]
        [SerializeField] private bool isOnKnockback = false;
        public bool IsOnKnockBack { get { return isOnKnockback; } set { isOnKnockback = value; } }

        [SerializeField]
        private float knockBackPower = 0.1f;
        public float KnockBackPower { get { return knockBackPower; } set { knockBackPower = value; } }


        [SerializeField]
        private float knockBackTime = 0.5f;
        public float KnockBackTime { get { return knockBackTime; } set { KnockBackTime = value; } }

        private static readonly int IsAttack = Animator.StringToHash("IsAttack");

        public BaseController Controller { get; private set; }

        private Animator animator;
        private SpriteRenderer weaponRenderer;

        public AudioClip SFXClip;
        protected virtual void Awake()
        {
            Controller = GetComponentInParent<BaseController>();
            animator = GetComponentInChildren<Animator>();
            weaponRenderer = GetComponentInChildren<SpriteRenderer>();

            animator.speed = 1.0f / delay;
            transform.localScale = Vector3.one * weaponSize;
        }

        protected virtual void Start()
        {

        }

        public virtual void Attack()
        {
            AttackAnimatino();
            if (SFXClip != null)
            {
                SoundManager.PlayClip(SFXClip);
            }
        }

        public void AttackAnimatino()
        {
            animator.SetTrigger(IsAttack);
        }

        public virtual void Rotate(bool isLeft)
        {
            weaponRenderer.flipY = isLeft;
        }
    }

}