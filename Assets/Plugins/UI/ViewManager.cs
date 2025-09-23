using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    public enum ViewTag
    {
        None,
    };

    public class ViewManager
    {
        private static int _count = 0;
        private static Dictionary<ViewTag, LinkedList<BaseViewItem>> _views = new();

        static ViewManager()
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

        public static Transform GetRootCanvas()
        {
            return RootCanvas.Instance.transform;
        }

        public static Transform GetWorldCanvas()
        {
            return WorldCanvs.Instance.transform;
        }
    }
}
