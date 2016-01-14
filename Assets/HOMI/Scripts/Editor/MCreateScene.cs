using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public enum E_M_GUI_CREATE_TYPE
{
    Nothing,
    OnlyManager,
    ManagerWithAudioEngine,
    RootI,
    RootIWithRootII,
    RootIWithRootIIWithRootIII,
    RootIWithRootIIWithRootIIIWithRootIV,
    ManagerWithRootI,
    ManagerWithRootIWithRootII,
    ManagerWithRootIWithRootIIWithRootIII,
    ManagerWithRootIWithRootIIWithRootIIIWithRootIV,
    FullSetting,
}

public class MCreateScene : EditorWindow
{
    //
    // Create Windows
    //

    [MenuItem("HOMI/Open Scene Maker", priority = 10000)]
    static public void ShowWindow()
    {
        // 윈도우 생성
        MCreateScene window = (MCreateScene)EditorWindow.GetWindow(typeof(MCreateScene));
        window.title = "Scene Maker";
    }

    // Variable Field
    string strSceneNumber;
    string strSceneName;
    string strScriptName;
    E_M_GUI_CREATE_TYPE eSelectionCreateType = E_M_GUI_CREATE_TYPE.Nothing;

    string[] strMngSource
    = {
    "using UnityEngine;",                                                               // 0
    "using System.Collections;",                                                        // 1
    "using System.Collections.Generic;",                                                // 2
    "using MHomiLibrary;",                                                              // 3

    "public class ", "HMenuSceneMng", ": HSingletonScene<","HMenuSceneMng",">",         // 4, 5, 6, 7, 8
    "{",                                                                                // 9
    "    protected ","HMenuSceneMng","() { } ",                                         // 10, 11, 12
    "",                                                                                 // 13
    "    void Awake()",                                                                 // 14
    "    {",                                                                            // 15
    "        cSceneList = new Dictionary<string, HState>();",                           // 16
    "",                                                                                 // 17
    "        for (int i = 0; i < SceneList.Count; i++)",                                // 18
    "            cSceneList.Add(GetClassName(SceneList[i].ToString()), SceneList[i]);", // 19
    "    }",                                                                            // 20
    "",                                                                                 // 21
    "    void Start()",                                                                 // 22
    "    {",                                                                            // 23
    "        HPrefabMng.I.DestroyPrefabs();",                                           // 24
    "        ChangeScene( " ,"HMenuScene" , " );",                                      // 25, 26, 27
    "    }",                                                                            // 28
    "}"                                                                                 // 29

    }; 

    string[] strSceneSource
    = {
    "using UnityEngine;",                                                               // 0
    "using System.Collections;",                                                        // 1
    "using MHomiLibrary;",                                                              // 2
    "",                                                                                 // 3
    "public class ", "HMenuScene", ": HState ",                                         // 4, 5, 6
    "{",                                                                                // 7
    "",                                                                                 // 8
    "   public UILabel InfoLabel = null;",                                              // 9
    "",                                                                                 // 10
    "   public override void Enter(params object[] oParams)",                           // 11
    "   {",                                                                             // 12
    "",                                                                                 // 13
    "   }",                                                                             // 14
    "",                                                                                 // 15
    "   public override void Execute()",                                                // 16
    "   {",                                                                             // 17
    "",                                                                                 // 18
    "   }",                                                                             // 19
    "",                                                                                 // 20
    "   public override void Exit()",                                                   // 21
    "   {",                                                                             // 22
    "",                                                                                 // 23
    "   }",                                                                             // 24
    "}",                                                                                // 25

    };

    string nameMng;
    string nameScene;


