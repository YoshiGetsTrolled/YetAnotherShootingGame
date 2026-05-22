using UnityEngine;

[System.Serializable]
public abstract class BossMoveEvents
{
    public float triggerTime;
    public abstract void Execute(EnemyManager em);
}
