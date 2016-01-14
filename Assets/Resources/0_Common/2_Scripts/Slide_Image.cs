using UnityEngine;
using System.Collections;

public class Slide_Image : MonoBehaviour {
    //public GameObject obj;
    //public Vector2 deltaPosition;
    //public float fSpeed = 1.0f;
    //public Vector2 prePos;
    //public Vector2 nowPos;
    //public Vector3 movPos;
	// Use this for initialization
	void Start () {
	
	}
    void Update()
    {
        int fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;

        }
        if (fingerCount > 0)
            Debug.Log(fingerCount);

    }

    // Update is called once per frame
    //void Update () {
    //if(Input.touchCount == 1)
    //       {
    //           Touch touch = Input.GetTouch(0);
    //           if (touch.phase == TouchPhase.Began)
    //           {
    //               prePos = touch.position - touch.deltaPosition;
    //               Debug.Log(prePos);
    //               Debug.Log("a");
    //           }
    //           else if (touch.phase == TouchPhase.Began)
    //           {
    //               nowPos = touch.position - touch.deltaPosition;
    //               movPos = (Vector3)(prePos - nowPos) * fSpeed;
    //               obj.transform.Translate(movPos);
    //               prePos = touch.position - touch.deltaPosition;
    //               Debug.Log(movPos);
    //           }
    //       }
    //   if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
    //       {
    //           Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
    //           transform.Translate(-touchDeltaPosition.x * fSpeed, -touchDeltaPosition.y 
    //               * fSpeed, 0);
    //       }
    //}
}
