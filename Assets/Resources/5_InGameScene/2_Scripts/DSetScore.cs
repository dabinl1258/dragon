using UnityEngine;
using System.Collections;

public class DSetScore : MonoBehaviour {
    TextMesh textMesh = null;
    public DInGameScore dis;
    public string message;
	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if(dis == null)
        {
            dis = GameObject.Find("players").GetComponent<DInGameScore>();
        }
        textMesh.text = message + dis.GetTotalScore().ToString();
	}
}
