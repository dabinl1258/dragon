using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MHomiLibrary;
public class DTestPerSceneMng: HSingletonScene<DTestPerSceneMng>
{
    protected DTestPerSceneMng() { } 

    void Awake()
    {
        cSceneList = new Dictionary<string, HState>();

        for (int i = 0; i < SceneList.Count; i++)
            cSceneList.Add(GetClassName(SceneList[i].ToString()), SceneList[i]);
    }

    void Start()
    {
        HPrefabMng.I.DestroyPrefabs();
        ChangeScene( "DTestPerScene" );
    }
}