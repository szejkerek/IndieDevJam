using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    float massTotal = 0, massRequired = 1;
    [SerializeField]
    List<GameObject> chargableObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null) massTotal += other.GetComponent<Rigidbody>().mass;
        foreach (GameObject g in chargableObjects)
        {
            if (massTotal >= massRequired && g.GetComponent<IChargable>() != null)
            {
                g.GetComponent<IChargable>().OnCharge(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null) massTotal -= other.GetComponent<Rigidbody>().mass;
        foreach (GameObject g in chargableObjects)
        {
            if (massTotal < massRequired && g.GetComponent<IChargable>() != null)
            {
                g.GetComponent<IChargable>().OnDischarge(true);
            }
        }
    }
}
