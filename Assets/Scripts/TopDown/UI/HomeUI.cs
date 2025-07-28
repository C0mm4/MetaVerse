using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace TopDownShooter
{
    public class HomeUI : BaseUI
    {
        [SerializeField]
        private Button startB;

        [SerializeField]
        private Button endB;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            startB.onClick.AddListener(OnClickStartButton);
            endB.onClick.AddListener(OnClickEndButton);
        }

        public void OnClickStartButton()
        {
            GameManager.Instance.StartGame();
        }

        public void OnClickEndButton()
        {
            SceneManager.LoadScene(0);
        }

        protected override UIState GetUIState()
        {
            return UIState.Home;
        }
    }

}