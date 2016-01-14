using UnityEditor;
using UnityEngine;
using System.Collections;

public class MBuildAssetBundle {

    [MenuItem("Assets/Select Build AssetBundles")]
    static void ExportResource()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");

        if (path.Length != 0)
        {
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            Debug.Log("Select Cnt : " + selection.Length);

            foreach (Object SelObj in selection)
            {
                Debug.Log("Select Name : " + SelObj.ToString());
            }

#if UNITY_ANDROID
            BuildPipeline.BuildAssetBundle( Selection.activeObject, 
                                            selection, path, 
                                            BuildAssetBundleOptions.CollectDependencies | 
                                            BuildAssetBundleOptions.CompleteAssets | 
                                            BuildAssetBundleOptions.DeterministicAssetBundle,
                                            BuildTarget.Android);
#endif


#if UNITY_IPHONE
            BuildPipeline.BuildAssetBundle( Selection.activeObject, 
                                            selection, path, 
                                            BuildAssetBundleOptions.CollectDependencies | 
                                            BuildAssetBundleOptions.CompleteAssets | 
                                            BuildAssetBundleOptions.DeterministicAssetBundle,
                                            BuildTarget.iPhone);
#endif

            Selection.objects = selection;
            selection = null;
        }
    }
}
