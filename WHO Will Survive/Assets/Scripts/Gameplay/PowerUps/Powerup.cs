using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // cached for efficiency
    private Animator anim;

    /// <summary>
    /// Use for initialization
    /// </summary>
    void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // destroy the game object if the powerup has finished its animation
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            this.gameObject.SetActive(false);
        }
    }
}
