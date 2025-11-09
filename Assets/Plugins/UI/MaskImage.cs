using GameBase.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class MaskImage
    {
        private Image _image;
        private string _textureName;
        private Material _material;
        public string TextureName => _textureName;
        public Material Material => _material;

        public MaskImage(Image image)
        {
            _image = image;
            _material = image.material;
        }

        public void SetDir(float dir1, float dir2)
        {
            _material.SetFloat("_Dir1", dir1);
            _material.SetFloat("_Dir2", dir2);
        }

        public void SetIcon(string textureName)
        {
            if (_textureName == textureName)
            {
                return;
            }

            _textureName = textureName;

            if (textureName == null)
            {
                return;
            }

            _material = new Material(_image.material);

            _material.SetFloat("_Dir1", -1f);
            _material.SetFloat("_Dir2", -1f);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(textureName));

            _material.SetTexture("_Target", texture);

            _image.material = _material;
        }
    }
}
