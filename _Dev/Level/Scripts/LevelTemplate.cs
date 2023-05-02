using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelObjectType
{
    Cart,
    Customer,
    WetFloor,
    Spikes,
    RegisterLeft,
    RegisterRight,
    AisleLeft,
    AisleRight,
    MovingCustomer
}

public enum ChunkType
{
    Simple,
    HoleLeft,
    HoleRight
}
[Serializable]
public class LevelObject
{
    public LevelObjectType type;
    public Vector2 position;
}

[Serializable]
public class ChunkTemplate
{
    public LevelObject[] objects;
    public ChunkType chunkType;
}
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class LevelTemplate : ScriptableObject
{
    public ChunkTemplate[] chunks;
}
