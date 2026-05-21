using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossEnemyManager : MonoBehaviour
{
    [Header("オブジェクト参照")]
    private EnemyManager em;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI bossNameObject;
    private Animator animator;

    [Header("パラメーター")]
    public int maxHp;
    [SerializeField] private string bossNameText;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bossNameObject.text = bossNameText;
        em = GetComponentInChildren<EnemyManager>();
        maxHp = em.hp;
    }

    private void Update()
    {

        if (em.hp != 0) hpBar.fillAmount = (float)em.hp / maxHp;
        else hpBar.fillAmount = 0;

        if (em.hp <= 0)
        {
            animator.SetTrigger("BossEnd");
            Destroy(gameObject,5f);
        }
    }
}
