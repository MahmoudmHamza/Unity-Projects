using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSkill : Skill
{
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (skillTimer.Finished || !objectToFollow.activeSelf)
        {
            Destroy(this.gameObject);
        }
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
    }
}
