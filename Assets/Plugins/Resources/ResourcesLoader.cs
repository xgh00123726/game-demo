using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameBase.Resources
{

    public class ResourcesLoader
    {
        public static ResourcesMgr<GameObject> _prefabMgr;
        public static ResourcesMgr<Sprite> _spriteMgr;
        public static ResourcesMgr<Texture2D> _textureMgr;
        public static ResourcesMgr<AudioClip> _audioClipMgr;

        public static ResourcesMgr<GameObject> Prefab => _prefabMgr;
        public static ResourcesMgr<Sprite> Sprite => _spriteMgr;
        public static ResourcesMgr<Texture2D> Texture2D => _textureMgr;
        public static ResourcesMgr<AudioClip> AudioClip => _audioClipMgr;

        private static void CopyTexturesToSprite()
        {
            _spriteMgr = new(null);
            foreach (var kvp in _textureMgr._resources)
            {
                var texture = kvp.Value;
                var sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                _spriteMgr._resources.Add(kvp.Key, sprite);
            }
        }

        public static T LoadAddressable<T>(string filePath)
        {
            return Addressables.LoadAssetAsync<T>(filePath).WaitForCompletion();
        }

        public static GameObject InstantiateGameObject(string name)
        {
            return GameObject.Instantiate(Prefab.Get(name));
        }

        public static void LoadAllAsset()
        {
            _prefabMgr = new($"{Application.streamingAssetsPath}/Preload/Prefab.csv");
            _textureMgr = new($"{Application.streamingAssetsPath}/Preload/Texture2D.csv");
            _audioClipMgr = new($"{Application.streamingAssetsPath}/Preload/Audio.csv");

            CopyTexturesToSprite();
        }

    }
}
