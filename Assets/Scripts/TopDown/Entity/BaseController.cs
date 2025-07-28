using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class BaseController : MonoBehaviour
    {
        protected Rigidbody2D body;

        [SerializeField]
        private SpriteRenderer charactorRenderer;
        [SerializeField]
        private Transform weaponPivot;

        protected Vector2 movementDirection = Vector2.zero;
        public Vector2 MovementDirection { get { return movementDirection; } }

        protected Vector2 lookDirection = Vector2.zero;
        public Vector2 LookDirection { get { return lookDirection; } }

        private Vector2 knockback = Vector2.zero;
        private float knockbackDuration = 0f;

        protected AnimationHandler animationHandler;
        protected StatHandler statHandler;

        // Start is called before the first frame update

        [SerializeField]
        public WeaponHandler WeaponPrefab;
        protected WeaponHandler weaponHandler;

        protected bool isAttacking;
        private float timeSinceLastAttack = float.MaxValue;



        protected virtual void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animationHandler = GetComponent<AnimationHandler>();
            statHandler = GetComponent<StatHandler>();

            if (WeaponPrefab != null)
            {
                weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
            }
            else
            {
                weaponHandler = GetComponentInChildren<WeaponHandler>();
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            HandleAction();
            Rotate(lookDirection);
            HandleAttackDelay();
        }

        protected virtual void FixedUpdate()
        {
            Movement(movementDirection);
            if (knockbackDuration > 0f)
            {
                knockbackDuration -= Time.fixedDeltaTime;
            }
        }

        protected virtual void HandleAction()
        {

        }

        private void Movement(Vector2 direction)
        {
            direction = direction * statHandler.Speed;
            if (knockbackDuration > 0)
            {
                direction *= 0.2f;
                direction += knockback;
            }

            body.velocity = direction;
            animationHandler.Move(direction);
        }

        private void Rotate(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bool isleft = Mathf.Abs(rotZ) > 90f;

            charactorRenderer.flipX = isleft;

            if (weaponPivot != null)
            {
                weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
            }

            weaponHandler?.Rotate(isleft);
        }

        public void ApplyKnockback(Transform other, float power, float duration)
        {
            knockbackDuration = duration;
            knockback = (other.position - transform.position).normalized * power;
        }

        private void HandleAttackDelay()
        {
            if (weaponHandler == null)
            {
                return;
            }

            if (timeSinceLastAttack <= weaponHandler.Delay)
            {
                timeSinceLastAttack += Time.deltaTime;
            }

            if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
            {
                timeSinceLastAttack = 0;
                Attack();
            }
        }

        protected virtual void Attack()
        {
            if (lookDirection != Vector2.zero)
            {
                weaponHandler?.Attack();
            }
        }

        public virtual void Death()
        {
            body.velocity = Vector3.zero;

            foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                Color color = renderer.color;
                color.a = 0.3f;
                renderer.color = color;
            }

            foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
            {
                component.enabled = false;
            }

            Destroy(gameObject, 2f);
        }
    }

}