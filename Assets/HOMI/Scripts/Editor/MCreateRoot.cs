using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MCreateRoot : Editor
{
    ////////////////////////////////////////////////////////////////
    // Create Root I
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Root/Create Root_I", validate = true, priority = 0)]
    static public bool GetCreateRootIValidator()
    {
        bool bChk = (GameObject.Find("Root_I") == null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Root/Create Root_I", validate = false, priority = 0)]
    static public void CreateRootI()
    {
        MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
    }

    ////////////////////////////////////////////////////////////////
    // Create Root II
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Root/Create Root_II", validate = true, priority = 1)]
    static public bool GetCreateRootIIValidator()
    {
        bool bChk = (GameObject.Find("Root_II") == null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Root/Create Root_II", validate = false, priority = 1)]
    static public void CreateRootII()
    {
        MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");
    }

    ////////////////////////////////////////////////////////////////
    // Create Root III
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Root/Create Root_III", validate = true, priority = 2)]
    static public bool GetCreateRootIIIValidator()
    {
        bool bChk = (GameObject.Find("Root_III") == null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Root/Create Root_III", validate = false, priority = 2)]
    static public void CreateRootIII()
    {
        MHOMITools.CreateResourceObject("Prefabs/Root/Root_III", "Root_III");
    }


    ////////////////////////////////////////////////////////////////
    // Create Root IV
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Root/Create Root_IV", validate = true, priority = 3)]
    static public bool GetCreateRootIVValidator()
    {
        bool bChk = (GameObject.Find("Root_IV") == null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Root/Create Root_IV", validate = false, priority = 3)]
    static public void CreateRootIV()
    {
        MHOMITools.CreateResourceObject("Prefabs/Root/Root_IV", "Root_IV");
    }

}
