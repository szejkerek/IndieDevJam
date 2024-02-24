using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableDisappear : MonoBehaviour, IChargable
{
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    public bool activateByPlateOnly = false, noDischarge = false;

    public void OnCharge(bool byPlate = false)
    {
        if (byPlate || !activateByPlateOnly) obj.SetActive(false);
    }

    public void OnDischarge(bool byPlate = false)
    {
        if (noDischarge) return;
        if (byPlate || !activateByPlateOnly) obj.SetActive(true);
    }

}
