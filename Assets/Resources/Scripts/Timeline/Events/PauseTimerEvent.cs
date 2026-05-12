using UnityEngine;

[System.Serializable]
public class PauseTimerEvent : TimelineEvent
{
    public float pauseDuration;
    public override void Execute(GameManager gm)
    {
        gm.TogglePause();
    }
}
