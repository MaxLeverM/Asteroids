using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Leopotam.Ecs.UnityIntegration;
using InputSystem = Asteroids.Scripts.ECS.UnityComponents.InputSystem;

namespace Asteroids.Scripts.ECS
{
    public class Core : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems systems;
        [SerializeField] private Config config;
        [SerializeField] private SceneContext sceneContext;
        
        private void Start()
        {
            FieldBound fieldBound = new FieldBound();

            _world = new EcsWorld();
#if UNITY_EDITOR
            EcsWorldObserver.Create (_world);
#endif
            systems = new EcsSystems(_world);

            systems.Add(new PlayerInitSystem())
                .Add(new InputSystem())
                .Add(new SpaceEngineSystem())
                .Add(new MovableSystem())
                .Add(new FieldBorderSystem());
            
            systems.Inject(fieldBound)
                .Inject((ISceneContext)sceneContext)
                .Inject((IConfig)config);
            
            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void OnDestroy()
        {
            systems.Destroy();
            _world.Destroy();
        }
    }
}