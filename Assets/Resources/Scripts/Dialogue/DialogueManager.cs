using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

[Serializable]
public class DialogueText
{
    public enum Speaker
    {
        Left,
        Right,
    }
    public Speaker curSpeaker;

    public Sprite iconLeft;
    public Sprite iconRight;

    public string name;
    [TextArea(3, 5)] // インスペクターで見やすくするための設定
    public string text;
}

public class DialogueManager : MonoBehaviour
{
    GameManager gm;

    [Header("設定")]
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private bool canStopTimer;
    private Animator animator;

    [Header("テキストオブジェクト")]
    public TextMeshProUGUI charaNameObject;
    public TextMeshProUGUI textBox;

    [Header("画像オブジェクト")]
    public Image imgLeft;
    public Image imgRight;
    [Space(10)]
    public Sprite invisibleSprite;

    private int curDialogueNum = 0;
    private Coroutine activeCoroutine;
    private bool isTyping = false; // 文字が表示途中かどうか

    private void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        if (gm != null && canStopTimer)
        {
            gm.isPause = true;
        }
        animator = GetComponent<Animator>();
        if (dialogueTexts != null && dialogueTexts.Length > 0)
        {
            StartDialogue(0);
        }
    }

    private void Update()
    {
        // とりあえずスペースキーで会話進める
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnScreenClick();
        }
    }

    // クリックされた時の処理
    private void OnScreenClick()
    {
        if (isTyping)
        {
            // 文字が表示途中なら、一瞬で全文字を表示する
            StopCoroutine(activeCoroutine);
            textBox.maxVisibleCharacters = textBox.text.Length;
            isTyping = false;
        }
        else
        {
            // 次の会話へ
            curDialogueNum++;
            if (curDialogueNum < dialogueTexts.Length)
            {
                StartDialogue(curDialogueNum);
            }
            else
            {
                Debug.Log("会話が終了しました");
                animator.SetTrigger("EndDialogue");
                
                //タイマーを戻す
                gm.isPause = false;

                //2秒後に消滅
                Destroy(gameObject, 2f);
            }
        }
    }

    // 指定した番号の会話を開始する
    private void StartDialogue(int num)
    {
        curDialogueNum = num;
        charaNameObject.text = dialogueTexts[num].name;

        // 画像のセット
        if (dialogueTexts[num].iconLeft != null) imgLeft.sprite = dialogueTexts[num].iconLeft;
        else imgLeft.sprite = invisibleSprite;
        if (dialogueTexts[num].iconRight != null) imgRight.sprite = dialogueTexts[num].iconRight;
        else imgRight.sprite = invisibleSprite;

            // 話し手に応じてキャラクターの明暗（色）を変える
            SetSpeakerHighlight(dialogueTexts[num].curSpeaker);

        // 文字の演出を開始
        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(PlayText(dialogueTexts[num].text));
    }

    // 1文字ずつ表示するコルーチン（修正版）
    private IEnumerator PlayText(string targetText)
    {
        isTyping = true;

        textBox.text = targetText; // 最初から全文を代入
        textBox.maxVisibleCharacters = 0; // 表示文字数を0にする

        // TextMeshProの情報の更新を待つ
        textBox.ForceMeshUpdate();
        int totalVisibleCharacters = targetText.Length;

        for (int i = 0; i <= totalVisibleCharacters; i++)
        {
            textBox.maxVisibleCharacters = i; // 表示する文字数を1つずつ増やす
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    // 話し手を目立たせる演出用のメソッド
    private void SetSpeakerHighlight(DialogueText.Speaker speaker)
    {
        if (speaker == DialogueText.Speaker.Left)
        {
            animator.SetBool("isSpeakerRight",false);
            imgLeft.color = Color.white; // 明るく
            imgRight.color = new Color(0.5f, 0.5f, 0.5f, 1.0f); // 暗く
        }
        else
        {
            animator.SetBool("isSpeakerRight", true);
            imgLeft.color = new Color(0.5f, 0.5f, 0.5f, 1.0f); // 暗く
            imgRight.color = Color.white; // 明るく
        }
    }

    // インスペクターからデータを登録するための配列
    [Header("会話データ")]
    public DialogueText[] dialogueTexts;
}