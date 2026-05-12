using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("パラメーター")]
    public bool isPause;

    [SerializeField]
    float curTime;
    public float bgScrollSpeed;

    [Header("オブジェクト、コンポーネント")]
    public TextMeshProUGUI debugText;
    public GameObject[] bgObjects;

    public SpawnTimeline timeline;
    private int nextIndex = 0;

    public EnemyManager lastSpawnedEnemy;

    private void Start()
    {
        Application.targetFrameRate = 60;
        curTime = 0.0f;
    }

    private void Update()
    {
        BGScroll();

        //現在の時間を更新
        if (!isPause)
        {
            curTime += Time.deltaTime;
        }

        //イベント実行
        if (nextIndex < timeline.events.Length)
        {
            var e = timeline.events[nextIndex];

            if (curTime >= e.triggerTime)
            {
                e.Execute(this);
                nextIndex++;
            }
        }


        if (debugText)
        {
            debugText.text = curTime.ToString();
        }
    }



    public void TogglePause()
    {
        isPause = !isPause;
    }

    void BGScroll()
    {
        // 背景を動かす
        foreach (var bg in bgObjects)
        {
            bg.transform.Translate(0, bgScrollSpeed * Time.deltaTime, 0);
        }

        // 上方向にスクロール（bgScrollSpeed > 0）
        if (bgScrollSpeed > 0)
        {
            // 画面上に完全に出たら下に戻す
            if (bgObjects[0].transform.position.y >= 10)
            {
                bgObjects[0].transform.position = new Vector2(-2.22f, -10);
            }
            if (bgObjects[1].transform.position.y >= 10)
            {
                bgObjects[1].transform.position = new Vector2(-2.22f, -10);
            }
        }
        // 下方向にスクロール（bgScrollSpeed < 0）
        else
        {
            if (bgObjects[0].transform.position.y <= -10)
            {
                bgObjects[0].transform.position = new Vector2(-2.22f, 10);
            }
            if (bgObjects[1].transform.position.y <= -10)
            {
                bgObjects[1].transform.position = new Vector2(-2.22f, 10);
            }
        }
    }

}
