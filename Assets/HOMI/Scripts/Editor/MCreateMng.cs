using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MCreateMng : Editor 
{
    ////////////////////////////////////////////////////////////////
    // Create 0_Mng
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Mng/Create Mng", validate = true, priority = 0)]
    static public bool GetCreateMngValidator()
    {
        bool bChk = (GameObject.Find("0_Mngs") == null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Mng/Create Mng", validate = false, priority = 0)]
    static public void CreateMng()
    {
        MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");
    }
}