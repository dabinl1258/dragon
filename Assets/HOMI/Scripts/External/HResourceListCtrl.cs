using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 로딩한 리소스 저장 해두는 곳입니다. 
/// </summary>
public class HResourceListCtrl : MonoBehaviour {

	// Use this for initialization
     /// <summary>
    /// 현재 씬에서 사용할 리소스저장 리스트
    /// </summary>
    public List<GameObject> ResourceList = null;

    void Awake()
    {
        ResourceList = new List<GameObject>();
    }
    
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/in HResourceListCtrl");
        ResourceList.Clear();
        ResourceList = null;
    }
}
