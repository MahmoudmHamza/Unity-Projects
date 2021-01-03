using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer stoneRenderer;

    [SerializeField]
    private List<Sprite> stoneSprites;
    
    public void InitializeStone(InfinityStoneType type)
    {
        switch (type)
        {
            case InfinityStoneType.Mind:
                this.stoneRenderer.sprite = this.stoneSprites[0];
                break;
            case InfinityStoneType.Time:
                this.stoneRenderer.sprite = this.stoneSprites[1];
                break;
            case InfinityStoneType.Soul:
                this.stoneRenderer.sprite = this.stoneSprites[2];
                break;
            case InfinityStoneType.Power:
                this.stoneRenderer.sprite = this.stoneSprites[3];
                break;
            case InfinityStoneType.Reality:
                this.stoneRenderer.sprite = this.stoneSprites[4];
                break;
            case InfinityStoneType.Space:
                this.stoneRenderer.sprite = this.stoneSprites[5];
                break;
        }
    }
}
