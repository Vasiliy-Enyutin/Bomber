using System;
using UnityEngine;

public class GlobalEventStorage
{
    public static event Action OnPlayerDestroy;
    public static event Action OnEnemyDestroy;

    public static void InvokePlayerDestroy()
    {
        OnPlayerDestroy?.Invoke();
    }

    public static void InvokeEnemyDestroy()
    {
        OnEnemyDestroy?.Invoke();
    }
}
