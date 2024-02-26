using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PushBeamPower", menuName = "Power/PushBeamPower")]
public class PushBeamPower : Power
{
    [SerializeField]
    float power = 100;

    public override void UsePower()
    {
       
        if (Player.Instance.PlayerVision.PlayerRaycast(out Collider hit, Mathf.Infinity))
        {
            if (hit.gameObject.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = Player.Instance.GetLookRotation().normalized;
                hit.gameObject.GetComponent<Rigidbody>().AddForce(dir * power);
                PlayUseSound();
            }
        }
    }
}
