using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PowerUp item similar to food can be inherited 
public class PowerUpItem : MonoBehaviour
{
    private void Start()
    {
        RotateMode360();
    }

    private void RotateMode360()
    {
        transform.DOBlendableLocalRotateBy(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360).OnComplete(RotateMode360);
    }
}
