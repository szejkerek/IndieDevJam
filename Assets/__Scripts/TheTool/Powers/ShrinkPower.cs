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
            PlayUseSound();
            shrinked = true;
        }
        else
        {
            Player.Instance.gameObject.transform.localScale = Vector3.one;
            shrinked = false;
        }
        

        Debug.Log("Shrink Power");
    }
}
