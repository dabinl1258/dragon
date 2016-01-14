using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Xml;
using MHomiLibrary;


/// <summary>
/// ��ġ :  0_Mngs
/// ���� UI���� �� ����ġ ���� ���� ���� �� ���� �޴����Դϴ�.
/// UI��ư Ŭ�� ���¹� �� ���� ��ġ�� �������������� ������ �δ� ���Դϴ�.
/// </summary>
public class HEtcMng : HSingleton<HEtcMng>
{
    protected HEtcMng() { }

    /// <summary>
    /// �񵿱�� �ε�
    /// </summary>
    AsyncOperation async = null;


    /// <summary>   
    /// Ŀ�´�Ƽ �˾�â���� ���
    /// ���� �������� ģ������߿� ����� ���ȴ��� �����ص�(���̵�)
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
    /// ���ѱ�� �Լ��Դϴ�.
    /// �ε��� �ڵ� ������ݴϴ�.
    /// </summary>
    /// <param name="sLevelName"></param>
    public void LoadLevelAsync(string sLevelName)
    {
        ResourcePrefabGame = HPrefabMng.I.CreatePrefab("PopupOffset", E_H_RESOURCELOAD.E_0_Common, "HLoadingPrefab");

        StartCoroutine(LoadingSpriteAnimation(sLevelName));
    }

    /// <summary>
    /// �ִϸ��̼��� ��� �Ǹ鼭 ���� ���� �����͵��� �ε��մϴ�.
    /// </summary>
    /// <param name="sName"> �� </param>
    /// <returns></returns>
    private IEnumerator LoadingSpriteAnimation(string sName)
    {
        HLoadingSpriteCtrl SpriteCtrl = ResourcePrefabGame.GetComponent<HLoadingSpriteCtrl>();

        async = Application.LoadLevelAsync(sName);

        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();

            //! �ε� �ִϸ��̼��� Update�մϴ�.
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
    /// �ε��� ������ִ� �Լ� �Դϴ�. (�� �Ѿ �� ���)
    /// </summary>
    /// <param name="sLevelName"> �� �̸� </param>
    /// <param name="ABundleList"> ���� ������ ������� �̸��� �����ϴ�. </param>
    public void LoadLevelAsync(string sLevelName, params string[] ABundleList)
    {
        ResourcePrefabGame = HPrefabMng.I.CreatePrefab("PopupOffset", E_H_RESOURCELOAD.E_0_Common, "HLoadingPrefab");

        StartCoroutine(LoadingSpriteAnimation(sLevelName, ABundleList));
    }

    /// <summary>
    /// �����͸� �ε��ؼ� �ε��� �ϴ� �Լ��Դϴ�.
    /// WWW�� ������ �ٿ�ε� �޴� �����Ͱ� �Ϸ�Ǹ� ���� ������ �Ѿ�ϴ�.
    /// </summary>
    /// <param name="sName"> �� </param>
    /// <param name="ABundleList"> ���� ���鿡�� ��������� �̸��� �����ɴϴ�. </param>
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

            //! �ε� �ִϸ��̼��� Update�մϴ�.
            if (SpriteCtrl != null)
                SpriteCtrl.UpdateFrame();
        }

        SpriteCtrl = null;
        Destroy(ResourcePrefabGame);
    }
}
