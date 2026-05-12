using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEvent
{
    public string type;
    public float time;

    // spawn
    public string prefab;
    public Vector2 spawnPos;
    public int HP;

    // move
    public int enemyIndex;
    public Vector2 start;
    public Vector2 end;
    public float duration;
    public bool destroyAfter;

    // pause
    public float pauseDuration;

    // random spawn
    public string[] prefabs;
    public Vector2 HPRange;
    public Vector2 speedRange;
    public bool canMoveHorizontally;
    public float randomSpawnDuration;
    public float spawnRate;
    public int seed;

    // scroll speed
    public int scrollSpeed;
}


[Serializable]
public class GameEventData
{
    public List<GameEvent> events = new List<GameEvent>();
}