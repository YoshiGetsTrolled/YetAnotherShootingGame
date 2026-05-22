using UnityEngine;

[CreateAssetMenu(fileName = "BossMoveTimeLine", menuName = "Game/BossTimeline")]
public class BossMoveTimeLine : ScriptableObject
{
    [SerializeReference]
    public BossMoveEvents[] events;
}
