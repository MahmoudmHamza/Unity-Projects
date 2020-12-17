using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    private static GameObject fireSkillPrefab;
    private static GameObject iceSkillPrefab;
    private static GameObject sandSkillPrefab;
    private static GameObject rockSkillPrefab;

    private SkillType currentSkill;

    private Dictionary<SkillType, GameObject> skillTypeToObject = new Dictionary<SkillType, GameObject>();

    private GameObject[] selectedSkill = new GameObject[3]; 

    public SkillType CurrentSkill
    {
        get
        {
            return this.currentSkill;
        }
        set
        {
            this.currentSkill = value;
        }
    }

    public GameObject[] SelectedSkills
    {
        get
        {
            return this.selectedSkill;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        this.Initialize();
    }

    private void Initialize()
    {
        fireSkillPrefab = Resources.Load<GameObject>("FireShot");
        iceSkillPrefab = Resources.Load<GameObject>("IceShot");
        sandSkillPrefab = Resources.Load<GameObject>("SandWave");
        rockSkillPrefab = Resources.Load<GameObject>("RockGate");

        skillTypeToObject.Add(SkillType.FireShot, fireSkillPrefab);
        skillTypeToObject.Add(SkillType.IceShot, iceSkillPrefab);
        skillTypeToObject.Add(SkillType.SandWave, sandSkillPrefab);
        skillTypeToObject.Add(SkillType.RockGate, rockSkillPrefab);

        //generic init
        selectedSkill[0] = sandSkillPrefab;
        selectedSkill[1] = iceSkillPrefab;
        selectedSkill[2] = fireSkillPrefab;

        this.currentSkill = SkillType.DefaultShot;
    }
    
    public GameObject GetSkillByType(SkillType type)
    {
        return this.skillTypeToObject[type];
    }
    
    private void OnSkillExecuted(SkillType type, GameObject unit)
    {
        //instantiate skill prefab
        var skillPrefab = Instantiate(this.GetSkillByType(type));
        var skill = skillPrefab.GetComponent<Skill>();

        skill.Initialize(unit);

        this.currentSkill = SkillType.DefaultShot;
    }

    private void OnSkillFailed()
    {
        this.currentSkill = SkillType.DefaultShot;
    }
}
