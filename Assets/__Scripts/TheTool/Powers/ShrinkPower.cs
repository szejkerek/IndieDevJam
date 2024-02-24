using System.Collections;
using System.Collections.Generic;
using GordonEssentials;
using UnityEngine;

[CreateAssetMenu(fileName = "ShrinkPower", menuName = "Power/ShrinkPower")]
public class ShrinkPower : Power
{
    private bool shrinked = false;
    public override void UsePower()
    {
        if(!shrinked)
        {
            Player.Instance.gameObject.transform.localScale = Vector3.one * 0.1f;
            Player.Instance.GetComponent<Rigidbody>().mass = 0.001f;
            shrinked = true;
        }
        else
        {
            Player.Instance.gameObject.transform.localScale = Vector3.one;
            Player.Instance.GetComponent<Rigidbody>().mass = 1f;
            shrinked = false;
        }
        

        Debug.Log("Shrink Power");
    }
}
