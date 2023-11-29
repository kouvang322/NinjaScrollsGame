using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKills : MonoBehaviour
{
   public int NumberOfEnemyKills {  get; private set; }

    public UnityEvent<PlayerKills> OnEnemyKilled;

    public void EnemyKilled()
    {
        NumberOfEnemyKills++;
        OnEnemyKilled.Invoke(this);
    }
}
