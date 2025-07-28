using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FlappyPlane
{
    public class GameManager : MonoBehaviour
    {
        static GameManager _instance;

        public static GameManager Instance
        {
            get { return _instance; }
        }

        private int currentScore = 0;
        UIManager uiManager;
        public UIManager UIManager { get { return uiManager; } }

        private void Awake()
        {
            _instance = this;
            uiManager = FindObjectOfType<UIManager>();
        }


        public void GameOver()
        {
            Debug.Log("Game Over");
            uiManager.SetRestart();
        }

        public void RestartGame()
        {
            // 0번 씬으로 이동
            SceneManager.LoadScene(0);
        }

        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
        }
    }

}