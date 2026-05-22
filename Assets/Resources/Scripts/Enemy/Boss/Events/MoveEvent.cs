using UnityEngine;

public class MoveEvent : BossMoveEvents
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float duration;

    public override void Execute(EnemyManager em)
    {
        em.StartMove(startPos,endPos,duration,false);
    }
}
