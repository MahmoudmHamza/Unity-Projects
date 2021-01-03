using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stonesList;

    private LevelManager LevelManager => LevelManager.Instance;

    private void OnEnable()
    {
        var obtainedStones = this.LevelManager.GetObtainedStoneList();
        foreach(var stone in obtainedStones)
        {
            this.SetStoneState(stone);
        }
    }

    private void SetStoneState(InfinityStoneType type)
    {
        switch(type)
        {
            case InfinityStoneType.Mind:
                this.EnableProgressAnimation(this.stonesList[0]);
                break;
            case InfinityStoneType.Time:
                this.EnableProgressAnimation(this.stonesList[1]);
                break;
            case InfinityStoneType.Reality:
                this.EnableProgressAnimation(this.stonesList[2]);
                break;
            case InfinityStoneType.Power:
                this.EnableProgressAnimation(this.stonesList[3]);
                break;
            case InfinityStoneType.Soul:
                this.EnableProgressAnimation(this.stonesList[4]);
                break;
            case InfinityStoneType.Space:
                this.EnableProgressAnimation(this.stonesList[5]);
                break;
        }
    }

    private void EnableProgressAnimation(GameObject obj)
    {
        var animator = obj.GetComponent<Animator>();
        animator.SetBool("IsObtained", true);
    }
}
