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

        if(scripts.Count > 0)
        {
            textUI.SetData(scripts[index]);
            Debug.Log(scripts[index++]);

            index = Mathf.Min(index, scripts.Count - 1);
        }
    }

    public override void EnterTriggerRange()
    {
        if(scripts.Count > 0)
        {
            base.EnterTriggerRange();
            textUI?.SetData("Talking");
        }
    }
}
