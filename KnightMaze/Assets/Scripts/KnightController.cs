using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls knight's behavior
/// </summary>
public class KnightController : MonoBehaviour
{
    [SerializeField]
    private AudioSource knightAudioSource;

    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private float knightSpeed;

    private GameManager gameManager => GameManager.Instance;

    private AudioManager AudioManager => AudioManager.Instance;

    private Animator animator;

    private Rigidbody2D knightRigidBody;

    private float horizontalInput;

    private float verticalInput;

    private bool isAttacking;

    private void Awake()
    {
        this.animator = this.GetComponentInChildren<Animator>();
        this.knightRigidBody = this.GetComponent<Rigidbody2D>();
        this.weapon.SetActive(false);
        this.isAttacking = false;
    }

    private void Update()
    {
        if (this.gameManager.IsGameEnded)
        {
            return;
        }

        this.HandleAttack();
        this.HandleWeaponRotation();
        this.HandleMovement();
        this.HandleAnimation();
        this.HandleRotation();
    }

    /// <summary>
    /// handles attacking
    /// </summary>
    private void HandleAttack()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (this.isAttacking)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.animator.Play("Attack");
            this.AudioManager.PlaySoundEffect(AudioKey.Attack);
            this.StartCoroutine(this.ActivateWeapon());
        }  
    }

    /// <summary>
    /// Activates weapon object whenever the player attacks
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActivateWeapon()
    {
        this.weapon.SetActive(true);
        this.isAttacking = true;

        yield return new WaitForSeconds(0.3f);
        this.weapon.SetActive(false);
        this.isAttacking = false;
    }

    /// <summary>
    /// Moves player when pressing axis keys
    /// </summary>
    private void HandleMovement()
    {
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
        this.verticalInput = Input.GetAxisRaw("Vertical");

        var direction = new Vector2(this.horizontalInput, this.verticalInput);
        direction.Normalize();

        var movement = direction * this.knightSpeed * Time.deltaTime;
        this.transform.Translate(movement, Space.World);
    }

    /// <summary>
    /// playes walking animation when moving
    /// </summary>
    private void HandleAnimation()
    {
        var isNotMoving = this.horizontalInput == 0 && this.verticalInput == 0;

        this.animator.SetBool("IsMoving", !isNotMoving);
        this.knightAudioSource.enabled = !isNotMoving;
    }

    /// <summary>
    /// Rotates player to the direction of movement 
    /// </summary>
    private void HandleRotation()
    {
        this.transform.rotation = this.horizontalInput > 0 ? Quaternion.LookRotation(new Vector3(0, 0, 1)) :
            this.horizontalInput < 0 ? Quaternion.LookRotation(new Vector3(0, 0, -1)) : this.transform.rotation;
    }

    /// <summary>
    /// handles weapon position around the player while moving
    /// </summary>
    private void HandleWeaponRotation()
    {
        this.weapon.transform.eulerAngles = this.verticalInput > 0f ? new Vector3(0f, 0f, 90f) :
            this.verticalInput < 0f ? new Vector3(0f, 0f, 270f) : this.transform.eulerAngles;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameManager.IsGameEnded)
        {
            return;
        }

        if (collision.CompareTag("Orb"))
        {
            this.AudioManager.PlaySoundEffect(AudioKey.OrbCollected);
            this.gameManager.TriggerOnGameEnded(GameStatus.Win);
            this.animator.Play("Win");
            Destroy(collision.gameObject);
            Debug.Log("You Win");

            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("Spikes"))
        {
            this.gameManager.TriggerOnGameEnded(GameStatus.Lose);
            this.animator.Play("Die");
            Debug.Log("You lose");

            Destroy(this.gameObject);
        }
    }
}
