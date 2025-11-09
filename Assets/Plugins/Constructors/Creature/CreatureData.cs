using GameBase.AI;
using GameBase.Animations;
using UnityEngine;

namespace Constructor.Creatures
{
    public class CreatureData
    {
        public string prefabName;
        public GameBase.Creatures.CampType camp;
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
