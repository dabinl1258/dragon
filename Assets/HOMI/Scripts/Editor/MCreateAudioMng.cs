using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MCreateAudioMng : Editor
{
    ////////////////////////////////////////////////////////////////
    // Create AudioMng
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Mng/Create AudioMng", validate = true, priority = 1)]
    static public bool GetCreateAudioMngValidator()
    {
        bool bChk = (GameObject.Find("0_Mngs") != null) && (GameObject.Find("AudioMng") == null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Mng/Create AudioMng", validate = false, priority = 1)]
    static public void CreateAudioMng()
    {
        MHOMITools.AddComponent("0_Mngs", "MAudioPlayMng");
        MHOMITools.CreateChildObject("0_Mngs", "AudioMng", "AudioListener");

        MAudioPlayMng obj = GameObject.Find("0_Mngs").GetComponent<MAudioPlayMng>();
        obj.AudioManager = GameObject.Find("AudioMng");
    }

}
