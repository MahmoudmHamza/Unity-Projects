using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]
    private Sprite frozenStar;

    [SerializeField]
    private Sprite moltenstar;

    private SpriteRenderer spriteRenderer;

    private float currentMeltingValue;

    public float CurrentMeltingValue
    {
        get
        {
            return this.currentMeltingValue;
        }
        set
        {
            this.currentMeltingValue = value;
        }
    }

    private void Awake()
    {
        EventsManager.Instance.OnStarMelted += OnStarMelted;
        EventsManager.Instance.OnStarFroze += OnStarFroze;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EventsManager.Instance.TriggerOnStarHit(GameConstants.PlayerBulletDamage);
            AudioManager.Instance.Play(AudioClipName.StarHit);
            Destroy(collision.gameObject);
        }
    }

    private void OnStarMelted()
    {
        spriteRenderer.sprite = moltenstar;
    }

    private void OnStarFroze()
    {
        spriteRenderer.sprite = frozenStar;
    }
}
