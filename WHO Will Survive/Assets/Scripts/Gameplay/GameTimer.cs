using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private const float CountdownDuration = 180f;

    [SerializeField]
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Finished)
        {
            //trigger win
            EventsManager.Instance.TriggerOnGameEnded(true);
        }
    }

    private void StartTimer()
    {
        timer.Duration = CountdownDuration;
        timer.Run();
    }
}
