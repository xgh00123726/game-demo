using GameBase.AI;
using GameBase.Animations;
using GameBase.Creatures;
using UnityEngine;

namespace Constructor.Creatures
{
    public class CreatureData
    {
        public string PrefabName { get; set; }
        public Camp.Typedef Camp {  get; set; }
        public AnimType AnimType { get; set; }
        public AIType AIType {  get; set; }
        public float Damage { get; set; }
        public float MoveSpeed { get; set; }
        public float MaxHP { get; set; }
        public float HealthRegen { get; set; }
        public float AttackRange { get; set; }
        public Vector3 HealthBarOffset { get; set; }
        public bool CollideEnable {  get; set; }
    }
}
