using GameBase.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class SuperImage
    {
        private int _textureID;
        private Sprite _sprite;
        private Image _image;
        private Color _colorHide;
        private Color _colorShow;
        public int TextureID => _textureID;
        public Sprite Sprite => _sprite;

        public SuperImage(Image image)
        {
            _image = image;
        }

        public void SetIcon(int textureID)
        {
            if (_textureID == textureID)
            {
                return;
            }
            if (textureID < 0)
            {
                _sprite = null;
                _image.sprite = null;
                return;
            }
            
            _textureID = textureID;
            _sprite = ResourcesLoader.GetSpriteFromTextureID(textureID);
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
            (_textureID, other._textureID) = (other._textureID, _textureID);
        }

        public void Copy(SuperImage other)
        {
            SetIcon(other.TextureID);
            SetColor(other._image.color);
            SetHideColor(other._colorHide);
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
