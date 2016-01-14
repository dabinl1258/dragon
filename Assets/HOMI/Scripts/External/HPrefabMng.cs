using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using MHomiLibrary;

#region HPrefabData
public enum E_M_PREFAB_TYPE
{
    E_NORMAL,
    E_OVERLAP,
    E_POPUP,
    E_MAX
}

public class MPrefabData
{
    public MPrefabData(E_H_RESOURCELOAD eResource, string sPrefab, Vector3 vPosVec = default(Vector3), Vector3 vScaleVec = default(Vector3), string sAddCom = "", string sTitle = "")
    {
        eResourcesLoadPos = eResource;
        sPrefabName = sPrefab;
        vPosVec3 = vPosVec;
        vScaleVec3 = vScaleVec;
        sTitleName = sTitle;
        sAddComponents = sAddCom;

        fZPos = 0.0f;
        parentGame = null;
        parentString = "";
    }

    public MPrefabData(E_H_RESOURCELOAD eResource, string sPrefab, float fZ = 0.0f, string sAddCom = "", string sTitle = "")
    {
        eResourcesLoadPos = eResource;
        sPrefabName = sPrefab;
        sTitleName = sTitle;
        sAddComponents = sAddCom;
        fZPos = fZ;

        vPosVec3 = default(Vector3);
        vScaleVec3 = default(Vector3);
        parentGame = null;
        parentString = "";
    }

    public MPrefabData(E_H_RESOURCELOAD eResource, string sPrefab, GameObject parentgame, Vector3 vPosVec = default(Vector3), Vector3 vScaleVec = default(Vector3), string sAddCom = "", string sTitle = "")
    {
        eResourcesLoadPos = eResource;
        sPrefabName = sPrefab;
        parentGame = parentgame;
        vPosVec3 = vPosVec;
        vScaleVec3 = vScaleVec;
        sTitleName = sTitle;
        sAddComponents = sAddCom;

        fZPos = 0.0f;
        parentString = "";
    }

    public MPrefabData(E_H_RESOURCELOAD eResource, string sPrefab, GameObject parentgame, float fZ = 0.0f, string sAddCom = "", string sTitle = "")
    {
        eResourcesLoadPos = eResource;
        sPrefabName = sPrefab;
        parentGame = parentgame;
        sTitleName = sTitle;
        sAddComponents = sAddCom;
        fZPos = fZ;

        vPosVec3 = default(Vector3);
        vScaleVec3 = default(Vector3);
        parentString = "";
    }

    public MPrefabData(E_H_RESOURCELOAD eResource, string sPrefab, string parentstr, Vector3 vPosVec = default(Vector3), Vector3 vScaleVec = default(Vector3), string sAddCom = "", string sTitle = "")
    {
        eResourcesLoadPos = eResource;
        sPrefabName = sPrefab;
        parentString = parentstr;
        vPosVec3 = vPosVec;
        vScaleVec3 = vScaleVec;
        sTitleName = sTitle;
        sAddComponents = sAddCom;

        fZPos = 0.0f;
        parentGame = null;
    }

    public MPrefabData(E_H_RESOURCELOAD eResource, string sPrefab, string parentstr, float fZ = 0.0f, string sAddCom = "", string sTitle = "")
    {
        eResourcesLoadPos = eResource;
        sPrefabName = sPrefab;
        parentString = parentstr;
        sTitleName = sTitle;
        sAddComponents = sAddCom;
        fZPos = fZ;

        vPosVec3 = default(Vector3);
        vScaleVec3 = default(Vector3);
        parentGame = null;
    }

    public GameObject parentGame
    {
        get;
        set;
    }

    public string parentString
    {
        get;
        set;
    }

    public E_H_RESOURCELOAD eResourcesLoadPos
    {
        get;
        set;
    }

    public string sPrefabName
    {
        get;
        set;
    }

    public string sTitleName
    {
        get;
        set;
    }

