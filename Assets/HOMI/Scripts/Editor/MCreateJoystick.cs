using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MCreateJoystick : Editor
{
    ////////////////////////////////////////////////////////////////
    // Create Joystick
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Other/Create Joystick", validate = true, priority = 3)]
    static public bool GetCreateJoystickValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Other/Create Joystick", validate = false, priority = 3)]
    static public void CreateJoystick()
    {
        GameObject obj = MHOMITools.CreateChildObject("CC_Offset" ,"Joystick", "MJoystickControl", "MJoystickSpriteControl", "MJoystickEffectTech", "MJoystickMng");
        GameObject back = MHOMITools.CreateChildObject("Joystick", "JoystickBack", "UISprite");
        GameObject bar = MHOMITools.CreateChildObject("Joystick", "JoystickBar", "UISprite");

        obj.GetComponent<MJoystickControl>().JoystickTarget_Cam = obj.transform.parent.parent.parent.GetComponent<Camera>();
        obj.GetComponent<MJoystickSpriteControl>().BackGround_Sprite = back.GetComponent<UISprite>();
        obj.GetComponent<MJoystickSpriteControl>().Object_Sprite = bar.GetComponent<UISprite>();
        obj.GetComponent<MJoystickEffectTech>().JoystickCtrl_Script = obj.GetComponent<MJoystickControl>();
        obj.GetComponent<MJoystickEffectTech>().JoystickSpriteCtrl_Script = obj.GetComponent<MJoystickSpriteControl>();

        obj.GetComponent<MJoystickMng>().JoystickSpriteCtrl_Script = obj.GetComponent<MJoystickSpriteControl>();
        obj.GetComponent<MJoystickMng>().JoystickCtrl_Script = obj.GetComponent<MJoystickControl>();
        obj.GetComponent<MJoystickMng>().JoystickEffect_Script = obj.GetComponent<MJoystickEffectTech>();
    }
}
