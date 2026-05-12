using UnityEngine;

[CreateAssetMenu(fileName = "SpawnTimeline", menuName = "Game/SpawnTimeline")]
public class SpawnTimeline : ScriptableObject
{
    [SerializeReference]
    public TimelineEvent[] events;
}
