using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Destroy;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.Systems;
using Asteroids.Scripts.ECS.Systems.Input;
using Asteroids.Scripts.ECS.UnityComponents;
using LeoEcsPhysics;
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
            
            EcsPhysicsEvents.ecsWorld = world;
            
            systems.Add(new PlayerInitSystem())
                .Add(new AsteroidPoolCreatorSystem())
                .Add(new InputMoveSystem())
                .Add(new InputRotationSystem())
                .Add(new InputFireSystem())
                .Add(new InputAdditionalFireSystem())
                .Add(new AsteroidSpawnSystem())
                
                .Add(new SpaceEngineRotateSystem())
                .Add(new SpaceEngineMoveSystem())
                .Add(new MovableSystem())
                .Add(new FieldBorderSystem())
                .Add(new RechargeTimerSystem())
                .Add(new RechargeFireSystem())
                .Add(new BulletSpawnSystem())
                
                .Add(new DestroyOnCollideSystem())
                .Add(new DamageSystem())
                
                .Add(new HealthLostSystem())
                .Add(new DestroyTimerSystem())
                
                .Add(new ReleasePoolObjectSystem<AsteroidView>())
                .Add(new DestroyTransformSystem())
                .Add(new DestroyEntitySystem());

            systems.Inject(fieldBound)
                .Inject((ISceneContext)sceneContext)
                .Inject((IConfig)config);

            systems.OneFrame<FireEvent>()
               // .OneFrame<OnTriggerEnter2DEvent>()
                .OneFrame<DestroyEvent>();

            systems.OneFramePhysics2D(); //its not working.... fixed
            
            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void OnDestroy()
        {
            EcsPhysicsEvents.ecsWorld = null;
            systems.Destroy();
            systems = null;
            world.Destroy();
            world = null;
        }
    }
}