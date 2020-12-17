using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject thrustPrefab;

    [SerializeField]
    private GameObject firingPoint;

    private Rigidbody2D rigidbody;
    private Vector2 thrustDirection;
    private bool isOverHeated = false;
    private bool isAudioPlayed = false;
    private bool isMoving = false;

    private void Awake()
    {
        EventsManager.Instance.OnPlayerDestroyed += OnPlayerDestroyed;
        EventsManager.Instance.OnOverHeated += OnOverHeated;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
        HandlePlayerRotation();
        if (!isOverHeated)
        {
            HandleFireBullet();
        }
    }

    /// <summary>
    /// Handles Player Movement
    /// </summary>
    private void HandlePlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) || GameManager.Instance.IsGameFinished)
        {
            this.rigidbody.velocity = Vector2.zero;
            thrustPrefab.SetActive(false);
            return;
        }

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if((horizontalInput != 0 || verticalInput != 0))
        {
            thrustPrefab.SetActive(true);
        }
        else
        {
            thrustPrefab.SetActive(false);
        }

        this.thrustDirection = new Vector2(horizontalInput, verticalInput);
        this.rigidbody.AddForce(this.thrustDirection * GameConstants.ShipThrustForce);
    }

    /// <summary>
    /// Handles Player Rotation
    /// </summary>
    private void HandlePlayerRotation()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // change thrust direction to match ship rotation
        float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
        this.thrustDirection.x = Mathf.Cos(zRotation);
        this.thrustDirection.y = Mathf.Sin(zRotation);
    }

    /// <summary>
    /// The method that fires bullet
    /// </summary>
    private void HandleFireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.firingPoint.transform.position;
            bullet.transform.rotation = this.transform.rotation;

            Bullet bulletController = bullet.GetComponent<Bullet>();
            bulletController.ApplyForce(this.thrustDirection);

            EventsManager.Instance.TriggerOnBulletFired();
            AudioManager.Instance.Play(AudioClipName.ShipFire);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameObjectTag = collision.gameObject.tag;
        switch (gameObjectTag)
        {
            case "Asteroid":
                EventsManager.Instance.TriggerOnDamageTaken(GameConstants.AsteroidDamage);
                AudioManager.Instance.Play(AudioClipName.ShipHit);
                GameManager.Instance.UnregisterAsteroid(collision.gameObject);
                break;

            case "MiniAsteroid":
                EventsManager.Instance.TriggerOnDamageTaken(GameConstants.MiniAsteroidDamage);
                Destroy(collision.gameObject);
                break;

            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            EventsManager.Instance.TriggerOnDamageTaken(GameConstants.StarBurnDamage);
            if (!isAudioPlayed)
            {
                AudioManager.Instance.Play(AudioClipName.StarHit);
                isAudioPlayed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            isAudioPlayed = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            EventsManager.Instance.TriggerOnDamageTaken(GameConstants.StarBurnDamage);
            if (!isAudioPlayed)
            {
                AudioManager.Instance.Play(AudioClipName.StarHit);
                isAudioPlayed = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            isAudioPlayed = false;
        }
    }

    private void OnPlayerDestroyed()
    {
        AudioManager.Instance.Play(AudioClipName.ShipDestruction);
        Destroy(this.gameObject);
    }

    private void OnOverHeated(bool state)
    {
        isOverHeated = state;
    }
}
