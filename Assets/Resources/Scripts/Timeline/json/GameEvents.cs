using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEvent
{
    public string type; //move,shoot,stopshoot
    public float time;

    public string prefab;
    public Vector2 spawnPos;

    public int enemyIndex;  //“G‚Ì”Ô†Š„‚è“–‚Ä

    public Vector2 start;
    public Vector2 end;
    public float duration;
    public bool destroyAfter;
}

[Serializable]
public class GameEventData
{
    public List<GameEvent> events = new List<GameEvent>();
}