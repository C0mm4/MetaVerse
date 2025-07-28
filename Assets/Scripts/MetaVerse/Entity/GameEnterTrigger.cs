using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnterTrigger : InteractionHandler
{
    [SerializeField]
    private string LoadSceneName;

    public override void Interaction()
    {
        base.Interaction();
        SceneManager.LoadScene(LoadSceneName);
    }
}
