using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace TopDownShooter
{
    public class ProjectileManager : MonoBehaviour
    {
        private static ProjectileManager instance;
        public static ProjectileManager Instance
        {
            get { return instance; }
        }

        [SerializeField]
        private GameObject[] projectilePrefabs;

        [SerializeField]
        private ParticleSystem impactParticleSystem;

        private void Awake()
        {
            instance = this;

        }

        public void ShootBullet(RangeWeaponHandler weaponHandler, Vector2 startPos, Vector2 dir)
        {
            GameObject origin = projectilePrefabs[weaponHandler.BulletIndex];
            GameObject obj = Instantiate(origin, startPos, Quaternion.identity);

            ProjectileController projectileController = obj.GetComponent<ProjectileController>();

            projectileController.Init(dir, weaponHandler, this);


        }

        public void CreateImpactParticleAtPosition(Vector3 pos, RangeWeaponHandler weapon)
        {
            impactParticleSystem.transform.position = pos;
            ParticleSystem.EmissionModule em = impactParticleSystem.emission;
            em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weapon.BulletSize * 5)));

            ParticleSystem.MainModule mainModule = impactParticleSystem.main;
            mainModule.startSpeedMultiplier = weapon.BulletSize * 10f;
            impactParticleSystem.Play();

        }
    }

}