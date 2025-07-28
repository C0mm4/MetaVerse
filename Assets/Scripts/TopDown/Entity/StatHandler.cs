using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class StatHandler : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField]
        private int health = 10;

        public int Health
        {
            get { return health; }
            set { health = Mathf.Clamp(value, 0, 100); }
        }

        [Range(1, 20)]
        [SerializeField]
        private float speed = 3;
        public float Speed
        {
            get { return speed; }
            set { speed = Mathf.Clamp(value, 0, 20); }
        }


    }

}