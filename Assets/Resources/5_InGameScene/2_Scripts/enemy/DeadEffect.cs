using UnityEngine;
using System.Collections;
public class DeadEffect : MonoBehaviour {
    public bool ableEffect;
    private float gravity = 60.0f;
    Vector3 velocity;
    Vector3 rotation;
    static public Vector3[] velocityArray = new Vector3[]
        {new Vector3(5, 20 , -10)};
    
    public void EnableEffect()
    {
        rotation = new Vector3(Random.Range(180, -180), Random.Range(180, -180), Random.Range(180, -180));
        velocity = new Vector3(Random.Range(-3, 3), 20.0f, Random.Range(-13, 13));
        ableEffect = true;
        
    }
    void Effect()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        transform.localEulerAngles += rotation * Time.deltaTime;
    }
	// Update is called once per frame
    void Start()
    {
        
    }

	void Update () {

	}

    void FixedUpdate()
    {
        if(ableEffect)
        {
            Effect();
        }
    }

    public void Reset()
    {
        ableEffect = false;
        transform.localEulerAngles = Vector3.zero;
    }
    void OnBecameInvisible()
    {
        if (ableEffect)
            gameObject.SetActive(true);
    }
    

}
