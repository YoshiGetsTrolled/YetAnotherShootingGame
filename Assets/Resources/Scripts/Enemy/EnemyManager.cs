using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour , IDamageable , IPlayerInteractive
{
    [Header("パラメーター")]
    public int hp = 5;
    [Header("オブジェクト参照")]
    public EnemyBulletManager bulletManager;
    [SerializeField] private GameObject deathParticle;

    [Header("弾の管理システム")]
    public string bulletPatternName;

    [Header("行動設定")]
    public Vector2 startPos;
    public Vector2 endPos;
    public float moveSpeed;
    public bool destroyAfterMove;

    private void Start()
    {
        bulletManager = GetComponentInChildren<EnemyBulletManager>();
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
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (deathParticle)
            {
                GameObject particle = Instantiate(deathParticle, transform.position, transform.rotation);
                Destroy(particle,2f);
            }
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
        bulletManager.canShoot = true;
    }

    public void StopShoot()
    {
        bulletManager.canShoot = false;
    }




    #region interface

    public void OnPlayerTouch(PlayerController player, PlayerManager manager)
    {
        if (manager.canHitBullet)
        {
            manager.ChangeHP(-1);
            manager.StartCoroutine(manager.Invincible(manager.invincibleTime));
        }
        if (deathParticle)
        {
            GameObject particle = Instantiate(deathParticle, transform.position, transform.rotation);
            Destroy(particle, 2f);
        }
        TakeDamage(1);
    }

    #endregion
}
