using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameActive { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            IsGameActive = false;
            PlayerPrefs.GetInt("Level", 1);
        }
    }

    private GameStateBase currentGameState;

    public GameStateBase GameState
    {
        get { return currentGameState; }
    }

    //Changes the current game state
    public void SetState(System.Type newStateType)
    {
        if (currentGameState != null)
        {
            currentGameState.Deactivate();
        }

        currentGameState = GetComponentInChildren(newStateType) as GameStateBase;
        if (currentGameState != null)
        {
            currentGameState.Activate();
        }
    }

    //Changes the current game scene
    public void SetScene(int SceneIndex)
    {
        SceneManager.LoadScene("Level" + SceneIndex);
    }

    void Update()
    {
        if (currentGameState != null)
        {
            currentGameState.UpdateState();
        }
    }

    void Start()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
        SetState(typeof(MenuGameState));
    }
}