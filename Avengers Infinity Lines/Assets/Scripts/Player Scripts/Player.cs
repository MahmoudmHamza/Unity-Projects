using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string avengerName;

    [SerializeField]
    private Sprite avengerIcon;

    [SerializeField]
    private Rigidbody2D avengerRigidBody;

    [SerializeField]
    private AudioClip avengerVoiceOver;

    public Sprite Icon => this.avengerIcon;

    public event Action OnPlayerDestroyed;

    public event Action OnStoneCollected;

    private GameManager GameManager => GameManager.Instance;

    private Collider2D avengerCollider;

    private bool isStarted = false;

    void Start()
    {
        this.avengerCollider = this.GetComponent<Collider2D>();
    }

    void Update ()
    {
        this.HandleKeyboardInput();
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetButtonDown("Start") && !this.GameManager.IsSelectingStone)
        {
            this.avengerRigidBody.bodyType = RigidbodyType2D.Dynamic;
            AudioManager.Instance.PlayAudioClip(this.avengerVoiceOver);
        }
    }

    private void CheckAvengerState()
    {
        if (!this.isStarted)
        {
            return;
        }

        if(this.avengerRigidBody.velocity.magnitude <= 0)
        {
            this.TriggerPlayerDestroyed();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Destroy")
        {
            if (this.GameManager.IsPowerStoneEnabled)
            {
                this.DestroyEnemy(other);
                return;
            }

            this.TriggerPlayerDestroyed();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Finish")
        {
            this.TriggerStoneCollected();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void DestroyEnemy(Collider2D collidedEnemy)
    {
        var enemy = collidedEnemy.GetComponent<Enemy>();
        this.GameManager.EnemyController.DestroyEnemy(enemy);
        Destroy(collidedEnemy.gameObject);
        this.GameManager.IsPowerStoneEnabled = false;
    }

    private void TriggerPlayerDestroyed()
    {
        this.OnPlayerDestroyed?.Invoke();
    }

    private void TriggerStoneCollected()
    {
        this.OnStoneCollected?.Invoke();
    }
}
