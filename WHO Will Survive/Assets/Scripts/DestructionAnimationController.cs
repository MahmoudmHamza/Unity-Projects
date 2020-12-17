using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionAnimationController : MonoBehaviour
{
    // cached for efficiency
    private Animator anim;

    /// <summary>
    /// Use for initialization
    /// </summary>
    void Start()
    {
        this.anim = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // destroy the game object if the explosion has finished its animation
        if (this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(this.gameObject);
        }
    }
}
