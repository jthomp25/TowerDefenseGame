using System;
using System.Collections.Generic;

[System.Serializable]
public class TowerSaveData
{
    public float x;
    public float y;
    public float z;
    public int prefabIndex; // which tower type it is
}

[System.Serializable]
public class GameSaveData
{
    public int coins;
    public int health;
    public int waveIndex;
    public int sceneIndex;
    public List<TowerSaveData> towers = new List<TowerSaveData>();
}