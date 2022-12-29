using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Components
{
    public class ScoreSystem : IEcsRunSystem
    {
        private EcsFilter<FinalDamageOwner, ScoreReward, DestroyEvent> filter;
        public void Run()
        {
            foreach (var i in filter)
            {
                var scoreRecipient = filter.Get1(i).owner;
                if (!scoreRecipient.IsAlive() || !scoreRecipient.Has<Score>()) continue;
                
                ref var scoreReward = ref filter.Get2(i);
                ref var score = ref scoreRecipient.Get<Score>();
                score.score += scoreReward.points;
            }
        }
    }
}