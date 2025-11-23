using UnityEngine;
using UnityEngine.UI;

namespace GameBase.Resources
{
    public static class Utils
    {
        public static Sprite ToSprite(this Texture2D texture)
        {
            return UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        public static void SetSprite(this Image image, string textureName)
        {
            Sprite sprite = null;
            if (ResourceMgr.Sprite._resources.ContainsKey(textureName))
            {
                sprite = ResourceMgr.Sprite.Get(textureName);
                image.sprite = sprite;
            }
            else
            {
                var texture = ResourceMgr.Texture2D.Get(textureName);
                sprite = texture.ToSprite();
                image.sprite = sprite;
                ResourceMgr._spriteMgr._resources.Add(textureName, sprite);
            }
        }
    }
}
