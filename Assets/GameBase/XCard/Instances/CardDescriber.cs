using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.XCard
{
    public class CardDescriber : MonoBehaviour
    {
        private static Transform _transform;
        public static Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }
        public static string Text
        {
            get => _tmpText.text;
            set => _tmpText.text = value;
        }
        static TMP_Text _tmpText;
        static GameObject _textObject;
        static GameObject _imageObject;

        public static bool Active
        {
            get => _imageObject.activeSelf;
            set => SetUIActive(value);
        }

        private static void SetUIActive(bool active)
        {
            _textObject.SetActive(active);
            _imageObject.SetActive(active);
        }
        // Start is called before the first frame update
        void Awake()
        {
            _tmpText = transform.Find("Text").GetComponent<TMP_Text>();
            Assert.IsNotNull(_tmpText);

            _transform = transform;

            _textObject = transform.Find("Text").gameObject;
            Assert.IsNotNull(_textObject);

            _imageObject = transform.Find("Image").gameObject;
            Assert.IsNotNull(_imageObject);

            SetUIActive(false);
        }
    }
}
