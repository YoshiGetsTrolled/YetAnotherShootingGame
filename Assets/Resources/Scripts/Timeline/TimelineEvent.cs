using UnityEngine;
//Šî’êƒNƒ‰ƒX

[System.Serializable]
public abstract class TimelineEvent
{
    public float triggerTime;
    public abstract void Execute(GameManager gm);
}
