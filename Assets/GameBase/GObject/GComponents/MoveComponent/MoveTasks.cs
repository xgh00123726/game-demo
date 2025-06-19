using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Object
{
    internal class MoveTasks
    {
        private List<MoveTask> _tasks = new List<MoveTask>();

        public MoveTask this[int i]
        {
            get
            {
                return _tasks[i];
            }
            set
            {
                _tasks[i] = value;
            }
        }
        public int Count => _tasks.Count;


        public void Add(int priority, Vector3 dest)
        {
            _tasks.Add(new MoveTask() { priority = priority, dest = dest });
        }
        public void Clear()
        {
            _tasks.Clear();
        }
        /*
         * @FUNC 为任务排序，优先级(priority)越小，任务优先级越高
         * @COMMENT 
         */
        public void Sort()
        {
            _tasks.Sort((MoveTask t1, MoveTask t2) =>
            {
                return t2.priority - t1.priority;
            });
        }
    }
}
