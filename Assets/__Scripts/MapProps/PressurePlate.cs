using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    float massTotal = 0, massRequired = 1;
    [SerializeField]
    List<GameObject> chargableObjects;
    public UnityEvent onPlateDown;
    public UnityEvent onPlateUp;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null) massTotal += other.GetComponent<Rigidbody>().mass;
        foreach (GameObject g in chargableObjects)
        {
            if (massTotal >= massRequired && g.GetComponent<IChargable>() != null)
            {
                Debug.Log("Stepped on");
                g.GetComponent<IChargable>().OnCharge(true);
                onPlateDown?.Invoke();
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
                onPlateUp?.Invoke();
            }
        }
    }
}
