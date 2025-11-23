using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    public enum ViewTag
    {
        None = 1,

        All = 0x7FFFFFFF,
    };

    public class ViewMgr
    {
        private static int _count = 0;
        private static Dictionary<ViewTag, LinkedList<BaseViewItem>> _views = new();

        static ViewMgr()
        {
            foreach (ViewTag tag in Enum.GetValues(typeof(ViewTag)))
            {
                _views.Add(tag, new LinkedList<BaseViewItem>());
            }
        }

        internal static void RegisterView(BaseViewItem view)
        {
            _views[view.tag].AddLast(view);
            _count++;
        }

        internal static void RemoveView(BaseViewItem view)
        {
            _views[view.tag].Remove(view);
            _count--;
        }

        public static int GetViewCount()
        {
            return _count;
        }

        public static int GetViewCount(ViewTag tag)
        {
            return _views[tag].Count;
        }

        public BaseViewItem GetItem(Vector3 position, ViewTag tag = ViewTag.All)
        {
            foreach (var k in _views.Keys)
            {
                if ((k & tag) != 0)
                {
                    foreach (var e in _views[k])
                    {
                        Rect r = e.RectTransform.rect;
                        r.center = e.UIScript.transform.position;
                        if (r.Contains(position))
                        {
                            return e;
                        }
                    }
                }
            }
            
            return null;
        }
    }
}
