using System;
using UnityEngine;

public class GamePlayGameState : GameStateBase
{
    #region implemented abstract members of _StatesBase

    private float elapsedTime;
    public float gamePlayingTime;


    public override void Activate()
    {
        LevelManager.Instance.InitLevel(); // initial setup of current level
        GameManager.Instance.IsGameActive = true; // game has been started
        UIManager.Instance.ActivateUI(Menus.INGAME); // IngameUI opened
        AudioManager.Instance.PlayBackGroundClip();
        elapsedTime = 0; // elapsed time has been reset
        Debug.Log("<color=green>Gameplay State</color> OnActive");
    }

    public override void Deactivate()
    {
        Debug.Log("<color=red>Gameplay State</color> OnDeactivate");
    }

    public override void UpdateState()
    {
        if (Timer()) // if timer is true turn, game over
            LevelManager.Instance.GameOver();

        Debug.Log("<color=yellow>Gameplay State</color> OnUpdate");
    }

    /// <summary>
    /// Calculate elapsedtime and Update IngameUI 
    /// </summary>
    /// <returns></returns>
    public bool Timer()
    {
        elapsedTime += Time.deltaTime;
        var roundFigure = (int) Math.Ceiling(gamePlayingTime - elapsedTime);
        //TODO: not about timer, change place
        var remainderSumo = LevelManager.Instance.RemainderSumoValue();
        UIManager.Instance.UpdateInGameUI(roundFigure, remainderSumo);
        if (elapsedTime >= gamePlayingTime)
        {
            elapsedTime = 0;
            return true;
        }

        return false;
    }

    #endregion
}