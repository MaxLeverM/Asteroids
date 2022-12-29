namespace Asteroids.Scripts.ECS.Components
{
    public struct TeamComponent
    {
        public TeamID team;
    }

    public enum TeamID
    {
        Player,
        Enemy
    }
}