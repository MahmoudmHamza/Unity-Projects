using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Ball : MonoBehaviour
{

    Timer deathTimer;

    float impulse = ConfigurationUtils.BallImpulseForce;
    float LifeSeconds = ConfigurationUtils.BallLifeTime;

    private BallSpawner ballspawner;
    
    void Start()
    {
        ballspawner = Camera.main.GetComponent<BallSpawner>();

        GetComponent<Rigidbody2D>().AddForce(
            new Vector2(0, -5f),
            ForceMode2D.Impulse);

        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = LifeSeconds;
        deathTimer.Run();
    }

    void Update()
    {
        DestroyBall();
        if (deathTimer.Finished)
        {
            gameObject.tag = "Dead Ball";
            ballspawner.BallDead = true;
        }
        else
        {
            gameObject.tag = "Ball";
            ballspawner.BallDead = false;
        }
    }


    public void SetDirection(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(
            direction,
            ForceMode2D.Impulse);
    }

    void OnBecameInvisible()
    {
        ballspawner.BallOut = true;
        gameObject.tag = "Ball Out";
    }

    void DestroyBall()
    {
        if (ballspawner.BallOut && gameObject.CompareTag("Ball Out"))
        {
            Destroy(gameObject);
            ballspawner.BallsDestroyedIncrease();
        }
        else if(ballspawner.BallDead && gameObject.CompareTag("Dead Ball"))
        {
            Destroy(gameObject);
            ballspawner.BallsDestroyedIncrease();
        }
    }
}


