using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTimelinePlayer : MonoBehaviour
{
    [Header("オブジェクト")]
    public GameEventData data;      // JSON から読み込んだイベント
    public EnemyManager enemy;      // 命令を送る対象
    public GameManager gm;

    [Header("ファイル参照用パス")]
    public string enemiesPath = "Enemies/";
    public string bulletPath = "BulletPatterns/";
    public string dialoguePath = "DialogueUI/";
    public string bossPath = "Bosses/";

    private List<EnemyManager> spawnedEnemies = new List<EnemyManager>();

    private float timer = 0f;
    private int index = 0;

    private void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        timer = gm.curTime;
        if (data == null || data.events.Count == 0) return;

        // イベントを順番に実行
        while (index < data.events.Count && timer >= data.events[index].time)
        {
            PlayEvent(data.events[index]);
            index++;
        }
    }

    #region EventPlayer

    //時間に応じてイベントを実行
    private void PlayEvent(GameEvent e)
    {
        GameObject prefab = null;
        GameObject bulletPrefab = null;
        GameManager gm = FindFirstObjectByType<GameManager>();
        switch (e.type)
        {
            case "spawn":
                //リソースを読み込み
                prefab = Resources.Load<GameObject>(enemiesPath + e.prefab);
                bulletPrefab = Resources.Load<GameObject>(bulletPath + e.bulletPrefab);
                //敵生成処理
                Spawn(e, prefab, bulletPrefab);
                break;

            case "randomSpawn":
                StartCoroutine(RandomSpawnEvent(e));
                break;

            case "move":
                if (spawnedEnemies[e.enemyIndex] != null)
                {
                    spawnedEnemies[e.enemyIndex].StartMove(e.start, e.end, e.duration, e.destroyAfter);
                }
                break;

            case "shoot":
                if (spawnedEnemies[e.enemyIndex] != null && 
                    spawnedEnemies[e.enemyIndex].bulletManager != null)
                {
                    spawnedEnemies[e.enemyIndex].StartShoot();
                }
                break;

            case "stopShoot":
                if (spawnedEnemies[e.enemyIndex] != null &&
                    spawnedEnemies[e.enemyIndex].bulletManager != null)
                {
                    spawnedEnemies[e.enemyIndex].StopShoot();
                }
                break;

            case "pause":

                gm.TogglePause();
                break;

            case "scrollSpeedChange":
                gm.bgScrollSpeed = e.scrollSpeed;
                break;

            case "startDialogue":
                GameObject dialogueObject = Resources.Load<GameObject>(dialoguePath + e.dialoguePrefab);
                if (dialogueObject != null) // nullチェックを追加すると安全
                {
                    GameObject dialogue = Instantiate(dialogueObject);
                }
                else
                {
                    Debug.LogError($"ダイアログが見つかりません: {dialoguePath + e.dialoguePrefab}");
                }
                break;

            case "spawnBoss":
                GameObject bossObject = Resources.Load<GameObject>(bossPath + e.bossPrefab);
                if (bossObject != null)
                {
                    GameObject newBoss = Instantiate(bossObject);
                }
                break;

            default:
                Debug.LogError("ないんだな～そんなイベント：" + e.type);
                break;
        }
    }

    #endregion

    public void ResetTimeline()
    {
        timer = 0f;
        index = 0;
    }

    #region EventActions

    //ランダム出現コルーチン
    private IEnumerator RandomSpawnEvent(GameEvent e)
    {
        //固定シードを設定
        UnityEngine.Random.InitState(e.seed);

        if (!e.useSeed)
        {
            //固定シード値を使わない場合、現在時刻のミリ秒でシード値をランダム化
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        }
        float t = 0f;

        while (t < e.randomSpawnDuration)
        {
            t += Time.deltaTime;

            //確率
            float chance = UnityEngine.Random.Range(0f, 100f);

            if (chance < e.spawnRate)
            {
                // 方向を設定
                int dir = UnityEngine.Random.Range(0, 3);

                Vector2 spawnPos = Vector2.zero;

                if (e.canMoveHorizontally)
                {   //方向に応じて出現位置もランダムで設定
                    spawnPos = dir switch
                    {
                        0 => new Vector2(-10, UnityEngine.Random.Range(-3f, 4f)),
                        1 => new Vector2(10, UnityEngine.Random.Range(-3f, 4f)),
                        _ => new Vector2(UnityEngine.Random.Range(-8f, 3.5f), 6)
                    };
                }
                else
                {   //方向を固定する場合は上だけ
                    spawnPos = new Vector2(UnityEngine.Random.Range(-8f, 3.5f), 6);
                }

                // 敵をランダムで選択
                string prefabName = e.prefabs[UnityEngine.Random.Range(0, e.prefabs.Length)];
                GameObject prefab = Resources.Load<GameObject>(enemiesPath + prefabName);

                var enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
                var em = enemy.GetComponent<EnemyManager>();

                em.hp = (int)UnityEngine.Random.Range(e.HPRange.x, e.HPRange.y + 1);

                // 移動先の位置を方向に応じて指定
                Vector2 endPos = e.canMoveHorizontally
                    ? (dir == 0 ? new Vector2(10, spawnPos.y)
                    : dir == 1 ? new Vector2(-10, spawnPos.y)
                    : new Vector2(spawnPos.x, -6))
                    : new Vector2(spawnPos.x, -6);

                //移動開始させる
                em.StartMove(spawnPos, endPos, UnityEngine.Random.Range(e.speedRange.x, e.speedRange.y + 1), true);
            }

            yield return null;
        }
        yield return null;
    }

    private void Spawn(GameEvent e,GameObject prefab,GameObject bulletPrefab)
    {
        //敵を生成
        EnemyManager newEnemy = Instantiate(prefab, e.spawnPos, Quaternion.identity)
            .GetComponent<EnemyManager>();

        newEnemy.hp = e.HP;

        //設定されていたら弾を生成
        if (bulletPrefab != null)
        {
            GameObject newBullet = Instantiate(bulletPrefab, 
                                               newEnemy.transform.position,
                                               bulletPrefab.transform.rotation,
                                               newEnemy.transform);

            EnemyBulletManager bulletManager = newBullet.GetComponent<EnemyBulletManager>();

            bulletManager.canShoot = e.canShoot;
            bulletManager.fireRate = e.fireRate;
            bulletManager.damage = e.damage;
            bulletManager.transform.rotation *= Quaternion.Euler(0,0,e.rotOffsetEuler);
        }

        //リストに追加
        spawnedEnemies.Add(newEnemy);
    }
    #endregion
}
