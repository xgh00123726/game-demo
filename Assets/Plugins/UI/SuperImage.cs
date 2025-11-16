using GameBase.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class SuperImage
    {
        private string _textureName;
        private Sprite _sprite;
        private Image _image;
        private Color _colorHide;
        private Color _colorShow;
        public string TextureName => _textureName;
        public Sprite Sprite => _sprite;

        public SuperImage(Image image)
        {
            _image = image;
        }

        public void SetIcon(string textureName)
        {
            if (_textureName == textureName)
            {
                return;
            }
            if (textureName == null)
            {
                _sprite = null;
                _image.sprite = null;
                _textureName = textureName;
                return;
            }

            _textureName = textureName;
            _sprite = ResourceMgr.Sprite.Get(textureName);
            _image.sprite = _sprite;
        }

        public void Show()
        {
            _image.sprite = _sprite;
        }

        public void Hide()
        {
            _image.sprite = null;
        }

        public void Swap(SuperImage other)
        {
            (_sprite, other._sprite) = (other._sprite, _sprite);

            (_image.sprite, other._image.sprite) = (_sprite, other._sprite);

            (_image.color, other._image.color) = (other._image.color, _image.color);
            (_colorHide, other._colorHide) = (other._colorHide, _colorHide);
            (_colorShow, other._colorShow) = (other._colorShow, _colorShow);
            (_textureName, other._textureName) = (other._textureName, _textureName);
        }

        public void Copy(SuperImage other)
        {
            SetIcon(other.TextureName);
            SetColor(other._image.color);
            SetHideColor(other._colorHide);
        }

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public void SetColor(int rarity)
        {
            _image.color = ViewConfig.GetColor(rarity);
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }

        public void SetHideColor(Color color)
        {
            _colorHide = color;
        }

        public void HideColor()
        {
            _colorShow = _image.color;
            _image.color = _colorHide;
        }

        public void ShowColor()
        {
            _image.color = _colorShow;
        }
    }
}
