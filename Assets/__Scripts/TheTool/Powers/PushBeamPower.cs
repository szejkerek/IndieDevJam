using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PushBeamPower", menuName = "Power/PushBeamPower")]
public class PushBeamPower : Power
{
    public override void UsePower()
    {
       
        if (Player.Instance.PlayerVision.PlayerRaycast(out Collider hit, Mathf.Infinity))
        {
            Debug.Log("Did Hit");
        }
    }
}
