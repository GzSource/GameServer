using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class NullLance : IGameScript
    {
        public void OnActivate(IObjAiBase owner)
        {
        }

        public void OnDeactivate(IObjAiBase owner)
        {
        }

        public void OnStartCasting(IObjAiBase owner, ISpell spell, IAttackableUnit target)
        {
            AddParticleTarget(owner, "Kassadin_Base_Q_cas.troy", owner, 1, "L_HAND");
        }

        public void OnFinishCasting(IObjAiBase owner, ISpell spell, IAttackableUnit target)
        {
            spell.AddProjectileTarget("NullLance", target, true);
        }

        public void ApplyEffects(IObjAiBase owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
            var AbilityPower = owner.Stats.AbilityPower.Total * 0.7f;
            var SilenceTime = new[] { 0, 1.5f, 1.75f, 2, 2.25f, 2.5f }[spell.Level];
            var Damage = new[] { 0, 80, 130, 180, 230, 280 }[spell.Level] + AbilityPower;
            if (target != null && !target.IsDead)
            {
                target.TakeDamage(owner, Damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL,false);
                AddBuff("Silence", SilenceTime, 1, spell, (IObjAiBase)target, owner);
            }
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
