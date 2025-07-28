using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : InteractionHandler
{
    [SerializeField]
    private string GameName;

    [SerializeField]
    private string SceneName;

    [SerializeField]
    private Animator anim;

    public override void Interaction()
    {
        base.Interaction();
        SceneManager.LoadScene(SceneName);
    }

    public override void EnterTriggerRange()
    {
        base.EnterTriggerRange();
        textUI.SetData($"Enter Game {GameName}");
        anim.SetBool("IsOpen", true);
    }

    public override void ExitTriggerRange()
    {
        base.ExitTriggerRange();
        anim.SetBool("IsOpen", false);
    }
}
