using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DInGameMapScroll : MonoBehaviour {
    ///맵스크롤 하는 오브젝트
    ///

    #region Variable
    public GameObject MapSprite = null;
    public Transform targetCamera = null;
    public int size = 1;
    private GameObject[] spriteList = new GameObject[10];
    
    public float spriteSize = 0;
    public float cameraView;
    public float endPos; // 이미지들의 맨 오른쪽 
    private int front = 0;
    private int end = 0;
    

    #endregion 

    #region Virtual Function

    // Use this for initialization
	void Start () {
	    for(int i =0 ; i<size ; i++)
        {
            GameObject temp = Instantiate(MapSprite) as GameObject;
            temp.transform.parent = transform;
            temp.transform.position = new Vector3(i * spriteSize, 0, 0);
            spriteList[i] = temp;
        }
        end = size-1;
        endPos = spriteSize * size;
	}
	
	// Update is called once per frame
	void Update () {
        if (endPos + cameraView < targetCamera.position.x)
        {
            spriteList[front].transform.position = new Vector3(spriteList[end].transform.position.x + spriteSize, 0);
            end = front;
            front = (front + 1) % size;
            endPos += spriteSize;
        }
        
    }
    
    #endregion
}
