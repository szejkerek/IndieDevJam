using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PushBeamPower", menuName = "Power/PushBeamPower")]
public class PushBeamPower : Power
{
    public override void UsePower()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.Instance.transform.position, Player.Instance.TheTool.transform.position, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Player.Instance.transform.position, Player.Instance.TheTool.transform.position * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        Debug.DrawRay(Player.Instance.transform.position, Player.Instance.transform.position * 1000, Color.yellow);
        Debug.Log("Push Beam");
    }
}
