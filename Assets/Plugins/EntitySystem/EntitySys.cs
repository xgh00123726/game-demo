using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    /// <summary>
    /// EntitySysÄ£°å
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntitySys<T, T_Container>
    {
        public EntitySys(T_Container container)
        {
            _container = container;
        }

        protected internal LinkedList<T> _entityNeedRegister = new LinkedList<T>();
        protected internal LinkedList<T> _entitiesNeedRemove = new LinkedList<T>();
        
        protected internal T_Container _container;

        protected internal bool _inUpdating = false;
        protected internal int _currIterIndex = 0;

        //public void AddEntity(T e)
        //{
        //    if (e == null) return;

        //    if (_entitiesNeedRemove.Contains(e))
        //    {
        //        return;
        //    }

        //    if (!_inUpdating)
        //    {
        //        Entities.Remove(e);
        //        Constructor.ReleaseEntity(e);
        //    }
        //    else
        //    {
        //        _entitiesNeedRemove.AddLast(e);
        //    }
        //}

        public void RemoveEntity(T e)
        {
        }

        public void Update()
        {

        }
    }
}
