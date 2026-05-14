using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour , IDamageable , IPlayerInteractive
{
    [Header("パラメーター")]
    public int hp = 5;

    public bool canShoot;
    public float shootInterval;
    private float shootTimer;
    [Header("弾")]
    public Vector2 shootVec;
    public float shootSpeed;
    public Vector2 playerDir;
    private GameObject player;

    [Header("弾の管理システム")]
    public string bulletPatternName;

    [Header("行動設定")]
    public Vector2 startPos;
    public Vector2 endPos;
    public float moveSpeed;
    public bool destroyAfterMove;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>().gameObject;
    }

    private void Update()
    {
        //範囲外で削除
        if (transform.position.x > 15 ||
            transform.position.x < -15 ||
            transform.position.y > 15 ||
            transform.position.y < -15)
        {
            Destroy(gameObject);
        }

        //撃つよん
        if (canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0;
            }
        }


        //プレイヤーに向いている、正規化したベクトル
        if (player != null)
        {
            playerDir = (player.transform.position - transform.position).normalized;
        }
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void StartMove(Vector2 start, Vector2 end,float duration,bool destroy)
    {
        startPos = start; 
        endPos = end;
        moveSpeed = duration;
        destroyAfterMove = destroy;

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator  MoveCoroutine()
    {
        float t = 0;
        while(t < 1f)
        {
            t += Time.deltaTime / moveSpeed;
            transform.position = Vector2.Lerp(startPos,endPos, t);
            yield return null;
        }
        if (destroyAfterMove)
        {
            Destroy(this.gameObject);
        }
    }

    public void StartShoot()
    {
        canShoot = true;
    }

    public void StopShoot()
    {
        canShoot = false;
    }




    #region interface

    public void OnPlayerTouch(PlayerController player, PlayerManager manager)
    {
        manager.ChangeHP(-1);
        Destroy(gameObject);
    }

    #endregion
}
