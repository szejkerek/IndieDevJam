using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezePower", menuName = "Power/FreezePower")]
public class FreezePower : Power
{
    private float radius = 10f;
    public override void UsePower()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Player.Instance.transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.GetComponent<Rigidbody>() != null)
            {
                Debug.Log(hitCollider.gameObject.name);
                Rigidbody rb = hitCollider.gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = !rb.isKinematic;
            }
        }
        Debug.Log("Freeze Power");
    }
}
