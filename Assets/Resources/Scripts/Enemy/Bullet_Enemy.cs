using UnityEngine;

public class Bullet_Enemy : MonoBehaviour , IPlayerInteractive
{
    [Header("ˆÚ“®Ý’è")]
    public Vector2 moveVec;
    public float moveSpeed;
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ˆê‰ž30•b‚ÅÁ‚¦‚é‚æ‚¤‚É
        Destroy(gameObject, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVec * moveSpeed * Time.deltaTime);

        if (transform.position.y > 10.0f || transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }

    #region interface

    public void OnPlayerTouch(PlayerController player, PlayerManager manager)
    {
        manager.ChangeHP(-damage);
        Destroy(gameObject);
    }

    #endregion
}
