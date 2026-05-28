using UnityEngine;

public class RemoveEnemyEvent : TimelineEvent
{
    public override void Execute(GameManager gm)
    {
        EnemyManager[] enemies = GameObject.FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);
        foreach (EnemyManager enemy in enemies)
        {
            Object.Destroy(enemy.gameObject);
        }
    }
}
