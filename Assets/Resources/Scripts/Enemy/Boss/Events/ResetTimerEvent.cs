using UnityEngine;

public class ResetTimerEvent : BossMoveEvents
{
    public override void Execute(EnemyManager em)
    {
        BossEnemyManager boss = em.GetComponentInParent<BossEnemyManager>();

        if (boss != null)
        {
            boss.ResetBossTimer();
        }
    }
}
