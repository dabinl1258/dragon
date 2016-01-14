using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class MCreateExcelParsing : EditorWindow {

    [MenuItem("HOMI/Open Excel Parser", priority = 10000)]
    static public void ShowWindow()
    {
        // 윈도우 생성
        MCreateExcelParsing window = (MCreateExcelParsing)EditorWindow.GetWindow(typeof(MCreateExcelParsing));
        window.title = "Excel Parser";
        window.maxSize = new Vector2(600, 300);
        window.minSize = new Vector2(600, 300);
    }

    string sBasePath = Application.dataPath + "/Resources/";

    Rect rtToggle = new Rect(10, 10, 560, 17);

    string sPath = string.Empty;
    Rect rtPath = new Rect(10, 30, 560, 17);

    string sOutput = string.Empty;
    Rect rtOutput = new Rect(10, 50, 560, 17);

    string sObjectName = string.Empty;
    Rect rtObjectName = new Rect(10, 70, 560, 17);

    string sSheetName = string.Empty;
    Rect rtSheetName = new Rect(10, 90, 560, 17);

    Rect rtCreateButton = new Rect(450, 150, 100, 50);

    bool bOnlyOne = false;

    MExcelDataChunk chunkData = null;

    void OnGUI()
    {
        OnlyCheck();
        PathField();

        //if (!bOnlyOne)
        OutputField();

        ObjectsField();
        SheetNameField();
        ConvertButton();
    }

    void OnlyCheck()
    {
        bOnlyOne = EditorGUI.Toggle(rtToggle, "Have Script", bOnlyOne);

        //if(!bOnlyOne)
        //{
            rtObjectName = new Rect(10, 70, 560, 17);
            rtSheetName = new Rect(10, 90, 560, 17);    
        //}
        //else
        //{
        //    rtObjectName = new Rect(10, 50, 560, 17);
        //    rtSheetName = new Rect(10, 70, 560, 17);   
        //}
    }

    void PathField()
    {
        if (sPath == string.Empty)
            sPath = EditorGUI.TextField(rtPath, "Excel Path", "in resources folder ex) 0_Excels/data.xls");

        sPath = EditorGUI.TextField(rtPath, "Excel Path", sPath);
    }

    void OutputField()
    {
        if (sOutput == string.Empty)
            sOutput = EditorGUI.TextField(rtOutput, "Output Path", "in resources folder ex) 0_Commons/DataTable.cs");

        sOutput = EditorGUI.TextField(rtOutput, "Output Path", sOutput);
    }

    void ObjectsField()
    {
        if (sObjectName == string.Empty)
            sObjectName = EditorGUI.TextField(rtObjectName, "Object Name", "Object Function name ex) sDataLists");

        sObjectName = EditorGUI.TextField(rtObjectName, "Object Name", sObjectName);
    }

    void SheetNameField()
    {
        if (sSheetName == string.Empty)
            sSheetName = EditorGUI.TextField(rtSheetName, "Sheet Name", "ex) Sheet1");

        sSheetName = EditorGUI.TextField(rtSheetName, "Sheet Name", sSheetName);
    }

    void ConvertButton()
    {
        if (!GUI.Button(rtCreateButton, "Convert"))
            return;

        if (sOutput.Equals("in resources folder ex) 0_Commons/DataTable.cs")
        || sPath.Equals("in resources folder ex) 0_Excels/data.xls")
        || sSheetName.Equals("ex) Sheet1"))
            return;

        chunkData = MExcelParser.ReadXLS(sBasePath + sPath, sSheetName);

        ParsingScript();
    }

    void ParsingScript()
    {
        string scriptData = sBasePath + sOutput;

        string[] splitData = sOutput.Split('/', '.');
        string lastName = splitData[splitData.Length - 2];

        string[] strSceneSource
        = {
                "using UnityEngine;",                                                            
                "using System.Collections;",                                                     
                "using MHomiLibrary;",                                       
                "",                                                                              
                "public class "  + lastName + " : HSingleton" + "<" + lastName + ">",
                "{"
        };

        if (bOnlyOne == false)
        {
            System.IO.File.WriteAllText(scriptData, strSceneSource[0] + "\r\n", Encoding.UTF8);
            System.IO.File.AppendAllText(scriptData, strSceneSource[1] + "\r\n", Encoding.UTF8);
            System.IO.File.AppendAllText(scriptData, strSceneSource[2] + "\r\n", Encoding.UTF8);
            System.IO.File.AppendAllText(scriptData, strSceneSource[3] + "\r\n", Encoding.UTF8);
            System.IO.File.AppendAllText(scriptData, strSceneSource[4] + "\r\n", Encoding.UTF8);
            System.IO.File.AppendAllText(scriptData, strSceneSource[5], Encoding.UTF8);
            
            // 0번째에 있는것은 무조껀 변수 명
            System.IO.File.AppendAllText(scriptData, "\r\n" + "    string[,]" + " " + sObjectName + " = " + "{", Encoding.UTF8);
        }
        else
        {
            System.IO.StreamReader stream = new System.IO.StreamReader(scriptData, Encoding.UTF8);
            List<string> sList = new List<string>();

            while(stream.EndOfStream == false)
            {
                string s = stream.ReadLine();
                sList.Add(s);
            }

            stream.Close();

            System.IO.File.WriteAllText(scriptData, sList[0], Encoding.UTF8);

            for(int i = 1; i < sList.Count - 1; i++)
                System.IO.File.AppendAllText(scriptData, "\r\n" + sList[i], Encoding.UTF8);

            sList.Clear();
            sList = null;

            System.IO.File.AppendAllText(scriptData, "\r\r\n" + "     string[,]" + " " + sObjectName + " = " + "{", Encoding.UTF8);
        }

        for(int i = 0 ; i < chunkData.nY; i++)
        {
            System.IO.File.AppendAllText(scriptData, "\r\n" + "     { ", Encoding.UTF8);
            for (int j = 0; j < chunkData.nX; j++)
            {
                if (j == 0)
                    System.IO.File.AppendAllText(scriptData, "\"" + chunkData.arrData[i, 0] + "\"", Encoding.UTF8);
                else
                    System.IO.File.AppendAllText(scriptData, ", " + "\"" + chunkData.arrData[i, j] + "\"", Encoding.UTF8);
            }

            if (i == chunkData.nY - 1)
                System.IO.File.AppendAllText(scriptData, " }", Encoding.UTF8);
            else
                System.IO.File.AppendAllText(scriptData, " },", Encoding.UTF8);
        }

        System.IO.File.AppendAllText(scriptData, "\r\n" + "    }; //#" + "\r\n" + "}", Encoding.UTF8);

        // 에디터 업데이트
        EditorApplication.update();

        // 에셋 초기화 (최신으로)
        AssetDatabase.Refresh();

    }
}
