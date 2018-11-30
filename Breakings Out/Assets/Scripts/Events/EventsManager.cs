using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventsManager {

    static PickupBlock speedInvoker;
    static UnityAction<float> speedListener;
    static PickupBlock freezeInvoker;
    static UnityAction<float> freezeListener;
    static Block pointsInvoker;
    static UnityAction<int> pointsListener;
    static Block blockInvoker;
    static UnityAction<int> blockListener;
    static BallSpawner remainInvoker;
    static UnityAction<int> remainListener;

    public static void AddFreezeInvoker(PickupBlock script)
    {
        freezeInvoker = script;
        if (freezeListener != null)
        {
            freezeInvoker.AddFreezeEffectListener(freezeListener);
        }
    }

    public static void AddFreezeListener(UnityAction<float> handler)
    {
        freezeListener = handler;
        if (freezeInvoker != null)
        {
            freezeInvoker.AddFreezeEffectListener(freezeListener);
        }
    }

    public static void AddSpeedInvoker(PickupBlock script)
    {
        speedInvoker = script;
        if (speedListener != null)
        {
            speedInvoker.AddSpeedEffectListener(speedListener);
        }
    }

    public static void AddSpeedListener(UnityAction<float> handler)
    {
        speedListener = handler;
        if (speedInvoker != null)
        {
            speedInvoker.AddSpeedEffectListener(speedListener);
        }
    }

    public static void AddPointsInvoker(Block script)
    {
        pointsInvoker = script;
        if (pointsListener != null)
        {
            pointsInvoker.AddPointsEventListener(pointsListener);
        }
    }

    public static void AddPointsListener(UnityAction<int> handler)
    {
        pointsListener = handler;
        if (pointsInvoker != null)
        {
            pointsInvoker.AddPointsEventListener(pointsListener);
        }
    }

    public static void AddRemainInvoker(BallSpawner script)
    {
        remainInvoker = script;
        if (remainListener != null)
        {
            remainInvoker.BallRemainingEventListener(remainListener);
        }
    }

    public static void AddremainListener(UnityAction<int> handler)
    {
        remainListener = handler;
        if (remainInvoker != null)
        {
            remainInvoker.BallRemainingEventListener(remainListener);
        }
    }

    public static void AddBlocksInvoker(Block script)
    {
        blockInvoker = script;
        if (blockListener != null)
        {
            blockInvoker.BlockDestroyedListener(blockListener);
        }
    }

    public static void AddBlocksListener(UnityAction<int> handler)
    {
        blockListener = handler;
        if (blockInvoker != null)
        {
            blockInvoker.BlockDestroyedListener(blockListener);
        }
    }
}
