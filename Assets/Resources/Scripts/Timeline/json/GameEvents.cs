using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEvent
{
    public string type;
    public float time;

    [Header("スポーン")]
    public string prefab;
    public Vector2 spawnPos;
    public int HP;
    [Space(10)]
    public bool canShoot;
    public string bulletPrefab;
    public float fireRate;
    public int damage;
    public float rotOffsetEuler;


    [Header("移動設定")]
    public int enemyIndex;
    public Vector2 start;
    public Vector2 end;
    public float duration;
    public bool destroyAfter;

    [Header("ゲーム停止")]
    public float pauseDuration;

    [Header("ランダム出現")]
    public string[] prefabs;
    public Vector2 HPRange;
    public Vector2 speedRange;
    public bool canMoveHorizontally;
    public float randomSpawnDuration;
    public float spawnRate;
    public int seed;
    public bool useSeed;

    [Header("背景スクロール")]
    public int scrollSpeed;

    [Header("ダイアログ")]
    public string dialoguePrefab;
}


[Serializable]
public class GameEventData
{
    public List<GameEvent> events = new List<GameEvent>();
}