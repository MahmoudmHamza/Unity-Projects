using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezing : MonoBehaviour {

    private Animator anim;
    Timer freezeTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        freezeTimer = gameObject.AddComponent<Timer>();
        freezeTimer.Duration = 2;
        freezeTimer.Run();
        //freeze sound
    }

    void Update()
    {
        // destroy the game object if the explosion has finished its animation
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && freezeTimer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
