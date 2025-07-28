using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace TopDownShooter
{
    public class GameOverUI : BaseUI
    {
        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button exitButton;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            restartButton.onClick.AddListener(OnClickRestartButton);
            exitButton.onClick.AddListener(OnClickEndButton);
        }

        public void OnClickRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnClickEndButton()
        {
            SceneManager.LoadScene(0);
        }

        protected override UIState GetUIState()
        {
            return UIState.GameOver;
        }
    }

}