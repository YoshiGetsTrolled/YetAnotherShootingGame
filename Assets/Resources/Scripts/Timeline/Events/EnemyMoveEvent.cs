using UnityEngine;

[System.Serializable]
public class EnemyMoveEvent : TimelineEvent
{
    public EnemyManager target;
    public Vector2 startPos;
    public Vector2 endPos;
    public float duration;
    public bool destroyAfterMove;

    public override void Execute(GameManager gm)
    {
        if (target == null)
        {
            target = gm.lastSpawnedEnemy;
        }
        if (target != null)
        {
            target.StartMove(startPos, endPos, duration);
            target.destroyAfterMove = destroyAfterMove;
        }
    }
}
