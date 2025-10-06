using GameBase.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class MaskImage
    {
        private Image _image;
        private int _textureID;
        private Material _material;
        private Texture2D _texture;
        public int TextureID => _textureID;
        public Material Material => _material;

        public MaskImage(Image image)
        {
            _image = image;
        }

        public void SetDir(float dir1, float dir2)
        {
            _material.SetFloat("_Dir1", dir1);
            _material.SetFloat("_Dir2", dir2);
        }

        public void SetIcon(int textureID)
        {
            if (_textureID == textureID)
            {
                return;
            }

            _textureID = textureID;

            if (textureID < 0)
            {
                return;
            }

            _material = new Material(_image.material);

            _material.SetFloat("_Dir1", -1f);
            _material.SetFloat("_Dir2", -1f);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(textureID));

            _material.SetTexture("_Target", texture);

            _image.material = _material;
        }
    }
}
