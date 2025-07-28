using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


namespace TheStack
{

    public enum UIState
    {
        Home, Game, Score
    }

    public class UIManager : MonoBehaviour
    {
        static UIManager _instance;
        public static UIManager Instance
        {
            get { return _instance; }
        }

        UIState currentState = UIState.Home;
        HomeUI homeUI = null;
        GameUI gameUI = null;
        ScoreUI scoreUI = null;

        TheStack theStack = null;

        private void Awake()
        {
            _instance = this;
            theStack = FindAnyObjectByType<TheStack>();
            homeUI = GetComponentInChildren<HomeUI>(true);
            homeUI?.Init(this);

            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);

            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);

            ChangeState(UIState.Home);
        }

        public void ChangeState(UIState state)
        {
            currentState = state;
            homeUI?.SetActive(currentState);
            gameUI?.SetActive(currentState);
            scoreUI?.SetActive(currentState);

        }

        public void OnClickStart()
        {
            theStack.Restart();
            ChangeState(UIState.Game);
        }

        public void OnClickExit()
        {
            SceneManager.LoadScene(0);
            /*
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        ChangeState(UIState.Score);
#endif*/
        }

        public void UpdateScore()
        {
            gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
        }

        public void SetScoreUI()
        {
            scoreUI.SetUI(theStack.Score, theStack.Combo, theStack.BestScore, theStack.BestCombo);
            ChangeState(UIState.Score);
        }
    }

}