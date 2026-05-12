using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("ˆÚ“®Ý’è")]
    public Vector2 moveVec;
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ˆê‰ž3•b‚ÅÁ‚¦‚é‚æ‚¤‚É
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVec * moveSpeed * Time.deltaTime);

        if (transform.position.y > 10.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();

        if (target != null)
        {
            target.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
