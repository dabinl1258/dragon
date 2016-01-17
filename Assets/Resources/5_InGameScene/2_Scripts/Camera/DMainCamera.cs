using UnityEngine;
using System.Collections;

public class DMainCamera : MonoBehaviour {
    public static DMainCamera instance;
    public float collisionX;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public float GetCollision ()
    {
        return transform.position.x + collisionX;
    }

}
