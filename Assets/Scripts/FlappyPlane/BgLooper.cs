using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FlappyPlane
{
    public class BgLooper : MonoBehaviour
    {
        public int obstacleCount = 0;
        public Vector3 obstacleLastPosition = Vector3.zero;
        public int numBgCount = 5;


        void Start()
        {
            Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
            obstacleLastPosition = obstacles[0].transform.position;
            obstacleCount = obstacles.Length;

            // 장애물 위치 초기화
            for (int i = 0; i < obstacleCount; i++)
            {
                obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Obstacle obstacle = collision.GetComponent<Obstacle>();
            // 장애물이나 배경 객체 충돌 시 다음 위치로 이동
            if (obstacle)
            {
                obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
                obstacleCount++;
            }
            else
            {
                if (collision.CompareTag("BackGround"))
                {
                    float widthOfBgObject = ((BoxCollider2D)collision).size.x;
                    Vector3 pos = collision.transform.position;

                    pos.x += widthOfBgObject * numBgCount;
                    collision.transform.position = pos;
                    return;
                }
            }
        }
    }

}