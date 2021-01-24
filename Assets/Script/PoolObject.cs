using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public Transform parent;
    public float LifeTimeMax { get; set; } = 2;
    private float elapsedTime;
    private void OnEnable()
    {
        elapsedTime = 0;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= LifeTimeMax)
        {
            elapsedTime = 0;
            transform.parent = parent;
            gameObject.SetActive(false);
        }
    }
}
