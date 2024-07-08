using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public static class LoadAssetHeplder
{
    private static Dictionary<string, List<UnityEngine.Object>> cache = new Dictionary<string, List<UnityEngine.Object>>();
    /// <summary>
    /// Use for loading Unity Asset like texture,mesh, scriptable object,...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="additionalFilter"></param>
    /// <returns></returns>
    public static List<T> GetListTypeInAssets<T>(string additionalFilter = "t:") where T : UnityEngine.Object
    {
        string filter = $"{additionalFilter}{typeof(T).Name}";
        if (cache.TryGetValue(filter, out var cachedAssets))
        {
            return cachedAssets.Cast<T>().ToList();
        }

        string[] guids = AssetDatabase.FindAssets(filter);
        List<T> assetList = new List<T>();
        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var assetItem = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (assetItem != null)
            {
                assetList.Add(assetItem);
            }
            else
            {
                Debug.LogWarning($"Failed to load asset of type {typeof(T).Name} at path: {assetPath}");
            }
        }

        cache[filter] = assetList.Cast<UnityEngine.Object>().ToList();
        return assetList;
    }

}
#endif