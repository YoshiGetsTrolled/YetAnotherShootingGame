using UnityEngine;

public class ScrollSpeedChangeEvent : TimelineEvent
{
    public float newScrollSpeed;
    public override void Execute(GameManager gm)
    {
        gm.bgScrollSpeed = newScrollSpeed;
    }
}
