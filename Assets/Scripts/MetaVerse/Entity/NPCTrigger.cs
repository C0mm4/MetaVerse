using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : InteractionHandler
{
    [SerializeField]
    private List<string> scripts;

    private int index = 0;


    public override void Interaction()
    {
        base.Interaction();

        // 상호작용 시 다음 대사 출력 마지막 대사는 반복 출력
        if(scripts.Count > 0)
        {
            textUI.SetData(scripts[index]);
            Debug.Log(scripts[index++]);

            index = Mathf.Min(index, scripts.Count - 1);
        }
    }

    public override void EnterTriggerRange()
    {
        // 스크립트가 있으면 대화하기 출력
        if(scripts.Count > 0)
        {
            base.EnterTriggerRange();
            textUI?.SetData("Talking");
        }
    }
}
