using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    Timer freezeTimer;

    [SerializeField]
    private GameObject freezeObj;

    const float BounceAngleHalfRange = 60;
    const float halfColliderWidth = 1.2f;

    private bool frozen = false;
    private bool freezeTimerStarted = false;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        freezeTimer = gameObject.AddComponent<Timer>();

        EventsManager.AddFreezeListener(FreezePaddle);
    }
	
    void FixedUpdate()
    {
        if (!frozen)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput != 0)
            {
                transform.position = CalculateClampedX(transform.position);
                Vector3 position = transform.position;
                position.x += horizontalInput *
                    ConfigurationUtils.PaddleMoveUnitsPerSecond *
                    Time.deltaTime;
                transform.position = position;
            }
        }
    }

	void Update () {
        if (freezeTimerStarted)
        {
            if (freezeTimer.Finished)
            {
                frozen = false;
                freezeTimerStarted = false;
            }
            else
            {
                frozen = true;
            }
        }
    }

    Vector3 CalculateClampedX (Vector3 trans)
    {
        if (trans.x > 9.988679f)
        {
            trans.x = -10.18f;
        }
        else if(trans.x < -10.18f)
        {
            trans.x = 9.988679f;
        }

        return trans;
    }

   
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfColliderWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    public void FreezePaddle(float freezeTime)
    {
        Debug.Log("FREEZE");
        frozen = true;
        freezeTimerStarted = true;

        freezeTimer.Duration = freezeTime;
        freezeTimer.Run();

        GameObject newFreeze = Instantiate(freezeObj);
        newFreeze.transform.position = this.transform.position;
    }
}