    //
    // Windows Field
    //
    void OnGUI()
    {
        // 보여지는 UI 관련 null 시 초기화
        if (strSceneNumber == null)
            strSceneNumber = EditorGUI.TextField(new Rect(10, 10, 300, 17), "Scene Number", "EX) 5");

        if (strSceneName == null)
            strSceneName = EditorGUI.TextField(new Rect(10, 30, 300, 17), "Scene Name", "EX) Menu");

        if (strScriptName == null)
            strScriptName = EditorGUI.TextField(new Rect(10, 50, 300, 17), "User Initials", "EX) M = (마광휘)");

        // 실시간 문자열 받아오기
        strSceneNumber = EditorGUI.TextField(new Rect(10, 10, 300, 17), "Scene Nunmber", strSceneNumber);
        strSceneName = EditorGUI.TextField(new Rect(10, 30, 300, 17), "Scene Name", strSceneName);
        strScriptName = EditorGUI.TextField(new Rect(10, 50, 300, 17), "User Initials", strScriptName);

        // Enum 팝업
        eSelectionCreateType = (E_M_GUI_CREATE_TYPE)EditorGUI.EnumPopup(new Rect(10, 70, 300, 17), "Create Type", eSelectionCreateType);

        // 생성 할 시
        if(GUI.Button(new Rect(180,120,100,50), "Create"))
        {
            // 만약 입력 안했을 시 작성 해달라고 생성
            if (strSceneNumber.Equals("EX) 5") || strSceneName.Equals("EX) Menu") || strSceneName.Equals("EX) MMenuScene"))
            {
                Debug.Log("Scene Path와 Scene Name과 Menu Script Name을 작성 해주세요.");
                return;
            }

            // 기본 경로
            string strDirPath = Application.dataPath + "/Resources" + "/" + strSceneNumber + "_" + strSceneName + "Scene";

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

            // 디렉토리 생성
            System.IO.DirectoryInfo dirPath = new System.IO.DirectoryInfo(strDirPath);

            if (dirPath.Exists == false)
                dirPath.Create();

            // 폴더 생성
            foreach (string str in strFolderName)
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(strDirPath + "/" + str);

                if (dir.Exists == false)
                    dir.Create();
            }

            // 씬 폴더 생성
            System.IO.DirectoryInfo dirSceneScript = new System.IO.DirectoryInfo(strDirPath + "/" + strFolderName[2] + "/0_Scene");

            if (dirSceneScript.Exists == false)
                dirSceneScript.Create();

            string data = Application.dataPath + "/Scenes/" + strSceneNumber + "_" + strSceneName + "Scene.unity";

            // Scene 파일 생성
            if (System.IO.File.Exists(data))
                Debug.Log("씬 이름이 중복되어 파일이 생성이 되지 않았습니다.");
            else
                System.IO.File.Create(data);

            // 스크립트 파일 생성
            // 매니저
            string scriptData = dirSceneScript + "/" + strScriptName + strSceneName + "SceneMng.cs";
            nameMng = strScriptName + strSceneName + "SceneMng";
            nameScene = strScriptName + strSceneName + "Scene";

            // Manager 클래스 내부 제작
            // 이 부분은 건들지 마시오..
            System.IO.File.WriteAllText(scriptData, strMngSource[0], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[1], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[2], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[3], Encoding.Default);

            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[4] + nameMng + strMngSource[6] + nameMng + strMngSource[8], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[9], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[10] + nameMng + strMngSource[12], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[13], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[14], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[15], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[16], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[17], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[18], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[19], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[20], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[21], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[22], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[23], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[24], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[25] + "\"" + nameScene + "\"" + strMngSource[27], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[28], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strMngSource[29], Encoding.Default);

            // 상태
            scriptData = dirSceneScript + "/" + strScriptName + strSceneName + "Scene.cs";

