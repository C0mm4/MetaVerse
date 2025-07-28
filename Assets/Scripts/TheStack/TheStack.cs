using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Android;

namespace TheStack
{
    public class TheStack : MonoBehaviour
    {

        private const float BoundSize = 3.5f;
        private const float MovingBoundSize = 3f;
        private const float StackMovingSpeed = 5.0f;
        private const float BlockMovingSpeed = 3.5f;
        private const float ErrorMargin = 0.1f;

        public GameObject originBlock = null;

        private Vector3 prevBlockPosition;
        private Vector3 desiredPosition;
        private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

        Transform lastBlock = null;
        float blockTransform = 0f;
        float secondaryPosition = 0f;

        int stackCount = -1;

        public int Score { get { return stackCount; } }

        int comboCount = 0;
        public int Combo { get { return comboCount; } }

        private int maxCombo = 0;
        public int MaxCombo { get { return maxCombo; } }

        public Color prevColor;
        public Color nextColor;

        private bool isMovingX = true;

        int bestScore = 0;
        public int BestScore { get { return bestScore; } }
        int bestCombo = 0;
        public int BestCombo { get { return bestCombo; } }

        private const string BestScoreKey = "BestScore";
        private const string BestComboKey = "BestCombo";

        private bool isGameOver = true;

        private void Awake()
        {
            Screen.SetResolution(1080, 1920, false);

        }

        // Start is called before the first frame update
        void Start()
        {
            if (originBlock == null)
            {
                Debug.LogError("OriginBlock is NULL");
                return;
            }

            if (PlayerPrefs.HasKey(BestScoreKey))
            {
                bestScore = PlayerPrefs.GetInt(BestScoreKey);
            }
            if (PlayerPrefs.HasKey(BestComboKey))
            {
                bestCombo = PlayerPrefs.GetInt(BestComboKey);
            }

            prevBlockPosition = Vector3.down;

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            SpawnBlock();
            SpawnBlock();
        }

        // Update is called once per frame
        void Update()
        {
            if (isGameOver) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (PlaceBlock())
                {
                    SpawnBlock();
                }
                else
                {
                    Debug.Log("Game Over");
                    isGameOver = true;
                    UpdateScore();
                    GameOverEffect();
                    UIManager.Instance.SetScoreUI();
                }
            }

            MovingBlock();
            transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        }

        bool SpawnBlock()
        {
            if (lastBlock != null)
            {
                prevBlockPosition = lastBlock.localPosition;

            }

            GameObject newBlock = null;
            Transform newTrans = null;

            newBlock = Instantiate(originBlock);

            if (newBlock == null)
            {
                Debug.LogError("NewBlock Instantiate Failed");
                return false;
            }

            ColorChange(newBlock);

            newTrans = newBlock.transform;
            newTrans.parent = this.transform;
            newTrans.localPosition = prevBlockPosition + Vector3.up;
            newTrans.localRotation = Quaternion.identity;
            newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

            stackCount++;

            desiredPosition = Vector3.down * stackCount;
            blockTransform = 0f;

            lastBlock = newTrans;

            isMovingX = !isMovingX;

            UIManager.Instance.UpdateScore();

            return true;
        }

        Color GetRandomColor()
        {
            float r = Random.Range(100f, 250f) / 255f;
            float g = Random.Range(100f, 250f) / 255f;
            float b = Random.Range(100f, 250f) / 255f;

            return new Color(r, g, b);
        }

        void ColorChange(GameObject go)
        {
            Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

            Renderer rn = go.GetComponent<Renderer>();

            if (rn == null)
            {
                Debug.LogError("Renderer is NULL");
                return;
            }
            rn.material.color = applyColor;
            Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

            if (applyColor.Equals(nextColor) == true)
            {
                prevColor = nextColor;
                nextColor = GetRandomColor();
            }
        }

