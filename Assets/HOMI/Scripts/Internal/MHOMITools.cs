using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// HOMI 라이브러리 툴을 제작하는데 쓰이는 함수들을 모은 클래스 입니다.
/// 정적 클래스라 암대서나 . 찍고 가져오시면 됩니다.
/// </summary>
static public class MHOMITools
{
    /// <summary>
    /// 리소스에 있는 오브젝트를 생성하는 함수
    /// </summary>
    /// <param name="strTargetName"> 타겟의 이름을 가져옵니다. (리소스의 이름) </param>
    static public GameObject CreateResourceObject(string strTargetName)
    {
        GameObject obj = CreateResourceObject(strTargetName, "");

        return obj;
    }

    /// <summary>
    /// 리소스에 있는 오브젝트를 생성하는 함수
    /// </summary>
    /// <param name="strTargetName"> 타겟의 이름을 가져옵니다. (리소스의 이름) </param>
    /// <param name="strChangeName"> 리소스의 이름을 무엇으로 바꾸는지 설정합니다. </param>
    /// <param name="strMessages"> 컴포넌트 추가할 리스트를 적습니다. (가변인자) </param>
    static public GameObject CreateResourceObject(string strTargetName, string strChangeName, params string[] strMessages)
    {
        if (GameObject.Find(strChangeName) || GameObject.Find(strTargetName))
            return null;

        GameObject obj = Resources.Load(strTargetName) as GameObject;
        GameObject game = GameObject.Instantiate(obj) as GameObject;

        ChangeName(ref game, strChangeName);

        AddComponent(strChangeName, strMessages);

        game.transform.localPosition = new Vector3(0, 0, 0);
        game.transform.localScale = new Vector3(1, 1, 1);

        return game;
    }
    
    /// <summary>
    /// 오브젝트를 만듭니다.
    /// </summary>
    /// <param name="strCreateName"> 만들어질 오브젝트의 이름 </param>
    /// <param name="strMessages"> 추가할 컴포넌트 리스트를 적습니다. (가변인자) </param>
    static public GameObject CreateObject(string strCreateName, params string[] strMessages)
    {
        if (GameObject.Find(strCreateName))
            return null;

        GameObject game = new GameObject();

        ChangeName(ref game, strCreateName);

        AddComponent(strCreateName, strMessages);

        game.transform.localPosition = new Vector3(0, 0, 0);
        game.transform.localScale = new Vector3(1, 1, 1);

        return game;
    }

    /// <summary>
    /// 부모를 둔 오브젝트를 만듭니다.
    /// </summary>
    /// <param name="strParentObject"> 부모의 오브젝트 이름 </param>
    /// <param name="strCreateName"> 생성할 오브젝트의 이름 </param>
    /// <param name="strMessages"> 추가할 컴포넌트 리스트를 적습니다. (가변인자) </param>
    static public GameObject CreateChildObject(string strParentObject, string strCreateName, params string[] strMessages)
    {
        if (GameObject.Find(strParentObject).transform.FindChild(strCreateName) || !GameObject.Find(strParentObject))
            return null;

        GameObject game = new GameObject();

        game.transform.parent = GameObject.Find(strParentObject).transform;

        ChangeName(ref game, strCreateName);

        AddComponent(strCreateName, strMessages);

        game.transform.localPosition = new Vector3(0,0,0);
        game.transform.localScale = new Vector3(1,1,1);

        return game;
    }
    
    /// <summary>
    /// 컴포넌트들을 적습니다.
    /// </summary>
    /// <param name="strTargetName"> 컴포넌트를 추가할 타겟 </param>
    /// <param name="strMessages"> 추가할 컴포넌트 리스트를 적습니다. (가변인자) </param>
    static public void AddComponent(string strTargetName, params string[] strMessages)
    {
        GameObject obj = GameObject.Find(strTargetName);

        foreach(string msg in strMessages)
        {
            if(obj.GetComponent(msg) == null)
                obj.AddComponent(msg);
        }
    }

    /// <summary>
    /// 이름을 초기화 합니다.
    /// </summary>
    /// <param name="obj"> 초기화할 타겟 </param>
    static public void ResetName(ref GameObject obj)
    {
        ChangeName(ref obj, "");
    }

    /// <summary>
    /// 이름을 변경할 타겟
    /// </summary>
    /// <param name="obj"> 변경 할 타겟 </param>
    /// <param name="strTargetName"> 타겟 이름 </param>
    static public void ChangeName(ref GameObject obj, string strTargetName)
    {
        if(strTargetName == "")
            return;

        obj.name = strTargetName;
    }
}