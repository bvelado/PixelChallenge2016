using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapData {
    public int mapSceneIndex;
    public Vector3[] mapSpawnPoints;
    public string mapName;
}
