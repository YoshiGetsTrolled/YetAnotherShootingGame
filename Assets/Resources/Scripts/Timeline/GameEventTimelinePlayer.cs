using System.Collections.Generic;
using UnityEngine;

public class GameEventTimelinePlayer : MonoBehaviour
{
    [Header("タイムラインデータ")]
    // ここに作成した SpawnTimeline アセットをドラッグ&ドロップします
    public SpawnTimeline timelineData;

    [Header("参照")]
    public GameManager gm;

    private float timer = 0f;
    private int index = 0;

    private void Start()
    {
        // GameManagerを自動取得（シーンに1つであることを前提）
        if (gm == null) gm = FindFirstObjectByType<GameManager>();

        ResetTimeline();
    }

    private void Update()
    {
        // タイムラインやGameManagerがない場合は何もしない
        if (gm == null || timelineData == null || timelineData.events == null) return;

        // GameManagerから現在の経過時間を取得
        timer = gm.curTime;

        // 現在のIndexがイベント数より少なく、かつ実行時間に達している場合
        while (index < timelineData.events.Length && timer >= timelineData.events[index].triggerTime)
        {
            // 各イベントクラスのExecuteを呼び出す
            timelineData.events[index].Execute(gm);

            // 次のイベントへ
            index++;
        }
    }

    // タイムラインを最初からやり直すためのメソッド
    public void ResetTimeline()
    {
        timer = 0f;
        index = 0;
        Debug.Log("タイムラインをリセットしました");
    }
}