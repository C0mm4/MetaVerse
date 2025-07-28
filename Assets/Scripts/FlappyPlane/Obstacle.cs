using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlappyPlane
{
    public class Obstacle : MonoBehaviour
    {
        public float highPosY = 1f;
        public float lowPosY = -1f;

        public float holeSizeMin = 1f;
        public float holeSizeMax = 3f;

        public Transform topObject;
        public Transform bottomObject;

        public float widthPadding = 4f;

        public GameManager gameManager;

        public void Start()
        {
            gameManager = GameManager.Instance;
        }

        public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
        {
            float holeSize = Random.Range(holeSizeMin, holeSizeMax);

            float halfHoleSize = holeSize / 2;

            topObject.localPosition = new Vector3(0, halfHoleSize, 0);
            bottomObject.localPosition = new Vector3(0, -halfHoleSize, 0);

            Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

            placePosition.y = Random.Range(lowPosY, highPosY);

            transform.position = placePosition;
            return placePosition;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player)
            {
                gameManager.AddScore(1);
            }
        }
    }

}