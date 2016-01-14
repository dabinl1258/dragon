using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;


/// <summary>
/// ��ġ :  0_Mngs
/// ���ӿ����̴� ����Ÿ�� DB���� �о�� �����صδ� ���Դϴ�
/// �̰��� ������ �ϴ°��Դϴ�. 
/// </summary>
public class HMng : HSingleton<HMng>
{
    protected HMng() { }

    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
       
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/HMng.cs");
    }
}