    public string sAddComponents
    {
        get;
        set;
    }

    public float fZPos
    {
        get;
        set;
    }

    public Vector3 vPosVec3
    {
        get;
        set;
    }

    public Vector3 vScaleVec3
    {
        get;
        set;
    }
}

[Serializable]
public class MResourceList
{
    public MResourceList(string data)
    {
        name = data;
        resourceList = new List<GameObject>();
    }

    [SerializeField]
    public string name;
    [SerializeField]
    public List<GameObject> resourceList;
}
#endregion

#region HPrefabMng.cs
/// <summary>
/// 위치 :  0_Mngs
/// 프리팹 생성 삭제 관련 메니져입니다
/// </summary>
public class HPrefabMng : HSingleton<HPrefabMng>
{
    protected HPrefabMng() { }

    #region Variables

    /// <summary>
    /// 공통으로 쓰여지는 리소스 리스트입니다(경고창 같은 종류들)
    /// </summary>
    [SerializeField]
    public List<MResourceList> ResourceList = null;

    /// <summary>
    /// 만들어진 프리펩 GameObject 임시 저장 장소
    /// </summary>
    [SerializeField]
    public Dictionary<string, GameObject> CreatePrefabsList = null;

    /// <summary>
    /// 팝업 뎁스
    /// </summary>
    int nPopupDepth = 0;

    /// <summary>
    /// 팝업 옵셋 자동 찾는 기능에 필요한 자식 검출 Transform
    /// </summary>
    Transform PopupOffset_Transform = null;

    #endregion

    #region virtual function

    void Awake()
    {
        PopupOffset_Transform = GameObject.Find("PopupOffset").transform;

        CreatePrefabsList = new Dictionary<string, GameObject>();
        ResourceList = new List<MResourceList>();

        for(int i = 0 ; i < (int)E_H_RESOURCELOAD.E_MAX; i++)
            ResourceList.Add( new MResourceList(GetSceneName(  ((E_H_RESOURCELOAD)i).ToString() ).ToString()) );

        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/HPrefabMng.cs");

        CreatePrefabsList.Clear();
        CreatePrefabsList = null;

        ResourceList.Clear();
        ResourceList = null;
    }

    #endregion

    #region CreatePrefab

    public GameObject CreatePrefab(E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 vPos, Vector3 vScale, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_NORMAL, eResourceLoadPos, -1.0f, vPos, vScale, null, "", sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePrefab(GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_NORMAL, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), ParentGame, "", sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePrefab(string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_NORMAL, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), null, sParentName, sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePrefab(string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_NORMAL, eResourceLoadPos, -1.0f, PosVec, ScaleVec, null, sParentName, sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePrefab(GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_NORMAL, eResourceLoadPos, -1.0f, PosVec, ScaleVec, ParentGame, "", sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreateOverlapPrefab(string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_OVERLAP, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), null, sParentName, sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreateOverlapPrefab(string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_OVERLAP, eResourceLoadPos, -1.0f, PosVec, ScaleVec, null, sParentName, sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreateOverlapPrefab(GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_OVERLAP, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), ParentGame, "", sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreateOverlapPrefab(GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_OVERLAP, eResourceLoadPos, -1.0f, PosVec, ScaleVec, ParentGame, "", sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePopupPrefab(string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), null, sParentName, sPrefabName, sTitleName, sAddComponent);
    }
    public GameObject CreatePopupPrefab(int depth, string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), null, sParentName, sPrefabName, sTitleName, sAddComponent, depth);
    }

    public GameObject CreatePopupPrefab(string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, -1.0f, PosVec, ScaleVec, null, sParentName, sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePopupPrefab(int depth, string sParentName, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, -1.0f, PosVec, ScaleVec, null, sParentName, sPrefabName, sTitleName, sAddComponent, depth);
    }

    public GameObject CreatePopupPrefab(GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), ParentGame, "", sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePopupPrefab(int depth, GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZPos = -1.0f, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, fZPos, default(Vector3), default(Vector3), ParentGame, "", sPrefabName, sTitleName, sAddComponent, depth);
    }

