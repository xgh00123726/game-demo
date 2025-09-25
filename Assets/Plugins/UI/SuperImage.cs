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
            _image.sprite = _sprite;
            other._image.sprite = other._sprite;
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
            _image.color = _colorHide;
        }
    }
}
