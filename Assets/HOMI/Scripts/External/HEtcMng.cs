using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Xml;
using MHomiLibrary;


/// <summary>
/// 위치 :  0_Mngs
/// 각종 UI상태 및 씬위치 상태 관련 참조 및 저장 메니져입니다.
/// UI버튼 클릭 상태및 씬 현재 위치등 각각의정보들을 저장해 두는 곳입니다.
/// </summary>
public class HEtcMng : HSingleton<HEtcMng>
{
    protected HEtcMng() { }

    /// <summary>
    /// 비동기식 로딩
    /// </summary>
    AsyncOperation async = null;


    /// <summary>   
    /// 커뮤니티 팝업창에서 사용
    /// 현재 여러명의 친구목록중에 어떤놈을 눌렸는지 저장해둠(아이디)
    /// </summary>
    public string sCurrentSelFaceBookID = string.Empty;

    GameObject ResourcePrefabGame = null;

    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
       
    }

    void OnDestroy()
    {
        
    }

    /// <summary>
    /// 씬넘기는 함수입니다.
    /// 로딩바 자동 출력해줍니다.
    /// </summary>
    /// <param name="sLevelName"></param>
    public void LoadLevelAsync(string sLevelName)
    {
        ResourcePrefabGame = HPrefabMng.I.CreatePrefab("PopupOffset", E_H_RESOURCELOAD.E_0_Common, "HLoadingPrefab");

        StartCoroutine(LoadingSpriteAnimation(sLevelName));
    }

    /// <summary>
    /// 애니메이션이 재생 되면서 다음 씬의 데이터들을 로딩합니다.
    /// </summary>
    /// <param name="sName"> 씬 </param>
    /// <returns></returns>
    private IEnumerator LoadingSpriteAnimation(string sName)
    {
        HLoadingSpriteCtrl SpriteCtrl = ResourcePrefabGame.GetComponent<HLoadingSpriteCtrl>();

        async = Application.LoadLevelAsync(sName);

        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();

            //! 로딩 애니메이션을 Update합니다.
            if (SpriteCtrl != null)
                SpriteCtrl.UpdateFrame();
        }

        SpriteCtrl = null;
        Destroy(ResourcePrefabGame);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Asset Bundle Async Changed Scene

    /// <summary>
    /// 로딩바 출력해주는 함수 입니다. (씬 넘어갈 때 사용)
    /// </summary>
    /// <param name="sLevelName"> 씬 이름 </param>
    /// <param name="ABundleList"> 에셋 번들의 프리펩들 이름을 적습니다. </param>
    public void LoadLevelAsync(string sLevelName, params string[] ABundleList)
    {
        ResourcePrefabGame = HPrefabMng.I.CreatePrefab("PopupOffset", E_H_RESOURCELOAD.E_0_Common, "HLoadingPrefab");

        StartCoroutine(LoadingSpriteAnimation(sLevelName, ABundleList));
    }

    /// <summary>
    /// 데이터를 로드해서 로딩을 하는 함수입니다.
    /// WWW로 웹에서 다운로드 받는 데이터가 완료되면 다음 씬으로 넘어갑니다.
    /// </summary>
    /// <param name="sName"> 씬 </param>
    /// <param name="ABundleList"> 에셋 번들에서 프리펩들의 이름을 가져옵니다. </param>
    /// <returns></returns>
    private IEnumerator LoadingSpriteAnimation(string sName, params string[] ABundleList)
    {
        HLoadingSpriteCtrl SpriteCtrl = ResourcePrefabGame.GetComponent<HLoadingSpriteCtrl>();

        foreach (string sDownLoadName in ABundleList)
        {
            //sDownLoadName = AssetBundleInfoList[i].sKey;
            string sResourceSavePath = string.Empty;

#if UNITY_EDITOR
            sResourceSavePath = Application.dataPath + "/Resources/0_AssetBundles/" + sDownLoadName + ".unity3d";
#else
            sResourceSavePath = Application.persistentDataPath + "/" + sDownLoadName + ".unity3d";
#endif

            WWW downloadwww = new WWW("file:///" + sResourceSavePath);

            if (SpriteCtrl != null)
                SpriteCtrl.UpdateFrame();

            yield return downloadwww;

            if (SpriteCtrl != null)
                SpriteCtrl.UpdateFrame();

            if (downloadwww.isDone)
            {
                int nIndex = MAssetBundleMng.I.GetAssetBundleListIndex(sDownLoadName);

                if (nIndex == -1)
                    Debug.Log("Error AsssetBundle Index is -1");

                MAssetBundleMng.I.AssetBundleInfoList[nIndex].BundleWWW = downloadwww;

            }
        }

        async = Application.LoadLevelAsync(sName);

        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();

            //! 로딩 애니메이션을 Update합니다.
            if (SpriteCtrl != null)
                SpriteCtrl.UpdateFrame();
        }

        SpriteCtrl = null;
        Destroy(ResourcePrefabGame);
    }
}
