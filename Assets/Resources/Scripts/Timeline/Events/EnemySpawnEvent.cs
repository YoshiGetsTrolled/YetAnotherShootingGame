using UnityEngine;

[System.Serializable]
public class EnemySpawnEvent : TimelineEvent
{
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public Vector2 spawnPos;
    public int HP;
    public bool canShoot;
    public override void Execute(GameManager gm)
    {
        var enemy = Object.Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<EnemyManager>().hp = HP;

        if (bulletPrefab  != null )
        {
            var bullet = Object.Instantiate(bulletPrefab, spawnPos, bulletPrefab.transform.rotation, enemy.transform);
            bullet.GetComponent<EnemyBulletManager>().canShoot = canShoot;
        }

        gm.lastSpawnedEnemy = enemy.GetComponent<EnemyManager>();
    }
}
