using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Block : MonoBehaviour {

    protected BallSpawner ballspawner;

    protected int totalScore = 0;
    protected int standardPoints = ConfigurationUtils.StandardBlockPoints;
    protected int bonusPoints = ConfigurationUtils.BonusBlockPoints;
    protected int pickupPoints = ConfigurationUtils.PickupBlockPoints;
    protected float freezeDuration = ConfigurationUtils.FreezeDuration;
    protected float speedDuration = ConfigurationUtils.SpeedDuration;

    protected PointsAddedEvent pointsAddedEvent = new PointsAddedEvent();
    protected BlockDestroyedEvent blockDestroyedEvent = new BlockDestroyedEvent();

    protected virtual void Start () {
        EventsManager.AddPointsInvoker(this);
        EventsManager.AddBlocksInvoker(this);
    }
	

	void Update () {

    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.StandardBlockHit);
            Destroy(this.gameObject);
            blockDestroyedEvent.Invoke(0);
            pointsAddedEvent.Invoke(standardPoints);
        }
    }

    public void AddPointsEventListener(UnityAction<int> listener)
    {
        pointsAddedEvent.AddListener(listener);
    }

    public void BlockDestroyedListener(UnityAction<int> listener)
    {
        blockDestroyedEvent.AddListener(listener);
    }
}
