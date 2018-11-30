using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PickupEffect
{
    Freezer,
    Speedup
}
public class PickupBlock : Block {

    private int pickPts;
    private PickupEffect pickUpEffect;
    FreezeEffectActivated freezeEffectActivated = new FreezeEffectActivated();
    SpeedEffectActivated speedEffectActivated = new SpeedEffectActivated();
    
	// Use this for initialization
	protected override void Start () {
        pickPts = pickupPoints;
        EventsManager.AddFreezeInvoker(this);
        EventsManager.AddSpeedInvoker(this);
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.CompareTag("Freeze"))
        {
            pickUpEffect = PickupEffect.Freezer;
        }
        else if(gameObject.CompareTag("Speed"))
        {
            pickUpEffect = PickupEffect.Speedup;
        }

	}

    public void EffectState()
    {
        switch (pickUpEffect){
            case PickupEffect.Freezer:
                freezeEffectActivated.Invoke(freezeDuration);
                SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.FreezeBlockHit);
                Debug.Log("FR");
                break;
            case PickupEffect.Speedup:
                speedEffectActivated.Invoke(speedDuration);
                SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.SpeedBlockHit);
                Debug.Log("SP");
                break;
        }
        
    }
    public void AddFreezeEffectListener(UnityAction<float> listener)
    {
        freezeEffectActivated.AddListener(listener);
    }

    public void AddSpeedEffectListener(UnityAction<float> listener)
    {
        speedEffectActivated.AddListener(listener);
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        pointsAddedEvent.Invoke(pickupPoints);
        EffectState();
    }
}
