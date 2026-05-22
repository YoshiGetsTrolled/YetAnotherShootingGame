using UnityEngine;

public class RemoveBulletEvent : BossMoveEvents
{
    public bool isDeleteBullet;
    public bool isDeleteBossEnemy;
    public override void Execute(EnemyManager em)
    {
        if (isDeleteBullet)
        {
            // ボス本体の子要素にある弾の削除
            foreach (Transform child in em.transform)
            {
                // 弾のコンポーネントがついているか、名前で判定
                if (child.name.Contains("B_"))
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }
        if (isDeleteBossEnemy)
        {
            // シーン内の "BulletEnemy" タグが付いたものを全削除する
            //（とりあえず実装）
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletEnemy");
            foreach (GameObject b in bullets) Object.Destroy(b);
        }
    }
}
