using UnityEngine;
using System.Collections;

public class MenuGameState : GameStateBase
{
    #region implemented abstract members of GameState

    public override void Activate()
    {
        StartCoroutine(ActivateDelay());
        Debug.Log("<color=green>Menu State</color> OnActive");
    }

    public override void Deactivate()
    {
        Debug.Log("<color=red>Menu State</color> OnDeactivate");
    }

    public override void UpdateState()
    {
        Debug.Log("<color=yellow>Menu State</color> OnUpdate");
    }

    IEnumerator ActivateDelay()
    {
        yield return new WaitForSeconds(0.001f);
        UIManager.Instance.ActivateUI(Menus.MAIN);
    }

    #endregion
}