using UnityEngine;
using System.Collections;

public class DCreatePlayers : MonoBehaviour {

    public GameObject[] playersArray = null; // 플레이어들 어레이
    public DSmoothCamera sCamera = null;
	// Use this for initialization
	void Start () {
        GameObject temp =  Instantiate( playersArray[DPlayerData.instance.playerIndex]) as GameObject;
        sCamera.target = temp.transform;
        temp.name = "players";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
