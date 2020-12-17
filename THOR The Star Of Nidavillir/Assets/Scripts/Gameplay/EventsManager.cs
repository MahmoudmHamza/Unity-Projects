using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : Singleton<EventsManager>
{
    public event Action<float> OnDamageTaken;

    public event Action<float> OnStarHit;

    public event Action OnPlayerDestroyed;

    public event Action OnStarMelted;

    public event Action OnStarFroze;

    public event Action OnWeaponForged;

    public event Action OnBulletFired;

    public event Action<bool> OnOverHeated;

    public override void Awake()
    {
        base.Awake();
    }

    public void TriggerOnDamageTaken(float damage)
    {
        var handler = this.OnDamageTaken;
        if(handler != null)
        {
            handler.Invoke(damage);
        }
    }

    public void TriggerOnStarHit(float damage)
    {
        var handler = this.OnStarHit;
        if (handler != null)
        {
            handler.Invoke(damage);
        }
    }

    public void TriggerOnPlayerDestroyed()
    {
        var handler = this.OnPlayerDestroyed;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnStarMelted()
    {
        var handler = this.OnStarMelted;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnStarFroze()
    {
        var handler = this.OnStarFroze;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnWeaponForged()
    {
        var handler = this.OnWeaponForged;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnBulletFired()
    {
        var handler = this.OnBulletFired;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    public void TriggerOnOverHeated(bool state)
    {
        var handler = this.OnOverHeated;
        if (handler != null)
        {
            handler.Invoke(state);
        }
    }
}
