using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

namespace ParticleSystemExample
{
    public class CrumbleSystem : ParticleSystem
    {
        Color[] colors = new Color[]{
        Color.Fuchsia,
        Color.Red,
        Color.Green,
        Color.HotPink,
        Color.Gainsboro,
        Color.LimeGreen
        };
        Color color;
        public CrumbleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 25) { }
        protected override void InitializeConstants()
        {
            textureFilename = "circle";
            minNumParticles = 8;
            maxNumParticles = 10;
            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }
        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(120, 200);
            var lifetime = RandomHelper.NextFloat(0.3f, 0.5f);
            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            var acceleration = -velocity / lifetime;
            var scale = RandomHelper.NextFloat(3, 6);
            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }
        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);
            float normalizelifetime = particle.TimeSinceStart / particle.Lifetime;
            

            particle.Scale = 0.1f + 0.025f * normalizelifetime;
        }
        public void PlaceCrumble(Vector2 where)
        {

            color = Color.White;
           
            AddParticles(where);
        }
    }
}
