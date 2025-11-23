using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace GameBase.Tools
{
    /// <summary>
    /// EntitySysÄ£°å
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntitySys<T>
    {
        public EntitySys(IEContainer<T> container)
        {
            _container = container;
        }

        protected internal List<T> _entityNeedRegister = new();
        protected internal List<T> _entitiesNeedRemove = new();
        protected internal List<T> _entitiesNeedStart = new();

        public Action<T> StartAction {  get; set; }
        public Action<T> UpdateAction { get; set; }
        
        protected internal IEContainer<T> _container;

        protected internal bool _inUpdating = false;
        protected internal int _currIterIndex = 0;

        public IEContainer<T> Entities
        {
            get => _container;
            set => _container = value;
        }
        public bool InUpdating => _inUpdating;
        public int CurrentIterIndex => _currIterIndex;

        public void AddEntity(T e)
        {
            if (_entityNeedRegister.Contains(e))
            {
                return;
            }
            _entitiesNeedStart.Add(e);

            if (_inUpdating)
            {
                _entityNeedRegister.Add(e);
            }
            else
            {
                _container.Add(e);
            }
        }

        public void AddEntityImmediately(T e)
        {
            if (_entityNeedRegister.Contains(e))
            {
                return;
            }
            _entitiesNeedStart.Add(e);
            _container.Add(e);
        }

        public void RemoveEntity(T e)
        {
            if (_entitiesNeedRemove.Contains(e))
            {
                return;
            }

            _entitiesNeedRemove.Add(e);
        }

        public void Iterate()
        {
            foreach (var e in _entityNeedRegister)
            {
                _container.Add(e);
            }
            _entityNeedRegister.Clear();

            foreach (var e in _entitiesNeedStart)
            {
                StartAction?.Invoke(e);
            }
            _entitiesNeedStart.Clear();

            foreach (var e in _entitiesNeedRemove)
            {
                _container.Remove(e);
            }
            _entitiesNeedRemove.Clear();

            _currIterIndex = 0;
            _inUpdating = true;
            foreach (var e in _container)
            {
                UpdateAction?.Invoke(e);
                _currIterIndex++;
            }
            _inUpdating = false;
        }
    }
}
