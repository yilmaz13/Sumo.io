using UnityEngine;
using System.Collections;

public class GameOverGameState : GameStateBase
{
    #region implemented abstract members of StatesBase

    public override void Activate()
    {
        GameManager.Instance.IsGameActive = false;
        UIManager.Instance.ActivateGameOverUI();
        // Managers.Audio.PlayLoseSound();
        Debug.Log("<color=green>Game Over State</color> OnActive");
    }

    public override void Deactivate()
    {
        Debug.Log("<color=red>Game Over State</color> OnDeactivate");
    }

    public override void UpdateState()
    {
        Debug.Log("<color=yellow>Game Over State</color> OnUpdate");
    }

    #endregion
}