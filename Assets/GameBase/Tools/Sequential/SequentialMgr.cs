using System.Collections.Generic;
using UnityEngine;
namespace GameBase.Tools
{
    public class SequentialMgr : MonoBehaviour
    {
        internal static List<SequentialBoolFixed> _seqFixedBools = new List<SequentialBoolFixed>();
        internal static List<SequentialBool> _seqBools = new List<SequentialBool>();
        internal static void Add(SequentialBoolFixed seq)
        {
            _seqFixedBools.Add(seq);
        }
        internal static void Add(SequentialBool seq)
        {
            _seqBools.Add(seq);
        }
        internal static void Remove(SequentialBoolFixed seq)
        {
            _seqFixedBools.Remove(seq);
        }
        internal static void Remove(SequentialBool seq)
        {
            _seqBools.Remove(seq);
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        private void Update()
        {
            foreach (var seq in _seqBools)
            {
                seq._lastValue = seq._currValue;
            }
        }
        void FixedUpdate()
        {
            foreach (var seq in _seqFixedBools)
            {
                seq._lastValue = seq._currValue;
            }
        }
    }
}
