using UnityEngine;

namespace GameBase.Effects
{
    public class DestIndicator : CircleIndicator
    {
        protected override void Awake()
        {
            base.Awake();
            var particle = _side.GetComponent<ParticleSystem>();
            var particleMain = particle.main;
            particleMain.startColor = new ParticleSystem.MinMaxGradient(Color.red);
            Radius = 0.3f;
        }
    }
}
