using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using MHomiLibrary;

#region AssetBundle Infomation Class

[Serializable]
public class HAssetBundleInfo
{
    public HAssetBundleInfo()
    {

    }

    public HAssetBundleInfo(string key)
    {
        sKey = key;
    }

    public string sKey;
    public WWW BundleWWW;
};

#endregion

public class MAssetBundleMng : HSingleton<MAssetBundleMng> 
{
    protected MAssetBundleMng() { }

    #region Variable Field

    /// <summary>
    /// AssetBundle에서 읽어올지 아닐지를 결정하는 변수
    /// </summary>
    public bool bUseAssetBundleInEditor = false;

    /// <summary>
    /// AssetBundle 주소
    /// </summary>
    public string sLoadPath = "http://104.155.236.242:26259/pandaria/update/";

    /// <summary>
    /// AssetBundle 이름들
    /// 서버에서 업데이트 받아야할 이름들을 받아옵니다.
    /// </summary>
    public List<HAssetBundleInfo> AssetBundleInfoList = null;
    //public Dictionary<string, WWW> AssetBundleInfoList = null;

    /// <summary>
    /// 다운로드
    /// </summary>
    WWW www;

    /// <summary>
    /// AssetBundle 로딩 개수
    /// </summary>
    public int nLoadCnt = 0;

    /// <summary>
    /// 에셋 번들 파일 유무 확인
    /// </summary>
    public bool bAssetBundleExists = false;

    #endregion

    #region Virtual Function Field

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void OnDestroy()
    {
        AssetBundleInfoList.Clear();
        AssetBundleInfoList = null;
    }

    #endregion

    #region ETC 구문

    /// <summary>
    /// 엣셋 다운로드^^
    /// </summary>
    /// <param name="Slider"></param>
    /// <returns></returns>
    public IEnumerator UpdateAssetBundle()
    {
        // 다운받아야할 목록
        for (int i = 0; i < AssetBundleInfoList.Count; i++)
        {
            string sDownLoadName = AssetBundleInfoList[i].sKey;
            string sResourceSavePath = string.Empty;

#if UNITY_EDITOR
            sResourceSavePath = Application.dataPath + "/Resources/0_AssetBundles/" + sDownLoadName + ".unity3d";
#else
            sResourceSavePath = Application.persistentDataPath + "/" + sDownLoadName + ".unity3d";
#endif

            string sUserPath = sLoadPath + sDownLoadName + ".unity3d";
            www = new WWW(sUserPath);
            yield return www;


            // 다운로드가 완료된 후 실행
            if (www.isDone)
            {
                FileStream fs = new FileStream(sResourceSavePath, FileMode.Create);
                BinaryWriter w = new BinaryWriter(fs);

                w.Write(www.bytes);

                w.Close();
                fs.Close();

                nLoadCnt++;
            }

            www.assetBundle.Unload(false);
            www.Dispose();
            www = null;
        }
        
        if (AssetBundleInfoList.Count == nLoadCnt)
            bAssetBundleExists = true;
    }

    /// <summary>
    /// 다운받아놓은 엣셋 로딩
    /// </summary>
    /// <returns> </returns>
    public IEnumerator LoadAssetBundleInFile()
    {
        // 다운받아야할 목록
        for (int i = 0; i < AssetBundleInfoList.Count; i++)
        {
            string sDownLoadName = AssetBundleInfoList[i].sKey;
            string sResourceSavePath = string.Empty;

#if UNITY_EDITOR
            sResourceSavePath = Application.dataPath + "/Resources/0_AssetBundles/" + sDownLoadName + ".unity3d";
#else
            sResourceSavePath = Application.persistentDataPath + "/" + sDownLoadName + ".unity3d";
#endif

            WWW downloadwww = new WWW("file:///" + sResourceSavePath);
            yield return www;

            if (downloadwww.isDone)
            {
                AssetBundleInfoList[i].BundleWWW = downloadwww;
                nLoadCnt++;
            }
        }

        if (AssetBundleInfoList.Count == nLoadCnt)
            bAssetBundleExists = true;
    }

    #endregion

    #region Get 구문

    /// <summary>
    /// 에셋번들의 데이터를 가져옵니다.
    /// </summary>
    /// <param name="sCurSceneName"> 씬 이름 </param>
    /// <returns></returns>
    public WWW GetWWWInAssetBundle(string sCurSceneName)
    {
        for (int i = 0; i < AssetBundleInfoList.Count; i++)
        {
            if (AssetBundleInfoList[i].sKey.Equals(sCurSceneName))
                return AssetBundleInfoList[i].BundleWWW;
        }
        return null;
    }

    /// <summary>
    /// 에셋 번들 파일이 있는 지 유무 판단
    /// 딱 한번만실행합니다.
    /// </summary>
    /// <returns></returns>
    public bool GetAssetBundleFileExists()
    {
        if (bAssetBundleExists == false)
        {
            int nCnt = 0;
            string sResourceSavePath = string.Empty;

            // 다운받아야할 목록
            for (int i = 0; i < AssetBundleInfoList.Count; i++)
            {
                string sDownLoadName = AssetBundleInfoList[i].sKey;
#if UNITY_EDITOR
                sResourceSavePath = Application.dataPath + "/Resources/0_AssetBundles/" + sDownLoadName + ".unity3d";
#else
                sResourceSavePath = Application.persistentDataPath + "/" + sDownLoadName + ".unity3d";
#endif

                FileInfo fInfo = new FileInfo(sResourceSavePath);

                if (fInfo.Exists)
                {
                    nCnt++;
                }
            }

            if (AssetBundleInfoList.Count == nCnt)
            {
                bAssetBundleExists = true;
                return true;
            }
            else
                return false;
        }
        return true;
    }

    /// <summary>
    /// 현재 아이템 몇 퍼센트인지 가져옵니다.
    /// </summary>
    /// <returns></returns>
    public float GetPercentage()
    {
        return (float)(nLoadCnt) / AssetBundleInfoList.Count;
    }

    /// <summary>
    /// 에셋 번들 리스트 정보를 가져옵니다.
    /// </summary>
    /// <param name="sDownLoadName"> 다운로드 받을 아이템의 이름 </param>
    /// <returns></returns>
    public int GetAssetBundleListIndex(string sDownLoadName)
    {
        for (int i = 0; i < AssetBundleInfoList.Count; i++)
        {
            if (AssetBundleInfoList[i].sKey == sDownLoadName)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #region Destroy 구문

    /// <summary>
    /// 삭제하는 함수입니다.
    /// </summary>
    /// <param name="sCurSceneName"> 지울 씬 </param>
    /// <returns> 체크 반환합니다. </returns>
    public bool DestroyWWWAssetBundle(string sCurSceneName)
    {
        for (int i = 0; i < AssetBundleInfoList.Count; i++)
        {
            if (AssetBundleInfoList[i].sKey.Equals(sCurSceneName))
            {
                AssetBundleInfoList[i].BundleWWW.assetBundle.Unload(false);
                AssetBundleInfoList[i].BundleWWW.Dispose();
                AssetBundleInfoList[i].BundleWWW = null;
                return true;
            }
        }
        return false;
    }

    #endregion
}
