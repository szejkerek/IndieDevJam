using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PushBeamPower", menuName = "Power/PushBeamPower")]
public class PushBeamPower : Power
{
    public override void UsePower()
    {
        Debug.Log("Push Beam");
    }
}
