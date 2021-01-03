using UnityEngine;

public class StoneButtonEntry : MonoBehaviour
{
    [SerializeField]
    private InfinityStoneType stoneType;

    public InfinityStoneType StoneType => this.stoneType;
}
