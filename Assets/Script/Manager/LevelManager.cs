using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class LevelManager : MonoBehaviour
{
    // levelmanager is singleton so we access this in global 
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            GameManager.Instance.SetState(typeof(MenuGameState));
        }
    }

    public GameObject player;
    public Camera mainCamera;
    public Transform sumosTransform;
    public List<EnemyController> enemies;
    public Transform[] powerUpItemTransforms;
    public GameObject foodPrefab;

    /// <summary>
    /// Current level is initial setup
    /// </summary>
    public void InitLevel()
    {
        GameManager.Instance.IsGameActive = false;
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
        mainCamera.gameObject.GetComponent<CameraMovement>().SetTarget(player.transform);
        HandOutFood();

        foreach (Transform enemy in sumosTransform)
        {
            if (enemy.gameObject.GetComponent<EnemyController>() != null)
            {
                var enemyController = enemy.GetComponent<EnemyController>();
                enemies.Add(enemyController);
            }
        }
    }

    /// <summary>
    /// Finish Gameplay, active state is GameOverGameState
    /// </summary>
    public void GameOver()
    {
        GameManager.Instance.SetState(typeof(GameOverGameState));
        GameManager.Instance.IsGameActive = false;
    }

    /// <summary>
    /// Hand out food in arena for sumos
    /// </summary>
    public void HandOutFood()
    {
        var random = Random.Range(0, powerUpItemTransforms.Length);
        foreach (var powerUpItemTransform in powerUpItemTransforms)
        {
            Instantiate(foodPrefab, powerUpItemTransform);
        }
    }

    /// <summary>
    /// Return remainder sumo value in arena
    /// </summary>
    /// <returns></returns>
    public int RemainderSumoValue()
    {
        // add +1 playerSumo
        int remainderSumo = 1;
        foreach (EnemyController enemy in enemies)
        {
            if (!enemy.IsDead)
            {
                remainderSumo++;
            }
        }

        if (remainderSumo == 1)
        {
            GameManager.Instance.IsGameActive = false;
            UIManager.Instance.ActivateGameWonUI();
        }

        return remainderSumo;
    }

    /// <summary>
    /// Return nearest enemy transform 
    /// </summary>
    /// <param name="questioningSumo"></param>
    /// <returns></returns>
    public Transform NearestEnemy(Transform questioningSumo)
    {
        float distance = 2000;
        Transform nearestEnemy = null;
        foreach (Transform sumo in sumosTransform)
        {
            var currnetDistance = Vector3.Distance(sumo.position, questioningSumo.transform.position);
            var isDeadSumo = sumo.gameObject.GetComponent<SumoController>().IsDead;
            if (distance > currnetDistance && sumo != questioningSumo && !isDeadSumo)
            {
                distance = currnetDistance;
                nearestEnemy = sumo;
            }
        }

        return nearestEnemy;
    }

    public float GetScore => player.GetComponent<PlayerController>().score;
    public void AddScore(int score)
    {
        player.GetComponent<PlayerController>().score += score;
    }

    /// <summary>
    /// Load next level 
    /// </summary>
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", 1);

        GameManager.Instance.SetScene(PlayerPrefs.GetInt("Level"));
        GameManager.Instance.IsGameActive = false;
    }
}