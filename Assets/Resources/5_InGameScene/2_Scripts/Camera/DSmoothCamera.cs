using UnityEngine;
using System.Collections;

public class DSmoothCamera : MonoBehaviour
{

    #region Variable
    public Transform target= null;
    public float damSpeed;
    public Vector3 offSet;
    public bool noetAble = false;

    #endregion 
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        if (target == null)
            return;
        SmoothCameraMove();
    }

    void SmoothCameraMove()
    {
        if (noetAble)
            return;
        if (transform.position.x > target.position.x+ offSet.x)
            return;
        float _x = Mathf.Lerp(transform.position.x,target.position.x + offSet.x, Time.deltaTime *damSpeed);
        if (_x < 0.0f)
            return;
        transform.position = new Vector3(_x, transform.position.y+offSet.y, transform.position.z);


    }
}
