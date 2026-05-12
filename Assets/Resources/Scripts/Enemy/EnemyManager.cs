using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour , IDamageable
{
    [Header("パラメーター")]
    public int hp = 5;

    public bool canShoot;
    public bool shootInterval;

    public Vector2 shootVec;
    public float shootSpeed;

    [Header("行動設定")]
    public Vector2 startPos;
    public Vector2 endPos;
    public float moveSpeed;
    public bool destroyAfterMove;

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


}
