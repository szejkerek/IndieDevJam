using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class InteractibleScrew : MonoBehaviour, IInteractable
{
    Animator animator;
    Rigidbody rb;
    bool startedAnimation = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && startedAnimation)
        {
            OnAnimationEnd();
            startedAnimation = false;
        }
    }

    private void OnAnimationEnd()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void OnInteract()
    {
        startedAnimation = true;
        animator.SetBool("Animate", true);
    }
}
