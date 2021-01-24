using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : PowerUpItem
{
    private float _growthConstant = 1.2f;
    public float GrowthConstant => _growthConstant;
}
