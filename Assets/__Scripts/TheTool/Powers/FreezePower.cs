using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezePower", menuName = "Power/FreezePower")]
public class FreezePower : Power
{
    protected override void UsePower()
    {
        Debug.Log("Freeze Power");
    }
}
