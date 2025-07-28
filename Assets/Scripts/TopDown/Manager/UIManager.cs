using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


namespace TopDownShooter
{
    public enum UIState
    {
        Home, Game, GameOver
    }
    public class UIManager : MonoBehaviour
    {
        HomeUI homeUI;
        GameUI gameUI;
        GameOverUI gameOverUI;

        private UIState currentStage;

        private void Awake()
        {
            homeUI = GetComponentInChildren<HomeUI>(true);
            gameUI = GetComponentInChildren<GameUI>(true);
            gameOverUI = GetComponentInChildren<GameOverUI>(true);

            homeUI.Init(this);
            gameUI.Init(this);
            gameOverUI.Init(this);

            ChangeState(UIState.Home);
        }

        public void SetPlayGame()
        {
            ChangeState(UIState.Game);
        }

        public void SetGameOver()
        {
            ChangeState(UIState.GameOver);
        }

        public void ChangeWave(int waveIndex)
        {
            gameUI.UpdateWaveText(waveIndex);
        }

        public void ChangePlayerHP(float currentHP, float maxHP)
        {
            gameUI.UpdateHPSlider(currentHP / maxHP);
        }

        void ChangeState(UIState state)
        {
            currentStage = state;
            homeUI.SetActive(currentStage);
            gameUI.SetActive(currentStage);
            gameOverUI.SetActive(currentStage);
        }
    }

}