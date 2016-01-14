using UnityEngine;
using System.Collections;

public class DMonter : MonoBehaviour {

    public float moveSpeed;
    public GameObject particle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        transform.position += new Vector3(-moveSpeed, 0, 0) * Time.deltaTime;
    }

    public void Hit()
    {
        GameObject temp = Instantiate(particle) as GameObject;
        temp.transform.position = transform.position;
        Destroy(gameObject);
        Debug.Log("hit");
    }
}
