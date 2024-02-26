using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReverseGravityPower", menuName = "Power/ReverseGravityPower")]
public class ReverseGravityPower : Power
{
    public override void UsePower()
    {
        Physics.gravity *= -1;
        PlayUseSound();
        Debug.Log("Gravity Power");
    }
}
