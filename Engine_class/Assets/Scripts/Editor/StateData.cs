using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CreateStatDataClass
{
    [MenuItem("Assets/FSM/StatData")]
    public static StateDataManager CreateStatData()
    {
        StateDataManager asset = ScriptableObject.CreateInstance<StateDataManager>();
        AssetDatabase.CreateAsset(asset, "Assets/Data/StatData.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}

