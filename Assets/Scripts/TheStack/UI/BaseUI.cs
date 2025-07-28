using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

namespace TheStack
{
    public abstract class BaseUI : MonoBehaviour
    {
        protected UIManager UIManager;

        public virtual void Init(UIManager uiManager)
        {
            UIManager = uiManager;
        }

        protected abstract UIState GetUIState();

        public void SetActive(UIState state)
        {
            gameObject.SetActive(GetUIState() == state);
        }
    }

}