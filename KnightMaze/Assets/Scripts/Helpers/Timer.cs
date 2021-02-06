using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour {
	
	#region Fields
	
	// timer duration
	float totalSeconds = 0;
	
	// timer execution
	float elapsedSeconds = 0;
	bool running = false;
	
	// support for Finished property
	bool started = false;
	
	#endregion
	
	#region Properties
	
	/// <summary>
	/// Sets the duration of the timer
	/// The duration can only be set if the timer isn't currently running
	/// </summary>
	/// <value>duration</value>
	public float Duration
    {
		set
        {
			if (!this.running)
            {
                this.totalSeconds = value;
			}
		}
	}

    public float ElapsedSeconds
    {
        get
        {
            return this.totalSeconds - this.elapsedSeconds;
        }
    }
	
	/// <summary>
	/// Gets whether or not the timer has finished running
	/// This property returns false if the timer has never been started
	/// </summary>
	/// <value>true if finished; otherwise, false.</value>
	public bool Finished
    {
		get
        {
            return this.started && !this.running;
        } 
	}
	
	/// <summary>
	/// Gets whether or not the timer is currently running
	/// </summary>
	/// <value>true if running; otherwise, false.</value>
	public bool Running
    {
		get
        {
            return this.running;
        }
	}
	
	#endregion
	
	#region Methods
	
	// Update is called once per frame
	void Update ()
    {
		// update timer and check for finished
		if (this.running)
        {
            this.elapsedSeconds += Time.deltaTime;
			if (this.elapsedSeconds >= this.totalSeconds)
            {
				running = false;
			}
		}
	}
	
	/// <summary>
	/// Runs the timer
	/// Because a timer of 0 duration doesn't really make sense,
	/// the timer only runs if the total seconds is larger than 0
	/// This also makes sure the consumer of the class has actually 
	/// set the duration to something higher than 0
	/// </summary>
	public void Run ()
    {
		if (this.totalSeconds > 0)
        {
            this.started = true;
            this.running = true;
            this.elapsedSeconds = 0;
		}
	}
	
	#endregion
}
