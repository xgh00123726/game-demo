using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetailShadowView
{
    private static GameObject _shadowObj;
    private static Image _iconImage;
    private static TextMeshProUGUI _textTMP;
    static EquipmentDetailShadowView()
    {
        _shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(4));

        _shadowObj.transform.SetParent(RootCanvas.Instance.transform, false);

        _iconImage = _shadowObj.transform.Find("Icon").GetComponent<Image>();

        _textTMP = _shadowObj.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        if (_iconImage == null)
        {
            XLogger.Instance.Level(XLogger.LogLevel.Error)
                .Log("panel item must has icon object");
        }
    }

    public static void SetText(string text)
    {
        _textTMP.text = text;
    }


    public static void SetPosition(Vector3 position)
    {
        _shadowObj.SetActive(true);
        _shadowObj.transform.position = position;
    }

    public static void Hide()
    {
        _shadowObj.SetActive(false);
    }
}
