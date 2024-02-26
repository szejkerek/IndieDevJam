using System.Collections;
using GordonEssentials;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaperworkPower", menuName = "Power/PaperworkPower")]
public class PaperworkPower : Power
{
    [SerializeField]
    private GameObject paperworkReference;

    public override void UsePower()
    {
        PlayUseSound();
        GameObject paperWork = Instantiate(paperworkReference);
        paperWork.transform.position = Player.Instance.paperworkPivot.transform.position;
        paperWork.transform.rotation = Player.Instance.paperworkPivot.transform.rotation;

        paperWork.GetComponent<Rigidbody>().AddForce(paperWork.transform.forward * 5 , ForceMode.Impulse);
        Debug.Log("Paperwork Power");
    }
}

