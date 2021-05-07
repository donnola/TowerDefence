using System.Collections.Generic;
using Assets;
using UnityEditor;
using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(menuName = "Assets/Asset Root", fileName = "Asset Root")]
    public class AssetRoot : ScriptableObject
    {
        public SceneAsset UIScene;
        public List<LevelAsset> Levels;
    }
}