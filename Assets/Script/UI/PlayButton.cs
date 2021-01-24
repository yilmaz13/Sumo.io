using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        // Audio.PlayUIClick();
        StartCoroutine(PlayGame());
    }

    public void OnClickNextLevelButton()
    {
        LevelManager.Instance.NextLevel();
    }

    public void OnClickRestartButton()
    {
        // Audio.PlayUIClick();
        GameManager.Instance.SetScene(PlayerPrefs.GetInt("Level"));
    }

    IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.SetState(typeof(GamePlayGameState));
        GameManager.Instance.IsGameActive = true;
    }
}