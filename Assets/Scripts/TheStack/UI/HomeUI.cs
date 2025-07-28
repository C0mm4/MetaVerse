using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheStack
{
    public class HomeUI : BaseUI
    {
        Button startButton;
        Button exitButton;
        protected override UIState GetUIState()
        {
            return UIState.Home;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            startButton = transform.Find("StartButton").GetComponent<Button>();
            exitButton = transform.Find("ExitButton").GetComponent<Button>();

            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        void OnClickStartButton()
        {
            UIManager.OnClickStart();
        }

        void OnClickExitButton()
        {
            UIManager.OnClickExit();
        }
    }


}