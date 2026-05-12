using UnityEngine;

[System.Serializable]
public class EnemySpawnEvent : TimelineEvent
{
    public GameObject enemyPrefab;
    public Vector2 spawnPos;
    public int HP;
    public override void Execute(GameManager gm)
    {
        var enemy = Object.Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<EnemyManager>().hp = HP;
        gm.lastSpawnedEnemy = enemy.GetComponent<EnemyManager>();
    }
}
