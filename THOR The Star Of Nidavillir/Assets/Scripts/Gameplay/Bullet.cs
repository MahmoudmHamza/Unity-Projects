using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The bullet class
/// </summary>
public class Bullet : MonoBehaviour
{
    private Timer deathTimer;

    private void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = GameConstants.DeathTimerDuration;
        deathTimer.Run();
    }

    private void Update()
    {
        if (deathTimer.Finished)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Move the bullet when fired
    /// </summary>
    /// <param name="direction">the bullet direction</param>
    public void ApplyForce(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(
            direction * GameConstants.BulletForce, 
            ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
