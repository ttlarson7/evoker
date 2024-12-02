using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizardController : MonoBehaviour
{


    public Animator animator;

    public void startAnimation()
    {
        animator.SetBool("casting", true);
    }
    private void stopAnimation()
    {
        animator.SetBool("casting", false);
    }

    
}
