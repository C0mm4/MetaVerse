using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get { return instance; }
        }

        public PlayerController player { get; private set; }
        private ResourceController playerResourceController;

        [SerializeField]
        private int currentWaveIndex = 0;

        private EnemyManager enemyManager;

        private UIManager uiManager;
        public static bool isFirstLoading = true;

        private void Awake()
        {
            instance = this;

            player = FindObjectOfType<PlayerController>();
            player.Init(this);

            uiManager = FindObjectOfType<UIManager>();

            enemyManager = GetComponentInChildren<EnemyManager>();
            enemyManager.Init(this);

            playerResourceController = player.GetComponent<ResourceController>();

            playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
            playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        }

        private void Start()
        {
            if (!isFirstLoading)
            {
                StartGame();
            }
            else
            {
                isFirstLoading = false;
            }
        }

        public void StartGame()
        {
            uiManager.SetPlayGame();
            StartNextWave();
        }

        private void StartNextWave()
        {
            currentWaveIndex++;
            enemyManager.StartWave(1 + currentWaveIndex / 5);
            uiManager.ChangeWave(currentWaveIndex);
        }

        public void EndOfWave()
        {
            StartNextWave();
        }

        public void GameOver()
        {
            enemyManager.StopWave();
            uiManager.SetGameOver();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

}