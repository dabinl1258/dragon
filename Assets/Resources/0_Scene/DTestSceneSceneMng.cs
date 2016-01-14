using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MHomiLibrary;
public class DTestSceneSceneMng: HSingletonScene<DTestSceneSceneMng>
{
    protected DTestSceneSceneMng() { } 

    void Awake()
    {
        cSceneList = new Dictionary<string, HState>();

        for (int i = 0; i < SceneList.Count; i++)
            cSceneList.Add(GetClassName(SceneList[i].ToString()), SceneList[i]);
    }

    void Start()
    {
        HPrefabMng.I.DestroyPrefabs();
        ChangeScene( "DTestSceneScene" );
    }
}