using UnityEngine;

public class SpriteDisplayer : MonoBehaviour
{
    public int curSpriteNum;

    [System.Serializable]
    public struct Sprites
    {
        public Sprite sprite;
        public int spriteNum;
    }

    [SerializeField]
    private Sprites[] sprites;

    [Header("コンポーネント")]
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.sprite = sprites[curSpriteNum].sprite;
    }
}
