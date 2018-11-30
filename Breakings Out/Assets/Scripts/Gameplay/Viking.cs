using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MonoBehaviour {

    [SerializeField]
    private float Speed = 1.5f;

    private BallSpawner ballspawner;

    Timer audioTimer;

    void Start () {
        ballspawner = Camera.main.GetComponent<BallSpawner>();

        MoveRight();

        audioTimer = gameObject.AddComponent<Timer>();
        audioTimer.Duration = 30f;
        audioTimer.Run();
    }
	
	void Update () {

        if (transform.position.x >= 5f)
        {
            MoveLeft();
        }
        else if (transform.position.x <= -5f)
        {
            MoveRight();
        }

        AudioKing();
    }

    public void MoveRight()
    {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void MoveLeft()
    {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-Speed, GetComponent<Rigidbody2D>().velocity.y);
        
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            ballspawner.HitKing = true;
            SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.KingKilled);
            Destroy(gameObject);
        }
    }

    public void AudioKing()
    {
        if (audioTimer.Finished)
        {
            SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.EnemySound[Random.Range(0,5)]);
            audioTimer.Run();
        }
    }
}
