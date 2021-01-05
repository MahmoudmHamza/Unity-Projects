using UnityEngine;

public class Viking : MonoBehaviour {

    [SerializeField]
    private float Speed = 1.5f;

    private BallSpawner ballspawner;

    private Timer audioTimer;

    private void Start()
    {
        this.ballspawner = Camera.main.GetComponent<BallSpawner>();

        this.MoveDirection(1);
        this.InitializeKingAudioTimer();
    }

    private void Update()
    {
        this.MoveKing();
        this.KingAudioController();
    }

    private void InitializeKingAudioTimer()
    {
        this.audioTimer = this.gameObject.AddComponent<Timer>();
        this.audioTimer.Duration = 30f;
        this.audioTimer.Run();
    }

    private void MoveKing()
    {
        if (this.transform.position.x >= 5f)
        {
            this.MoveDirection(-1);
        }
        else if (this.transform.position.x <= -5f)
        {
            this.MoveDirection(1);
        }
    }

    private void MoveDirection(int direction)
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * direction, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void KingAudioController()
    {
        if (!this.audioTimer.Finished)
        {
            return;
        }

        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.EnemySound[Random.Range(0, 5)]);
        this.audioTimer.Run();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            this.ballspawner.OnKingKilled();
            SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.KingKilled);
            Destroy(this.gameObject);
        }
    }
}
