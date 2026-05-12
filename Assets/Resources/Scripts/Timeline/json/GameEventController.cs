using UnityEngine;

public class GameEventController : MonoBehaviour
{
    public GameEventLoader loader;
    public GameEventTimelinePlayer player;

    public string fileName = "test"; // test.json ‚ğ“Ç‚İ‚Ş

    private void Start()
    {
        // JSON “Ç‚İ‚İ
        GameEventData data = loader.Load(fileName);

        // TimelinePlayer ‚É“n‚·
        player.data = data;

        // ƒ^ƒCƒ€ƒ‰ƒCƒ“‚ğÅ‰‚©‚çÄ¶
        player.ResetTimeline();
    }
}
