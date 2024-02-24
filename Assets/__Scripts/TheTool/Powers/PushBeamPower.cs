using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PushBeamPower", menuName = "Power/PushBeamPower")]
public class PushBeamPower : Power
{
    public override void UsePower()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.Instance.PlayerCamera.transform.position, Player.Instance.GetLookRotation(), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Player.Instance.transform.position, Player.Instance.TheTool.transform.position * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }

        //Debug.DrawRay(Player.Instance.transform.position, Player.Instance.transform.forward * 100000, Color.yellow);
        Debug.Log(Player.Instance.transform.position);
    }
}
