using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class PlayerManager : MonoBehaviour
{
    [Header("パラメーター")]
    public int maxHp;
    public int hp;
    public int score;
    public float invincibleTime = 1.5f;
    public bool canHitBullet;

    public bool canUse3way;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image item3way;

    private void Start()
    {
        hp = maxHp;
        score = 0;
        UpdateUI();
    }

    public void ChangeHP(int h)
    {
        hp += h;
        UpdateUI();
        if (hp <= 0)
        {
            GameOver();
        }
    }

    public void AddScore(int num)
    {
        score += num;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (hpText != null && scoreText != null)
        {
            hpText.text = hp.ToString();
            scoreText.text = score.ToString();
        }
        if (canUse3way)
        {
            item3way.color = new Color(100,100,100);
        }
        else
        {
            item3way.color =new Color(0.1f,0.1f,0.1f);
        }
    }

    void GameOver()
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        gm.isPause = true;
    }

    public IEnumerator Invincible(float time)
    {
        canHitBullet = false;
        yield return new WaitForSeconds(time);
        canHitBullet = true;
    }
}
