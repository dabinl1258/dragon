using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MCreateGotoStart : Editor
{
    ////////////////////////////////////////////////////////////////
    // Create GotoStart
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Other/Create GotoStart", validate = true, priority = 3)]
    static public bool GetCreateGotoStartValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Other/Create GotoStart", validate = false, priority = 3)]
    static public void CreateGotoStart()
    {
        MHOMITools.AddComponent("Root_I", "HGotoStart");
    }

}
