
using GameBase.UI;
using Instance.UI.MVC;

public partial class Player
{
    private int[] _attrNeedShowIDs = {
            0,1,3,2,5,7,4,12,14,6,8,9
    };

    private void AttrInit()
    {
        AttrViewPanel.Instance.SetAttrIcon(_attrNeedShowIDs);
        AttrViewPanel.Instance.layout = new DefaultLayout()
        {
            xInterval = 150,
            yInterval = 35,
            width = 460,
        };
        AttrViewPanel.Instance.Show();
    }

    private void AttrUpdate()
    {
        AttrViewPanel.Instance.SetAttrValue(_attrNeedShowIDs, character);
    }
}
