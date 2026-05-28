using UnityEngine;

[System.Serializable]
public class DialogueEvent : TimelineEvent
{
    public GameObject dialoguePrefab;

    public override void Execute(GameManager gm)
    {
        if (dialoguePrefab != null)
        {
            Object.Instantiate(dialoguePrefab);
        }
    }
}