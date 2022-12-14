using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.Systems;
using Asteroids.Scripts.ECS.Systems.Input;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using Leopotam.Ecs.UnityIntegration;

namespace Asteroids.Scripts.ECS
{
    public class Core : MonoBehaviour
    {
        private EcsWorld world;
        private EcsSystems systems;
        [SerializeField] private Config config;
        [SerializeField] private SceneContext sceneContext;
        
        private void Start()
        {
            FieldBound fieldBound = new FieldBound();

            world = new EcsWorld();
#if UNITY_EDITOR
            EcsWorldObserver.Create (world);
#endif
            systems = new EcsSystems(world);
            
            systems.Add(new PlayerInitSystem())
                .Add(new InputMoveSystem())
                .Add(new InputRotationSystem())
                .Add(new InputFireSystem())
                .Add(new InputAdditionalFireSystem())
                .Add(new SpaceEngineRotateSystem())
                .Add(new SpaceEngineMoveSystem())
                .Add(new MovableSystem())
                .Add(new FieldBorderSystem());
            
            systems.Inject(fieldBound)
                .Inject((ISceneContext)sceneContext)
                .Inject((IConfig)config);

            systems.OneFrame<FireEvent>();
            
            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void OnDestroy()
        {
            systems.Destroy();
            world.Destroy();
        }
    }
}