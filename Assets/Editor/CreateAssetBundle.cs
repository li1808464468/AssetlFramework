using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle : Editor
{
    [MenuItem("Tools/CreateBundle")]
    public static void CreateBundle()
    {
        BuildPipeline.BuildAssetBundles("./AssetBundles/Test", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
