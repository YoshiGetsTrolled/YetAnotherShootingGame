using UnityEngine;

public class SpawnBulletEvent : BossMoveEvents
{
    public bool isCustomSpawn;
    public GameObject bullet;

    public Vector2 transform;

    public override void Execute(EnemyManager em)
    {
        if (bullet == null) return;

        if (isCustomSpawn)
        {
            GameObject spawn = GameObject.Instantiate(bullet, transform, bullet.transform.rotation,em.transform.parent);
        }
        else
        {
            // É{ÉXÇÃà íu (em.transform.position) Ç…ê∂ê¨
            GameObject spawn = GameObject.Instantiate(bullet, em.transform.position, bullet.transform.rotation, em.gameObject.transform);
            EnemyBulletManager ebm = spawn.GetComponent<EnemyBulletManager>();
            if (ebm != null)
            {
                ebm.canShoot = true;
            }
        }


    }
}