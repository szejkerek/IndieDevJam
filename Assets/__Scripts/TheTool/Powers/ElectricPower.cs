using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElectricPower", menuName = "Power/ElectricPower")]
public class ElectricPower : Power
{
    private float radius = 10f;
    public override void UsePower()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Player.Instance.transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.GetComponent<IChargable>() != null)
            {
                Debug.Log(hitCollider.gameObject.name + " charged");
                hitCollider.gameObject.GetComponent<IChargable>().OnCharge();
            }
        }
        Debug.Log("Electric Power");
    }
}
