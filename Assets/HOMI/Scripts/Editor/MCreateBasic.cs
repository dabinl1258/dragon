using UnityEngine;
using UnityEditor;
using System.Collections;

public class MCreateBasic : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////
    // Create Basic
    ////////////////////////////////////////////////////////////////
    [MenuItem("HOMI/Create Basic Folder", validate = true, priority = -1)]
    static public bool GetBasicFolder()
    {
        string strDirPath = Application.dataPath;
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(strDirPath + "/Resources");

        return !dir.Exists;
    }

    [MenuItem("HOMI/Create Basic Folder",validate = false ,priority = -1)]
    static public void CreateBasicFolder()
    {
        string strDirPath = Application.dataPath;
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(strDirPath + "/Resources");

        if (dir.Exists == false)
            dir.Create();

        dir = new System.IO.DirectoryInfo(strDirPath + "/Resources/0_AssetBundles");

        if (dir.Exists == false)
            dir.Create();

        // 내부에 추가할 폴더 이름들
        string[] strFolderName = {
                "0_Atlases",
                "1_Prefabs",
                "2_Scripts",
                "3_Animations",
                "4_Fonts",
                "5_Sounds",
                "6_Textures",
                "7_Shader"
            };

        // 폴더 생성
        foreach (string str in strFolderName)
        {
            dir = new System.IO.DirectoryInfo(strDirPath + "/Resources" + "/0_Common/" + str);

            if (dir.Exists == false)
                dir.Create();
        }

        System.IO.FileInfo fileInfo = new System.IO.FileInfo(strDirPath + "/HOMI" + "/Resources" + "/Prefabs" + "/Loading/" + "HLoadingPrefab.prefab");

        if(fileInfo.Exists)
        {
            fileInfo.CopyTo(strDirPath + "/HOMI" + "/Resources" + "/Prefabs" + "/Loading" + "1.prefab", true);

            System.IO.FileInfo fileMove = new System.IO.FileInfo(strDirPath + "/HOMI" + "/Resources" + "/Prefabs" + "/Loading" + "1.prefab");
            fileMove.MoveTo(strDirPath + "/Resources" + "/0_Common/" + "/1_Prefabs/" + "HLoadingPrefab.prefab");
        }

        dir = new System.IO.DirectoryInfo(strDirPath + "/Scenes");

        if (dir.Exists == false)
            dir.Create();

        // 에디터 업데이트
        EditorApplication.update();

        // 에셋 초기화 (최신으로)
        AssetDatabase.Refresh();
    }

}
