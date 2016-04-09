using UnityEngine;
using System.Collections;

public class SetInvincibleDEBUG : MonoBehaviour {
    void Start () {
        GetComponent<PlayerController>().Init(3, Color.red);
        GetComponent<PlayerController>().SetInvincible();
	}
}
