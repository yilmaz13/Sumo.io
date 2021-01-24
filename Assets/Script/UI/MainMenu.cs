using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text infoText;
    public Button playButton;

    void OnEnable()
    {
        infoText.enabled = true;
    }

    void OnDisable()
    {
        infoText.enabled = false;
    }

    public void MainMenuStartAnimation()
    {
        infoText.text = "Level " + PlayerPrefs.GetInt("Level");
    }
}