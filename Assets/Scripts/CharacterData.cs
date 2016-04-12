using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data")]
public class CharacterData : ScriptableObject {
    public Transform characterPortraitSprite;
    public Transform characterModel;
    public GameObject characterWinAnimated;
    public string characterName;
}
