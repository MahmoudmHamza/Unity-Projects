using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> unitSprites = new List<Sprite>();

    [SerializeField]
    private GameObject destructionEffect;

    [SerializeField]
    private GameObject pickEffect;

    private float unitSpeed = 0;

    private SpriteRenderer renderer;

    private UnitType currentUnitType;

    private void Awake()
    {
        this.unitSpeed = LevelManager.Instance.UnitSpeed;
        this.renderer = this.GetComponent<SpriteRenderer>();
    }

    public void Initialize(Vector3 position, UnitType type)
    {
        if(type == UnitType.Virus)
        {
            EventsManager.Instance.OnSanetizationActivated += this.OnSanetizationActivated;
        }

        this.currentUnitType = type;
        this.SetSpriteBasedOnType(type);
        this.gameObject.transform.position = position;
        var direction = this.UnitDirection();
        this.MoveUnit(direction);
    }

    private void SetSpriteBasedOnType(UnitType type)
    {
        switch (type)
        {
            case UnitType.Virus:
                this.renderer.sprite = this.unitSprites[0];
                break;
            case UnitType.Timer:
                this.renderer.sprite = this.unitSprites[1];
                break;
        }
    }

    private void MoveUnit(Vector2 direction)
    {
        this.GetComponent<Rigidbody2D>().AddForce(
            direction 
            * this.unitSpeed,
            ForceMode2D.Impulse);
    }

    private Vector2 UnitDirection()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return direction;
    }

    /// <summary>
    /// Unit takes damage when clicked
    /// </summary>
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(this.currentUnitType == UnitType.Timer)
        {
            this.PickOrDestroyTimer();
            return;
        }

        this.DestroyVirus();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameConstants.BordersTag)
        {
            if(this.currentUnitType != UnitType.Timer)
            {
                this.DestroyVirus(true);
            }
            else
            {
                this.PickOrDestroyTimer(true);
            }
        }
    }

    private void PickOrDestroyTimer(bool isDismissed = false)
    {
        if (!isDismissed)
        {
            EventsManager.Instance.TriggerOnTimerDecreased();
            AudioManager.Instance.Play(AudioClipName.TimerPicked);
        }

        this.PlayPickEffects();
        GameManager.Instance.TimersList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void DestroyVirus(bool isEscaped = false, bool sanetizationActivated = false)
    {
        if (!isEscaped)
        {
            int casesNumber = Random.Range(
                (int)LevelManager.Instance.RecoveredCasesMinValue,
                (int)LevelManager.Instance.RecoveredCasesMaxValue);

            EventsManager.Instance.TriggerOnUnitDestroyed(casesNumber);

            if (!sanetizationActivated)
            {
                AudioManager.Instance.Play(AudioClipName.VirusHit);
            }
        }
        else
        {
            int casesNumber = Random.Range(
                (int)LevelManager.Instance.SuspectedCasesMinValue,
                (int)LevelManager.Instance.SuspectedCasesMaxValue);

            EventsManager.Instance.TriggerOnUnitEscaped(casesNumber);
            EventsManager.Instance.TriggerOnTimerIncreased();

            AudioManager.Instance.Play(AudioClipName.VirusEscaped);
        }

        this.PlayDestructionEffects();
        GameManager.Instance.UnitsList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnSanetizationActivated()
    {
        this.DestroyVirus(false, true);
    }

    private void PlayDestructionEffects()
    {
        var destructionEffect = Instantiate(this.destructionEffect) as GameObject;
        destructionEffect.transform.position = this.gameObject.transform.position;
    }

    private void PlayPickEffects()
    {
        var pickEffect = Instantiate(this.pickEffect) as GameObject;
        pickEffect.transform.position = this.gameObject.transform.position;
    }

    private void OnDestroy()
    {
        if(this.currentUnitType == UnitType.Virus && EventsManager.Instance != null)
        {
            EventsManager.Instance.OnSanetizationActivated -= this.OnSanetizationActivated;
        }
    }
}
