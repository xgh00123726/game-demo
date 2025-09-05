using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

public class CommonDetailShadowView
{
    private static GameObject _shadowObj;
    private static Image _iconImage;
    static CommonDetailShadowView()
    {
        _shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(4));

        _shadowObj.transform.SetParent(RootCanvas.Instance.transform, false);

        _iconImage = _shadowObj.transform.Find("Icon").GetComponent<Image>();
        if (_iconImage == null)
        {
            XLogger.Instance.Level(XLogger.LogLevel.Error)
                .Log("panel item must has icon object");
        }
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
