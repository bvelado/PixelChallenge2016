using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapData {
    public int mapSceneIndex;
    public Transform[] mapSpawnPoints;
    public string mapName;
}
