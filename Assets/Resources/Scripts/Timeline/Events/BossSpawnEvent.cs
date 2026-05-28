using UnityEngine;

[System.Serializable]
public class BossSpawnEvent : TimelineEvent
{
    public GameObject bossPrefab;
    public Vector2 spawnPos = new Vector2(0, 8);

    public override void Execute(GameManager gm)
    {
        if (bossPrefab != null)
        {
            Object.Instantiate(bossPrefab, spawnPos, Quaternion.identity);
            Debug.Log("É{ÉXèoåªÇÒÇÒ??????????");
        }
    }
}