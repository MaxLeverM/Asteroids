using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.Systems;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;
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