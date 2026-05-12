using UnityEngine;
using System;

public class RandomSpawnEvent : TimelineEvent
{
    public GameObject[] enemies;
    public Vector2 HPRange = new Vector2(1, 2);
    public Vector2 speedRange = new Vector2(1, 3);

    [Header("ランダム設定")]
    public bool canMoveHorizontally = false;
    public float randomSpawnDuration = 3f;
    public float spawnRate = 30f;
    public int seed = 100;

    private enum Dir
    {
        Left,
        Right,
        Top
    };

    public static T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public override void Execute(GameManager gm)
    {
        gm.StartCoroutine(SpawnCoroutine(gm));
    }

    private System.Collections.IEnumerator SpawnCoroutine(GameManager gm)
    {
        UnityEngine.Random.InitState(seed);
        float t = 0f;

        while (t < randomSpawnDuration)
        {
            t += Time.deltaTime;

            float chance = UnityEngine.Random.Range(0f, 100f);

            if (chance < spawnRate)
            {
                // ランダム方向
                Dir dir = GetRandomEnumValue<Dir>();

                Vector2 spawnPos = Vector2.zero;
                //方向に応じてスポーン位置を変更
                if (canMoveHorizontally)
                {
                    spawnPos = dir switch
                    {
                        Dir.Left => new Vector2(-10, UnityEngine.Random.Range(-3f, 3f)),
                        Dir.Right => new Vector2(10, UnityEngine.Random.Range(-3f, 3f)),
                        Dir.Top => new Vector2(UnityEngine.Random.Range(-6f, 5f), 6),
                        _ => Vector2.zero
                    };
                }
                //上からのみ出現する場合
                else
                {
                    spawnPos = new Vector2(UnityEngine.Random.Range(-6f, 5f), 6);
                }

                    //敵の設定、出現
                    var prefab = enemies[UnityEngine.Random.Range(0, enemies.Length)];
                var enemy = UnityEngine.Object.Instantiate(prefab, spawnPos, Quaternion.identity);
                enemy.GetComponent<EnemyManager>().hp =
                    (int)UnityEngine.Random.Range(HPRange.x, HPRange.y + 1);

                //最後に出現させた敵として割り当て
                gm.lastSpawnedEnemy = enemy.GetComponent<EnemyManager>();



                //移動

                //移動位置設定
                Vector2 endPos = Vector2.zero;
                if (canMoveHorizontally)
                {
                    endPos = dir switch
                    {
                        Dir.Left => new Vector2(10, spawnPos.y),
                        Dir.Right => new Vector2(-10, spawnPos.y),
                        Dir.Top => new Vector2(spawnPos.x, -6),
                        _ => Vector2.zero
                    };
                }
                else
                {
                    endPos = new Vector2(spawnPos.x, -6);
                }

                //コンポーネント取得、移動処理開始
                EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
                enemyManager.StartMove(spawnPos, endPos, UnityEngine.Random.Range(speedRange.x, speedRange.y + 1),true);
            }

            yield return null;
        }
    }
}