    public GameObject CreatePopupPrefab(GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, -1.0f, PosVec, ScaleVec, ParentGame, "", sPrefabName, sTitleName, sAddComponent);
    }

    public GameObject CreatePopupPrefab(int depth, GameObject ParentGame, E_H_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 PosVec, Vector3 ScaleVec, string sTitleName = "", string sAddComponent = "")
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, eResourceLoadPos, -1.0f, PosVec, ScaleVec, ParentGame, "", sPrefabName, sTitleName, sAddComponent, depth);
    }

    public GameObject CreatePrefab(MPrefabData data)
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_NORMAL, data.eResourcesLoadPos, data.fZPos, data.vPosVec3, data.vScaleVec3, data.parentGame, data.parentString, data.sPrefabName, data.sTitleName, data.sAddComponents);
    }

    public GameObject CreateOverlapPrefab(MPrefabData data)
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_OVERLAP, data.eResourcesLoadPos, data.fZPos, data.vPosVec3, data.vScaleVec3, data.parentGame, data.parentString, data.sPrefabName, data.sTitleName, data.sAddComponents);
    }

    public GameObject CreatePopupPrefab(MPrefabData data)
    {
        return CreatePrefab(E_M_PREFAB_TYPE.E_POPUP, data.eResourcesLoadPos, data.fZPos, data.vPosVec3, data.vScaleVec3, data.parentGame, data.parentString, data.sPrefabName, data.sTitleName, data.sAddComponents);
    }

    GameObject CreatePrefab(E_M_PREFAB_TYPE eType, E_H_RESOURCELOAD eResourceLoadPos, float fZPos = -1.0f, Vector3 PosVec = default(Vector3), Vector3 ScaleVec = default(Vector3), GameObject ParentGame = null, string sParentName = "", string sPrefabName = "", string sTitleName = "", string sAddComponent = "", int nDepth = 1)
    {
        GameObject CreatePrefabGame = null;
        GameObject tempParentGame = null;

        if (MAssetBundleMng.I.bAssetBundleExists)
            sPrefabName = GetOnlyPrefabName(sPrefabName);

        if (sParentName != "")
            tempParentGame = GameObject.Find(sParentName);

        if (ParentGame != null)
            tempParentGame = ParentGame;

        // 중복되는 오브젝트에 대한 처리
        if (bOverLapGameObject(sPrefabName, sTitleName) && (eType != E_M_PREFAB_TYPE.E_OVERLAP))
        {
            Debug.Log("sTitleName is " + sTitleName + "/sPrefabName is " + sPrefabName + "/OverLapGameObject return null [CreatePrefab/HPrefabMng.cs]");
            return null;
        }

        List<GameObject> Resource = GetResourceList(sPrefabName, eResourceLoadPos);

        if (Resource != null)
        {
            var enumerator = Resource.GetEnumerator();
            while (enumerator.MoveNext())
            {
                GameObject obj = (GameObject)enumerator.Current;

                if (obj.transform.name == sPrefabName)
                {
                    CreatePrefabGame = Instantiate(obj) as GameObject;

                    if (sAddComponent != "")
                        CreatePrefabGame.AddComponent(sAddComponent);
                        
                    // 타이틀 이름 정했을 경우
                    if (sTitleName != "")
                    {
                        StringBuilder sb = new StringBuilder();
                        if (eType == E_M_PREFAB_TYPE.E_OVERLAP)
                            sb.Append(sTitleName).Append(CreatePrefabsList.Count.ToString()).Append("(Clone)");
                        else
                            sb.Append(sTitleName).Append("(Clone)");

                        CreatePrefabGame.transform.name = sb.ToString();
                    }
                    else
                    {
                        // 타이틀 이름을 정하지 않고 프리펩 이름을 정했을 경우
                        if (sPrefabName != "")
                        {
                            StringBuilder sb = new StringBuilder();
                            if (eType == E_M_PREFAB_TYPE.E_OVERLAP)
                                sb.Append(sPrefabName).Append(CreatePrefabsList.Count.ToString()).Append("(Clone)");
                            else
                                sb.Append(sPrefabName).Append("(Clone)");
                            CreatePrefabGame.transform.name = sb.ToString();
                        }
                    }

                    if (tempParentGame != null)
                        CreatePrefabGame.transform.parent = tempParentGame.transform;

                    if (!PosVec.Equals(default(Vector3)))
                        CreatePrefabGame.transform.localPosition = PosVec;
                    else
                        CreatePrefabGame.transform.localPosition = new Vector3(0, 0, fZPos);

                    if (!ScaleVec.Equals(default(Vector3)))
                        CreatePrefabGame.transform.localScale = ScaleVec;
                    else
                        CreatePrefabGame.transform.localScale = Vector3.one;

                    // 팝업창에 대해서 현재 PopupOffset의 밑에 있는 오브젝트 수의 + 1 만큼 Depth를 지정
                    if (eType == E_M_PREFAB_TYPE.E_POPUP)
                        CreatePrefabGame.GetComponent<UIPanel>().depth = PopupOffset_Transform.GetChildCount() + nDepth;

                    // 리스트에 등록
                    CreatePrefabsList.Add(CreatePrefabGame.transform.name, CreatePrefabGame);

                    return CreatePrefabGame;
                }
            }

            //Debug.Log(" No Find PrefabName ->" + sPrefabName + " [CreatePrefab/HPrefabMng.cs]");
        }
        else
            Debug.Log(" NULL Resource [CreatePrefab/HPrefabMng.cs]");

        return null;
    }

    #endregion

    #region Get Data

    public string GetSceneName(string sName)
    {
        if (sName != string.Empty)
        {
            int nStartNum = sName.IndexOf("_");

            string sTemp = sName.Substring(nStartNum + 1);

            return sTemp;
        }
        return null;
    }

    /// <summary>
    /// 리소스 로드 합니다^^
    /// </summary>
    /// <param name="sPrefabName"></param>
    /// <param name="eResourceLoadPos"></param>
    /// <returns></returns>
    List<GameObject> GetResourceList(string sPrefabName, E_H_RESOURCELOAD eResourceLoadPos)
    {
        List<GameObject> Resource = null;

        string sSceneName = GetSceneName(eResourceLoadPos.ToString());

        foreach (MResourceList list in ResourceList)
        {
            if (list.name.Equals(sSceneName))
            {
                Resource = list.resourceList;
            }
        }

        ResourceLoad(sSceneName, sPrefabName, Resource);
        return Resource;
    }

    public Vector3 GetPopupScale()
    {
        if (AspectRatios.GetAspectRatio() == AspectRatio.Aspect4by3)
            return new Vector3(0.8f, 0.8f, 0.8f);
        else if (AspectRatios.GetAspectRatio() == AspectRatio.Aspect3by2)
            return new Vector3(0.95f, 0.95f, 0.95f);
        else
            return Vector3.one;
    }

    /// <summary>
    /// Resource/1_prefab/hwang   -> hwang만 알아온다.
    /// </summary>
    /// <param name="sName"></param>
    /// <returns></returns>
    public string GetOnlyPrefabName(string sName)
    {
        if (sName != string.Empty)
        {
            int nStartNum = sName.LastIndexOf("/");

            if (nStartNum == -1)
                return sName;

            string sTemp = sName.Substring(nStartNum + 1);

            return sTemp;
        }

        return string.Empty;
    }
    #endregion

    #region ETC

    /// <summary>
    /// 이미 같은 이름의 프리펩이 만들어져있는지 검사합니다.
    /// </summary>
    /// <param name="sPrefabName"></param>
    /// <param name="sTitleName"></param>
    /// <returns></returns>
    bool bOverLapGameObject(string sPrefabName, string sTitleName)
    {
        string sTempStr = null;

        StringBuilder sb = new StringBuilder();
        if (sTitleName == "")
            sb.Append(sPrefabName);
        else
            sb.Append(sTitleName);

        sb.Append("(Clone)");
        sTempStr = sb.ToString();

        bool TempGameFlag = false;


        if (CreatePrefabsList == null)
            return false;

        if (CreatePrefabsList.Count == 0)
            return false;

        TempGameFlag = CreatePrefabsList.ContainsKey(sTempStr);

        if (TempGameFlag)
            return true;
        else
            return false;

    }

    /// <summary>
    /// 리소스 리스트를 토대로 리소스를 가져옵니다.
    /// </summary>
    /// <param name="sCurSceneName"> 씬의 이름입니다. </param>
    /// <param name="sPrefabName"> 프리펩의 이름입니다. </param>
    /// <param name="Resource"> 리소스 리스트의 이름입니다. </param>
    void ResourceLoad(string sCurSceneName, string sPrefabName, List<GameObject> Resource)
    {
        int nCnt = 0;

        var enumerator = Resource.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject obj = enumerator.Current;

            if (obj.transform.name == sPrefabName)
                nCnt++;
        }

        if (nCnt == 0)
        {
            GameObject ResourcePrefabGame = null;
            StringBuilder sb = new StringBuilder();
            string sPath = string.Empty;

            if (MAssetBundleMng.I.bAssetBundleExists)
            {
                if (MAssetBundleMng.I.GetWWWInAssetBundle(sCurSceneName) != null)
                {
                    WWW curWWW = MAssetBundleMng.I.GetWWWInAssetBundle(sCurSceneName);

                    if (curWWW.assetBundle == true)
                    {
                        UnityEngine.Object[] LoadPrefab = curWWW.assetBundle.LoadAll(typeof(GameObject));

                        foreach (UnityEngine.Object LoadObj in LoadPrefab)
                            Resource.Add((GameObject)LoadObj);
                    }
                    Debug.Log("-------------_>" + Resource.Count);
                    MAssetBundleMng.I.DestroyWWWAssetBundle(sCurSceneName);

                    return;
                }
                else
                {
                    sb.Append(sCurSceneName).Append("/1_Prefabs/").Append(sPrefabName);
                    sPath = sb.ToString();
                    ResourcePrefabGame = (GameObject)Resources.Load(sPath, typeof(GameObject));
                }
            }
            else
            {
                sb.Append(sCurSceneName).Append("/1_Prefabs/").Append(sPrefabName);
                sPath = sb.ToString();
                ResourcePrefabGame = (GameObject)Resources.Load(sPath, typeof(GameObject));
            }

            // 공통적인 구문

            if (ResourcePrefabGame == null)
            {
                //Debug.Log("ResourcePrefabGame is NULL " + sPath + " [ResourceLoad(..)/HPrefabMng.cs]");
                //오규석
                return;
            }

            ResourcePrefabGame.name = sPrefabName;

            if (ResourcePrefabGame != null)
                Resource.Add(ResourcePrefabGame);
            else
                Debug.Log(sPath + "<---  failed Resource.load [ResourceLoad/HPrefabMng.cs]");
        }
    }

    #endregion

    #region Destroy

    /// <summary>
    /// 프리펩을 이름으로 탐색해서 지워줍니다.
    /// </summary>
    /// <param name="Name"> 프리펩의 이름 </param>
    public void DestroyPrefab(string Name)
    {
        if (CreatePrefabsList == null)
            return;

        if (CreatePrefabsList.Count == 0)
        {
            Debug.Log("CreatePrefabList is Zero [DestroyPrefab/HPrefabMng.cs]");
            return;
        }

        if (CreatePrefabsList.ContainsKey(Name))
        {
            Destroy(CreatePrefabsList[Name]);
            CreatePrefabsList[Name] = null;
            CreatePrefabsList.Remove(Name);
        }
    }

    /// <summary>
    /// 프리펩을 게임 오브젝트형으로 삭제해줍니다.
    /// </summary>
    /// <param name="PrefabGame"> 프리펩의 게임 오브젝트 형 </param>
    public void DestroyPrefab(GameObject PrefabGame)
    {
        if (PrefabGame == null)
            return;

        string sDeleteName = PrefabGame.name;

        if (CreatePrefabsList == null)
            return;

        if (CreatePrefabsList.Count == 0)
        {
            Debug.Log("CreatePrefabList is Zero [DestroyPrefab/HPrefabMng.cs]");
            return;
        }

        if (CreatePrefabsList.ContainsKey(sDeleteName))
        {
            Destroy(CreatePrefabsList[sDeleteName]);
            CreatePrefabsList[sDeleteName] = null;
            CreatePrefabsList.Remove(sDeleteName);
        }
    }

    /// <summary>
    /// 프리펩 삭제하는 함수, 인자가 있어서 인자에 씬 이름을 넣으면 그 씬만 청소해줍니다.
    /// </summary>
    /// <param name="sDestroyResourceSceneName"> 리소스를 초기화 할 씬 </param>
    /// <param name="sDestroyResourceSceneParams"> 리소스를 초기화 할 다른 씬들 </param>
    public void DestroyPrefabs(E_H_RESOURCELOAD eType, params E_H_RESOURCELOAD[] eTypeParams)
    {
        if (CreatePrefabsList == null)
            return;

        if (CreatePrefabsList.Count == 0)
        {
            Debug.Log("CreatePrefabsList.Count == 0   [DestroyPrefab/HPrefabMng.cs]");
            return;
        }

        var enumerator = CreatePrefabsList.Values.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject obj = (GameObject)enumerator.Current;

            if (obj != null)
            {
                Destroy(obj);
                continue;
            }
        }

        string sResourceScene = GetSceneName( eType.ToString() );
        string[] sResourcesScene = new string[eTypeParams.Length];

        for (int i = 0; i < ResourceList.Count; i++)
        {
            if (ResourceList[i].name.Equals(sResourceScene))
            {
                for (int j = 0; j < ResourceList[i].resourceList.Count; j++)
                    ResourceList[i].resourceList.Remove(ResourceList[i].resourceList[j]);

                ResourceList[i].resourceList.Clear();
            }

            for (int j = 0; j < sResourcesScene.Length; j++)
            {
                if (ResourceList[i].name.Equals(sResourcesScene[j]))
                {
                    for (int k = 0; k < ResourceList[i].resourceList.Count; k++)
                        ResourceList[i].resourceList.Remove(ResourceList[i].resourceList[k]);

                    ResourceList[i].resourceList.Clear();
                }
            }
        }

        CreatePrefabsList.Clear();

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 모든 씬을 다 삭제해줍니다.
    /// </summary>
    public void DestroyPrefabs()
    {
        if (CreatePrefabsList == null)
            return;

        if (CreatePrefabsList.Count == 0)
        {
            Debug.Log("CreatePrefabsList.Count == 0   [DestroyPrefab/HPrefabMng.cs]");
            return;
        }

        var enumerator = CreatePrefabsList.Values.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject obj = (GameObject)enumerator.Current;

            if (obj != null)
            {
                Destroy(obj);
                continue;
            }
        }

        for(int i = 0 ; i < ResourceList.Count; i++)
        {
            for(int j = 0 ; j < ResourceList[i].resourceList.Count; j++)
            {
                ResourceList[i].resourceList.Remove( ResourceList[i].resourceList[j] );
            }

            ResourceList[i].resourceList.Clear();
        }

        CreatePrefabsList.Clear();

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    #endregion
}
#endregion