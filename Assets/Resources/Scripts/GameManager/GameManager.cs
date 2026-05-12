using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("パラメーター")]
    public bool isPause;

    [SerializeField]
    float curTime;

    [Header("オブジェクト、コンポーネント")]
    public TextMeshProUGUI debugText;

    public SpawnTimeline timeline;
    private int nextIndex = 0;

    public EnemyManager lastSpawnedEnemy;

    private void Start()
    {
        curTime = 0.0f;
    }

    private void Update()
    {
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
}
