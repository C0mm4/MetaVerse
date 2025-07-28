using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlappyPlane
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;
        float offsetX;

        // Start is called before the first frame update
        void Start()
        {
            if (target == null)
                return;
            // �ʱ� ī�޶�� Ÿ���� �Ÿ� ����
            offsetX = transform.position.x - target.position.x;

        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            // ����� �Ÿ���ŭ ī�޶� ��ġ
            Vector3 pos = transform.position;
            pos.x = target.position.x + offsetX;
            transform.position = pos;
        }
    }

}
