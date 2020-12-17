using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    protected string skillName = "New Name";

    [SerializeField]
    protected Sprite skillIcon = null;

    [SerializeField]
    protected SkillType skillType;

    [SerializeField]
    protected float damageModifier = 1;

    //move to child
    [SerializeField]
    protected float armorPenetrationiModifier = 1;

    [SerializeField]
    protected float cost = 1;

    [SerializeField]
    protected float castTime = 1f;

    [SerializeField]
    protected float cooldown = 1;

    protected GameObject objectToFollow;

    protected Timer skillTimer;

    #endregion

    #region PROPERTIES
    public string SkillName
    {
        get
        {
            return this.skillName;
        }
    }

    public Sprite SkillIcon
    {
        get
        {
            return this.skillIcon;
        }
    }

    public SkillType SkillType
    {
        get
        {
            return this.skillType;
        }
    }

    public float DamageModifier
    {
        get
        {
            return this.damageModifier;
        }
    }

    public float ArmorPenetrationiModifier
    {
        get
        {
            return this.armorPenetrationiModifier;
        }
    }

    public float Cost
    {
        get
        {
            return this.cost;
        }
    }

    public float CastTime
    {
        get
        {
            return this.castTime;
        }
    }

    public float Cooldown
    {
        get
        {
            return this.cooldown;
        }
    }
    #endregion

    public virtual void Awake()
    {
        skillTimer = this.GetComponent<Timer>();
        skillTimer.Duration = this.castTime;
    }

    public virtual void Update()
    {
        if(objectToFollow != null)
        {
            this.gameObject.transform.position = this.objectToFollow.transform.position;
        }
    }

    public virtual void Initialize(GameObject obj)
    {
        this.objectToFollow = obj;
        skillTimer.Run();
    }

    public virtual void SkillEffect()
    {
        //The effect changes from skill to another
    }
}
