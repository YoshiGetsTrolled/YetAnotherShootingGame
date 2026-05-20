using System.Collections;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    [Header("オブジェクト参照")]
    public GameObject bulletPrefab;

    [Header("パラメーター")]
    public bool canShoot;
    [Space(10)]
    public float fireRate;      //弾を撃つ間隔
    public Vector2 shootDir;    //弾のベクトル（forward）
    public Vector3 spawnPos;    //スポーン位置
    public float shootSpeed;    //撃った弾の速さ
    public float bulletLife;    //弾が何秒で消えるか
    public int burstAmount;     //同時に撃つ弾の数
    public int spawnAmount;     //撃つ弾の数
    [Space(10)]
    public bool isChasePlayer;  //プレイヤーを追うかどうか
    public bool isCustomSpawn;  //スポーン位置を設定するか（falseの場合Managerの位置から）
    [Space(10)]
    public int damage;


    [Header("複数発射時の変化量")]
    public float addFireRate;
    public Quaternion addShootDir;
    public float addShootSpeed;
    public float addBulletLife;

    [Header("同時に複数発射する時の変化量")]
    public Vector3 addBurstShootDir;
    public float addBurstShootSpeed;
    public Vector3 addBurstPos;
    
    private Vector3 defSpawnPos;

    private float t;
    private Transform player;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerManager>().GetComponent<Transform>();

        if(isCustomSpawn)
        {
            defSpawnPos = spawnPos;
        }

        if (burstAmount == 1)
        {
            addBurstShootDir = Vector3.zero;
            addBurstShootSpeed = 0f;
        }
    }

    private void Update()
    {
        //プレイヤーに向けるベクトル
        if (isChasePlayer)
        {
            shootDir = (player.position - spawnPos).normalized;
        }
        else
        {
            shootDir = transform.right.normalized;
        }

        if (!isCustomSpawn)
        {
            spawnPos = this.transform.position;
            defSpawnPos = this.transform.position;
        }

        if (canShoot)
        {
            t += Time.deltaTime;
        }

        if (t >= fireRate)
        {
            StartCoroutine(ShootRoutine());
            t = 0f;
        }
    }

    private IEnumerator ShootRoutine()
    {
        for (int s = 0; s < spawnAmount; s++)
        {
            //位置リセット
            spawnPos = defSpawnPos;

            // プレイヤー方向を中心にする
            Vector2 centerDir = shootDir.normalized;
            float baseSpeed = shootSpeed;

            // n-way の中心を 0 として左右に振る
            float half = (burstAmount - 1) * 0.5f;

            for (int i = 0; i < burstAmount; i++)
            {
                // i を -half ～ +half に変換
                float offsetIndex = i - half;

                // Z軸回転だけ使う
                float angle = addBurstShootDir.z * offsetIndex;

                Vector2 dir = Quaternion.Euler(0, 0, angle) * centerDir;

                GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                Bullet_Enemy be = bullet.GetComponent<Bullet_Enemy>();

                //生成した弾のパラメーター設定
                be.moveVec = dir;
                be.moveSpeed = baseSpeed;
                be.damage = damage;

                baseSpeed += addBurstShootSpeed;
                spawnPos += addBurstPos;
            }

            yield return new WaitForSeconds(addFireRate);

            shootSpeed += addShootSpeed;
            bulletLife += addBulletLife;
        }
    }
}
