using UnityEngine;

public class Ball : MonoBehaviour
{
    private float LifeSeconds = ConfigurationUtils.BallLifeTime;

    private Timer deathTimer;

    private BallSpawner ballspawner;
    
    private void Start()
    {
        this.ballspawner = Camera.main.GetComponent<BallSpawner>();
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5f), ForceMode2D.Impulse);

        this.InitializeDeathTimer();
    }

    private void Update()
    {
        if (!this.deathTimer.Finished)
        {
            return;
        }

        this.DestroyBall();
    }

    private void InitializeDeathTimer()
    {
        this.deathTimer = gameObject.AddComponent<Timer>();
        this.deathTimer.Duration = this.LifeSeconds;
        this.deathTimer.Run();
    }

    public void SetDirection(Vector2 direction)
    {
        this.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
    }

    private void DestroyBall()
    {
        if(this.ballspawner != null)
        {
            this.ballspawner.OnBallDestroyed();
        }

        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        this.DestroyBall();
    }
}


