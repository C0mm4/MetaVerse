using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter
{
    public class ResourceController : MonoBehaviour
    {
        [SerializeField]
        private float healthChangeDelay = .5f;

        private BaseController baseController;
        private StatHandler statHandler;
        private AnimationHandler animationHandler;

        private float timeSinceLastChnage = float.MaxValue;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => statHandler.Health;

        public AudioClip DamageSFX;

        private Action<float, float> OnChangeHealth;

        private void Awake()
        {
            baseController = GetComponent<BaseController>();
            statHandler = GetComponent<StatHandler>();
            animationHandler = GetComponent<AnimationHandler>();
        }

        private void Start()
        {
            CurrentHealth = statHandler.Health;
        }

        private void Update()
        {
            if (timeSinceLastChnage < healthChangeDelay)
            {
                timeSinceLastChnage += Time.deltaTime;
                if (timeSinceLastChnage > healthChangeDelay)
                {
                    animationHandler.InvincibilityEnd();
                }
            }
        }

        public bool ChangeHealth(float change)
        {
            if (change == 0 || timeSinceLastChnage < healthChangeDelay)
            {
                return false;
            }

            timeSinceLastChnage = 0f;

            CurrentHealth += change;
            CurrentHealth = Mathf.Max(0, Mathf.Min(MaxHealth, CurrentHealth));


            OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

            if (change < 0)
            {
                animationHandler.Damage();

                if (DamageSFX != null)
                {
                    SoundManager.PlayClip(DamageSFX);
                }
            }

            if (CurrentHealth <= 0f)
            {
                Death();
            }

            return true;
        }


        private void Death()
        {
            baseController.Death();
        }

        public void AddHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth += action;
        }

        public void RemoveHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth -= action;
        }
    }

}