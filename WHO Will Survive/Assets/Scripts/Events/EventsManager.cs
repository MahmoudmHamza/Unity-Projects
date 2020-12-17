using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : Singleton<EventsManager>
{
    public event Action<int> OnUnitDestroyed;

    public event Action<int> OnUnitEscaped;

    public event Action<bool> OnGameEnded;

    public event Action OnTimerIncreased;

    public event Action OnTimerDecreased;

    public event Action OnSanetizationAvailable;

    public event Action OnSanetizationActivated;

    public void TriggerOnUnitDestroyed(int recoveredCases)
    {
        var handler = this.OnUnitDestroyed;
        if (handler != null)
        {
            handler.Invoke(recoveredCases);
        }
    }

    public void TriggerOnUnitEscaped(int confirmedCases)
    {
        var handler = this.OnUnitEscaped;
        if (handler != null)
        {
            handler.Invoke(confirmedCases);
        }
    }

    public void TriggerOnGameEnded(bool state)
    {
        var handler = this.OnGameEnded;
        if (handler != null)
        {
            handler.Invoke(state);
        }
    }

    public void TriggerOnTimerIncreased()
    {
        var handler = this.OnTimerIncreased;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnTimerDecreased()
    {
        var handler = this.OnTimerDecreased;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnSanetizationAvailable()
    {
        var handler = this.OnSanetizationAvailable;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnSanetizationActivated()
    {
        var handler = this.OnSanetizationActivated;
        if (handler != null)
        {
            handler.Invoke();
        }
    }
}
