using GameBase.Projectile;


namespace GameBase.Spell
{
    public class Attack : GSpell
    {
        protected override void OnCast()
        {
            base.OnCast();
            
            if (_speller is IProjectileOwner owner)
            {
                new Projectile<Arrow>()
                {
                    owenr = owner,
                    dest = dest,
                    damage = effective
                }.SetAttr();
            }
        }
    }
}
