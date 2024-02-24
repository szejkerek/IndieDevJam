using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableKinematicSwitch : MonoBehaviour, IChargable
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    public bool activateByPlateOnly = false, noDischarge = false;

    public void OnCharge(bool byPlate = false)
    {
        if (byPlate || !activateByPlateOnly) rb.isKinematic = false;
    }

    public void OnDischarge(bool byPlate = false)
    {
        if (noDischarge) return;
        if (byPlate || !activateByPlateOnly) rb.isKinematic = true;
    }

}
