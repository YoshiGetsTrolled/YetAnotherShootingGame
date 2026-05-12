using System.Collections.Generic;
using UnityEngine;

public class GameEventTimelinePlayer : MonoBehaviour
{
    public GameEventData data;      // JSON から読み込んだイベント
    public EnemyManager enemy;      // 命令を送る対象

    private List<EnemyManager> spawnedEnemies = new List<EnemyManager>();

    private float timer = 0f;
    private int index = 0;

    private void Update()
    {
        if (data == null || data.events.Count == 0) return;

        timer += Time.deltaTime;

        // イベントを順番に実行
        while (index < data.events.Count && timer >= data.events[index].time)
        {
            PlayEvent(data.events[index]);
            index++;
        }
    }

    private void PlayEvent(GameEvent e)
    {
        switch (e.type)
        {
            case "spawn":
                GameObject prefab = Resources.Load<GameObject>(e.prefab);
                EnemyManager newEnemy = Instantiate(prefab, e.spawnPos, Quaternion.identity)
                    .GetComponent<EnemyManager>();

                spawnedEnemies.Add(newEnemy);
                break;

            case "move":
                spawnedEnemies[e.enemyIndex].StartMove(e.start, e.end, e.duration, e.destroyAfter);
                break;

            case "shoot":
                spawnedEnemies[e.enemyIndex].StartShoot();
                break;

            case "stopShoot":
                spawnedEnemies[e.enemyIndex].StopShoot();
                break;

            default:
                Debug.LogWarning("ないんだな～そんなイベント：" + e.type);
                break;
        }
    }

    public void ResetTimeline()
    {
        timer = 0f;
        index = 0;
    }
}
