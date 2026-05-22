using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossEnemyManager : MonoBehaviour
{
    [Header("オブジェクト参照")]
    private EnemyManager em;
    private GameManager gm;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI bossNameObject;
    private Animator animator;

    [Header("パラメーター")]
    public int maxHp;
    [SerializeField] private string bossNameText;

    [Header("タイムライン設定")]
    [SerializeField] private BossMoveTimeLine timeline;
    private int eventIndex = 0;
    public float timer;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bossNameObject.text = bossNameText;
        em = GetComponentInChildren<EnemyManager>();
        gm = GameObject.FindFirstObjectByType<GameManager>().GetComponent<GameManager>();

        //タイマーを止める
        gm.isPause = true;

        // EnemyManagerの初期HPを最大HPとして記録
        maxHp = em.hp;
        timer = 0f;
    }

    private void Update()
    {
        // HPバーの更新（ゼロ除算防止）
        if (maxHp > 0)
        {
            hpBar.fillAmount = Mathf.Clamp01((float)em.hp / maxHp);
        }

        // タイムラインの再生
        if (timeline != null && eventIndex < timeline.events.Length)
        {
            timer += Time.deltaTime;

            // 指定の時間になったらイベントを実行
            if (timer >= timeline.events[eventIndex].triggerTime)
            {
                timeline.events[eventIndex].Execute(em);
                eventIndex++;
            }
        }

        // ボス撃破時の処理
        if (em.hp <= 0)
        {
            animator.SetTrigger("BossEnd");
            // 撃破後はイベントが走らないようにする
            timeline = null;

            gm.isPause = false;

            Destroy(gameObject, 5f);
        }
    }

    public void ResetBossTimer()
    {
        timer = 0f;
        eventIndex = 0;
        Debug.Log("ボスのタイマーをリセットしたあああああああああああああ７８６ｑｑｇｙｖばえ８お９ｗ");
    }
}