        void MovingBlock()
        {
            blockTransform += Time.deltaTime * BlockMovingSpeed;

            float movePosition = Mathf.PingPong(blockTransform, BoundSize) - BoundSize / 2;

            if (isMovingX)
            {
                lastBlock.localPosition = new Vector3(movePosition * MovingBoundSize, stackCount, secondaryPosition);
            }
            else
            {
                lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, movePosition * MovingBoundSize);
            }
        }

        bool PlaceBlock()
        {
            Vector3 lastPosition = lastBlock.transform.localPosition;

            if (isMovingX)
            {
                float deltaX = prevBlockPosition.x - lastPosition.x;
                bool isNegativeNum = (deltaX < 0) ? true : false;

                deltaX = Mathf.Abs(deltaX);
                if (deltaX > ErrorMargin)
                {
                    stackBounds.x -= deltaX;
                    if (stackBounds.x <= 0)
                    {
                        return false;
                    }

                    float middle = (prevBlockPosition.x + lastPosition.x) / 2;
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                    Vector3 tempPosition = lastBlock.localPosition;
                    tempPosition.x = middle;
                    lastBlock.localPosition = lastPosition = tempPosition;

                    float rubbleHalfScale = deltaX / 2f;
                    CreateRubble(
                        new Vector3(isNegativeNum
                                ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                                : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                            , lastPosition.y
                            , lastPosition.z),
                        new Vector3(deltaX, 1, stackBounds.y)
                    );

                    comboCount = 0;
                }
                else
                {
                    lastBlock.localPosition = prevBlockPosition + Vector3.up;
                    ComboCheck();
                }
            }
            else
            {
                float deltaZ = prevBlockPosition.z - lastPosition.z;
                bool isNegativeNum = (deltaZ < 0) ? true : false;

                deltaZ = Mathf.Abs(deltaZ);
                if (deltaZ > ErrorMargin)
                {
                    stackBounds.y -= deltaZ;
                    if (stackBounds.y <= 0)
                    {
                        return false;
                    }

                    float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                    Vector3 tempPosition = lastBlock.localPosition;
                    tempPosition.z = middle;
                    lastBlock.localPosition = lastPosition = tempPosition;

                    float rubbleHalfScale = deltaZ / 2f;
                    CreateRubble(
                        new Vector3(
                            lastPosition.x
                            , lastPosition.y
                            , isNegativeNum
                                ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                                : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                        new Vector3(stackBounds.x, 1, deltaZ)
                    );
                    comboCount = 0;
                }
                else
                {
                    lastBlock.localPosition = prevBlockPosition + Vector3.up;
                    ComboCheck();
                }
            }

            secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;

            return true;
        }

        void CreateRubble(Vector3 pos, Vector3 scale)
        {
            GameObject go = Instantiate(lastBlock.gameObject);
            go.transform.parent = this.transform;
            go.transform.localPosition = pos;
            go.transform.localScale = scale;

            go.transform.localRotation = Quaternion.identity;

            go.AddComponent<Rigidbody>();
            go.name = "Rubble";

        }

        void ComboCheck()
        {
            comboCount++;

            if (comboCount > maxCombo)
            {
                maxCombo = comboCount;
            }

            if ((comboCount % 5) == 0)
            {
                Debug.Log("5 Combo Success!");
                stackBounds += new Vector3(0.5f, 0.5f);

                stackBounds.x = (stackBounds.x > BoundSize) ? BoundSize : stackBounds.x;
                stackBounds.y = (stackBounds.y > BoundSize) ? BoundSize : stackBounds.y;
            }
        }

        void UpdateScore()
        {
            if (bestScore < stackCount)
            {
                Debug.Log("최고 점수 갱신!");
                bestScore = stackCount;
                bestCombo = maxCombo;

                PlayerPrefs.SetInt(BestScoreKey, bestScore);
                PlayerPrefs.SetInt(BestComboKey, bestCombo);
            }
        }

        void GameOverEffect()
        {
            int childCount = this.transform.childCount;

            for (int i = 1; i < 20; i++)
            {
                if (childCount < i)
                    break;

                GameObject go =
                    this.transform.GetChild(childCount - i).gameObject;

                if (go.name.Equals("Rubble"))
                    continue;

                Rigidbody rigid = go.AddComponent<Rigidbody>();

                rigid.AddForce(
                    (Vector3.up * Random.Range(0, 10f)
                     + Vector3.right * (Random.Range(0, 10f) - 5f))
                    * 100f
                );
            }
        }

        public void Restart()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            isGameOver = false;

            lastBlock = null;
            desiredPosition = Vector3.zero;
            stackBounds = new Vector3(BoundSize, BoundSize);

            stackCount = -1;
            isMovingX = true;
            blockTransform = 0f;
            secondaryPosition = 0f;

            comboCount = 0;
            maxCombo = 0;

            prevBlockPosition = Vector3.down;

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            SpawnBlock();
            SpawnBlock();
        }
    }

}