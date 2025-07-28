using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform textUIRect;

    [SerializeField]
    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUIRect = transform.Find("BackGround").GetComponent<RectTransform>();
        textUI = textUIRect.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetData(string data)
    {
        textUI.text = data;

        // UI 크기 설정
        textUIRect.sizeDelta = new Vector2(textUI.preferredWidth + 50, textUIRect.sizeDelta.y);
    }


}
