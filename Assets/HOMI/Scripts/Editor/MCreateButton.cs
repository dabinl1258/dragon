using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;

public class MCreateButton : Editor
{
    ////////////////////////////////////////////////////////////////
    // Create NGUI Label Animation Button
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Button/NGUI Animation Label Button", validate = true, priority = 2)]
    static public bool GetNGUIAnimationLabelButtonValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Button/NGUI Animation Label Button", validate = false, priority = 2)]
    static public void CreateNGUIAnimationLabelButton()
    {
        bool bChk = true;
        int nCount = 0;
        GameObject obj_Offset = null;
        GameObject obj_Parent = null;
        GameObject obj_Child = null;

        while (bChk)
        {
            if (GameObject.Find("CC_Offset") == null)
                obj_Offset = MHOMITools.CreateChildObject("4_Center_Center", "CC_Offset");

            if (GameObject.Find("HOMI_NGUILabelButton" + nCount) == null)
            {
                obj_Parent = MHOMITools.CreateChildObject("CC_Offset", "HOMI_NGUILabelButton" + nCount.ToString(), "BoxCollider", "TweenScale", "UIButtonScale", "UIEventTrigger");
                
                bChk = false;
            }
            else
                nCount++;
        }
        obj_Child = MHOMITools.CreateChildObject(obj_Parent.name, "Label", "UILabel");
    }


    ////////////////////////////////////////////////////////////////
    // Create NGUI Animation Button
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Button/NGUI Animation Sprite Button", validate = true, priority = 2)]
    static public bool GetNGUIAnimationButtonValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Button/NGUI Animation Sprite Button", validate = false, priority = 2)]
    static public void CreateNGUIAnimationButton()
    {
        bool bChk = true;
        int nCount = 0;
        GameObject obj_Offset = null;
        GameObject obj_Parent = null;
        GameObject obj_Child = null;

        while(bChk)
        {
            if (GameObject.Find("CC_Offset") == null)
                obj_Offset = MHOMITools.CreateChildObject("4_Center_Center", "CC_Offset");

            if(GameObject.Find("HOMI_NGUISpriteButton" + nCount) == null)
            {
                obj_Parent = MHOMITools.CreateChildObject("CC_Offset", "HOMI_NGUISpriteButton" + nCount.ToString(), "BoxCollider", "TweenScale", "UIButtonScale", "UIEventTrigger");
                bChk = false;
            }
            else
                nCount++;
        }

        obj_Child = MHOMITools.CreateChildObject(obj_Parent.name, "Sprite", "UISprite");
    }

    ////////////////////////////////////////////////////////////////
    // Create NGUI Label Animation Button
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Button/NGUI Animation Sprite Label Button", validate = true, priority = 2)]
    static public bool GetNGUILabelAnimationButtonValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Button/NGUI Animation Sprite Label Button", validate = false, priority = 2)]
    static public void CreateNGUILabelAnimationButton()
    {
        bool bChk = true;
        int nCount = 0;
        GameObject obj_Offset = null;
        GameObject obj_Parent = null;
        GameObject obj_Child1 = null;
        GameObject obj_Child2 = null;

        while (bChk)
        {
            if (GameObject.Find("CC_Offset") == null)
                obj_Offset = MHOMITools.CreateChildObject("4_Center_Center", "CC_Offset");

            if (GameObject.Find("HOMI_NGUISpriteLabelButton" + nCount) == null)
            {
                obj_Parent = MHOMITools.CreateChildObject("CC_Offset", "HOMI_NGUISpriteLabelButton" + nCount.ToString(), "BoxCollider", "TweenScale", "UIButtonScale", "UIEventTrigger");
                bChk = false;
            }
            else
                nCount++;
        }

        obj_Child1 = MHOMITools.CreateChildObject(obj_Parent.name, "Sprite", "UISprite");
        obj_Child2 = MHOMITools.CreateChildObject(obj_Parent.name, "Label", "UILabel");
    }

    ////////////////////////////////////////////////////////////////
    // Create Animation Button
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Button/Animation Sprite Button", validate = true, priority = 2)]
    static public bool GetAnimationButtonValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Button/Animation Sprite Button", validate = false, priority = 2)]
    static public void CreateAnimationButton()
    {
        bool bChk = true;
        int nCount = 0;
        GameObject obj_Offset = null;
        GameObject obj_Parent = null;
        GameObject obj_Child = null;

        while (bChk)
        {
            if (GameObject.Find("CC_Offset") == null)
                obj_Offset = MHOMITools.CreateChildObject("4_Center_Center", "CC_Offset");

            if (GameObject.Find("HOMI_SpriteButton" + nCount) == null)
            {
                obj_Parent = MHOMITools.CreateChildObject("CC_Offset", "HOMI_SpriteButton" + nCount.ToString(), "BoxCollider", "Animation", "UIEventTrigger");
                bChk = false;
            }
            else
                nCount++;
        }

        obj_Child = MHOMITools.CreateChildObject(obj_Parent.name, "Sprite", "UISprite");
    }


    ////////////////////////////////////////////////////////////////
    // Create Label Animation Button
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Button/Animation Sprite Label Button", validate = true, priority = 2)]
    static public bool GetLabelAnimationButtonValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Button/Animation Sprite Label Button", validate = false, priority = 2)]
    static public void CreateLabelAnimationButton()
    {
        bool bChk = true;
        int nCount = 0;
        GameObject obj_Offset = null;
        GameObject obj_Parent = null;
        GameObject obj_Child = null;

        while (bChk)
        {
            if (GameObject.Find("CC_Offset") == null)
                obj_Offset = MHOMITools.CreateChildObject("4_Center_Center", "CC_Offset");

            if (GameObject.Find("HOMI_SpriteLabelButton" + nCount) == null)
            {
                obj_Parent = MHOMITools.CreateChildObject("CC_Offset", "HOMI_SpriteLabelButton" + nCount.ToString(), "BoxCollider", "Animation", "UIEventTrigger");
                bChk = false;
            }
            else
                nCount++;
        }

        obj_Child = MHOMITools.CreateChildObject(obj_Parent.name, "Sprite", "UISprite");
        obj_Child = MHOMITools.CreateChildObject(obj_Parent.name, "Label", "UILabel");
    }


    ////////////////////////////////////////////////////////////////
    // Create Label Button
    ////////////////////////////////////////////////////////////////

    [MenuItem("HOMI/Create Button/Animation Label Button", validate = true, priority = 2)]
    static public bool GetAnimationLabelButtonValidator()
    {
        bool bChk = (GameObject.Find("Root_I") != null);
        if (bChk)
            return true;
        return false;
    }

    [MenuItem("HOMI/Create Button/Animation Label Button", validate = false, priority = 2)]
    static public void CreateAnimationLabelButton()
    {
        bool bChk = true;
        int nCount = 0;
        GameObject obj_Offset = null;
        GameObject obj_Parent = null;
        GameObject obj_Child = null;

        while (bChk)
        {
            if (GameObject.Find("CC_Offset") == null)
                obj_Offset = MHOMITools.CreateChildObject("4_Center_Center", "CC_Offset");

            if (GameObject.Find("HOMI_LabelButton" + nCount) == null)
            {
                obj_Parent = MHOMITools.CreateChildObject("CC_Offset", "HOMI_LabelButton" + nCount.ToString(), "BoxCollider", "Animation", "UIEventTrigger");
                bChk = false;
            }
            else
                nCount++;
        }

        obj_Child = MHOMITools.CreateChildObject(obj_Parent.name, "Label", "UILabel");
    }
}