            // 이 부분은 건들지 마시오..
            System.IO.File.WriteAllText(scriptData, strSceneSource[0], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[1], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[2], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[3], Encoding.Default);

            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[4] + nameScene + strSceneSource[6], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[7], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[8], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[9], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[10], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[11], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[12], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[13], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[14], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[15], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[16], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[17], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[18], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[19], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[20], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[21], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[22], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[23], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[24], Encoding.Default);
            System.IO.File.AppendAllText(scriptData, "\r\n" + strSceneSource[25], Encoding.Default);

            string enumFileName = Application.dataPath + "/HOMI/Scripts/External/MSceneEnums.cs";

            string[] fileLines = System.IO.File.ReadAllLines(enumFileName, Encoding.Default);
            string[] tempFileLines = new string[fileLines.Length + 1];

            for (int i = 0; i < fileLines.Length - 1; i++)
                tempFileLines[i] = fileLines[i];

            tempFileLines[tempFileLines.Length - 3] = "    E_" + strSceneNumber + "_" + strSceneName + "Scene,";
            tempFileLines[tempFileLines.Length - 2] = "    E_MAX"; 
            tempFileLines[tempFileLines.Length - 1] = "}";

            System.IO.File.WriteAllLines(enumFileName, tempFileLines, Encoding.UTF8);

            // 에디터 업데이트
            EditorApplication.update();

            // 에셋 초기화 (최신으로)
            AssetDatabase.Refresh();

            // 포커싱 될 씬 열기
            EditorApplication.OpenScene(data);

            // enum 설정한 것 대로 prefab 및 씬 세팅
            switch (eSelectionCreateType)
            {
                case E_M_GUI_CREATE_TYPE.Nothing:
                    break;
                case E_M_GUI_CREATE_TYPE.OnlyManager:
                    MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");
                    break;
                case E_M_GUI_CREATE_TYPE.ManagerWithAudioEngine:
                    MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");

                    MHOMITools.AddComponent("0_Mngs", "MAudioPlayMng");
                    MHOMITools.CreateChildObject("0_Mngs", "AudioMng", "AudioListener");

                    MAudioPlayMng obj = GameObject.Find("0_Mngs").GetComponent<MAudioPlayMng>();
                    obj.AudioManager = GameObject.Find("AudioMng");
                    break;
                case E_M_GUI_CREATE_TYPE.RootI:
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    break;
                case E_M_GUI_CREATE_TYPE.RootIWithRootII:
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");
                    break;
                case E_M_GUI_CREATE_TYPE.RootIWithRootIIWithRootIII:
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_III", "Root_III");
                    break;
                case E_M_GUI_CREATE_TYPE.RootIWithRootIIWithRootIIIWithRootIV:
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_III", "Root_III");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_IV", "Root_IV");
                    break;
                case E_M_GUI_CREATE_TYPE.ManagerWithRootI:
                    MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");

                    break;
                case E_M_GUI_CREATE_TYPE.ManagerWithRootIWithRootII:
                    MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");

                    break;
                case E_M_GUI_CREATE_TYPE.ManagerWithRootIWithRootIIWithRootIII:
                    MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_III", "Root_III");

                    break;
                case E_M_GUI_CREATE_TYPE.ManagerWithRootIWithRootIIWithRootIIIWithRootIV:
                    MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_III", "Root_III");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_IV", "Root_IV");

                    break;
                case E_M_GUI_CREATE_TYPE.FullSetting:
                    MHOMITools.CreateObject("0_Mngs", "HMng", "HPrefabMng", "HConfigMng", "HEtcMng", "MAssetBundleMng");

                    MHOMITools.AddComponent("0_Mngs", "MAudioPlayMng");
                    MHOMITools.CreateChildObject("0_Mngs", "AudioMng", "AudioListener");

                    MAudioPlayMng obj2 = GameObject.Find("0_Mngs").GetComponent<MAudioPlayMng>();
                    obj2.AudioManager = GameObject.Find("AudioMng");

                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_I", "Root_I");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_II", "Root_II");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_III", "Root_III");
                    MHOMITools.CreateResourceObject("Prefabs/Root/Root_IV", "Root_IV");

                    break;
            }

            // 씬 저장
            EditorApplication.SaveScene();
            // 저장
            EditorApplication.SaveAssets();

            // Build Setting 에 넣기
            EditorBuildSettingsScene ebss = new EditorBuildSettingsScene();
            ebss.path = "Assets/Scenes/" + strSceneNumber + "_" + strSceneName + "Scene.unity";
            ebss.enabled = true;

            EditorBuildSettingsScene[] ebssArray = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length + 1];

            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                ebssArray[i] = EditorBuildSettings.scenes[i];

            ebssArray[EditorBuildSettings.scenes.Length] = ebss;

            EditorBuildSettings.scenes = ebssArray;

            Close();
        }
    }
}