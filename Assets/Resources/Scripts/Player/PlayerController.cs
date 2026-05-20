using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("コンポーネント")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteDisplayer spriteDisplayer;
    [SerializeField] private PlayerManager pm;

    [Header("動き")]
    public float moveSpeed = 1f;
    [SerializeField] private Vector2 bulletOffset = new Vector2(0,0.2f);
    [SerializeField] private float fireRate = 0.1f; // 連射間隔
    private float fireTimer = 0f;

    [Header("リソース")]
    [SerializeField] GameObject bullet;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteDisplayer = GetComponent<SpriteDisplayer>();
        pm = GetComponent<PlayerManager>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Inputs.moveInput * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        SetSprite();
        BulletInput();
    }

    private void BulletInput()
    {
        fireTimer += Time.deltaTime;

        // Aボタン長押しで連射
        if (Inputs.buttonA && fireTimer >= fireRate)
        {
            ShootBullet(new Vector2(0, 1), 12f);
            fireTimer = 0f;
        }

        // Bボタン長押しで3way連射
        if (Inputs.buttonB && pm.canUse3way && fireTimer >= fireRate)
        {
            ShootBullet(new Vector2(0, 1), 12f);
            ShootBullet(new Vector2(0.3f, 1), 10f);
            ShootBullet(new Vector2(-0.3f, 1), 10f);
            fireTimer = 0f;
        }
    }

    private void ShootBullet(Vector2 dir,float speed)
    {
        //取得
        GameObject b = Instantiate(bullet);
        BulletController bulletController = b.GetComponent<BulletController>();

        //座標設定
        b.transform.position = new Vector2(this.transform.position.x + bulletOffset.x,
                                           this.transform.position.y + bulletOffset.y);

        //弾の設定
        bulletController.moveVec = dir;
        bulletController.moveSpeed = speed;
    }


    void SetSprite()
    {
        Vector2 dir = Inputs.moveInput;

        if (dir == Vector2.zero)
        {
            spriteDisplayer.curSpriteNum = 0; // 待機
            return;
        }

        // 方向判定
        if (dir.x < 0 && dir.y > 0) spriteDisplayer.curSpriteNum = 1; // 左上
        else if (dir.x == 0 && dir.y > 0) spriteDisplayer.curSpriteNum = 2; // 上
        else if (dir.x > 0 && dir.y > 0) spriteDisplayer.curSpriteNum = 3; // 右上
        else if (dir.x < 0 && dir.y == 0) spriteDisplayer.curSpriteNum = 4; // 左
        else if (dir.x > 0 && dir.y == 0) spriteDisplayer.curSpriteNum = 5; // 右
        else if (dir.x < 0 && dir.y < 0) spriteDisplayer.curSpriteNum = 6; // 左下
        else if (dir.x == 0 && dir.y < 0) spriteDisplayer.curSpriteNum = 7; // 下
        else if (dir.x > 0 && dir.y < 0) spriteDisplayer.curSpriteNum = 8; // 右下
    }

    #region interface

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactive = collision.GetComponent<IPlayerInteractive>();
        if (interactive != null)
        {
            interactive.OnPlayerTouch(this,pm);
        }
    }

    #endregion
}
