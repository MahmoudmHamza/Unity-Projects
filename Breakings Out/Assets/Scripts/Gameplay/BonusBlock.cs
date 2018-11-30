using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block {

    private int bonusPts;

	// Use this for initialization
	protected override void Start () {
        bonusPts = bonusPoints;
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        pointsAddedEvent.Invoke(bonusPoints);
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BonusBlockHit);
    }
}
