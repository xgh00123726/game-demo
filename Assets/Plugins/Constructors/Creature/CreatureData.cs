using GameBase.AI;
using GameBase.Animations;
using GameBase.Creatures;
using UnityEngine;

namespace Constructor.Creatures
{
    public class CreatureData
    {
        public string prefabName;
        public Camp.Typedef camp;
        public AnimType animType;
        public AIType aiType;
        public float damage;
        public float moveSpeed;
        public float maxHP;
        public float healthRegen;
        public float attackRange;
        public Vector3 healthBarOffset;
        public bool collideEnable;
    }
}
