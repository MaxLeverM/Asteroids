using System;
using Asteroids.Scripts.Gameplay;
using UnityEngine;

namespace Asteroids.Scripts.UI
{
    public class GameStatisticView : MonoBehaviour
    {
        [SerializeField] private TextPatternDrawer scoreDrawer;
        [SerializeField] private TextPatternDrawer shipPositionDrawer;
        [SerializeField] private TextPatternDrawer shipRotationDrawer;
        [SerializeField] private TextPatternDrawer shipVelocityDrawer;
        [SerializeField] private TextPatternDrawer laserShotsDrawer;
        [SerializeField] private TextPatternDrawer rechargeTimeDrawer;
        
        private Starship starship;
        
        public void Init(Player player, Starship starship)
        {
            this.starship = starship;
            
            OnScoreValueChanged(player.ScoreHandler.Score);
           // OnShotCountChanged(starship.AdditionalGun.ShotCount);  // Make drawer for different guns, use simple factory or other patter for spawn drawer. but it must be reactive
           // OnRechargeTimeChanged(starship.AdditionalGun.RechargeTime);
            OnPositionUpdate(starship.MovableSpaceObject.Position);
            OnRotationUpdate(starship.MovableSpaceObject.Rotation);
            OnVelocityUpdate(starship.MovableSpaceObject.Velocity);

            player.ScoreHandler.OnScoreValueChanged += OnScoreValueChanged;
           // starship.AdditionalGun.OnShotCountChanged += OnShotCountChanged;
           // starship.AdditionalGun.OnRechargeTimeChanged += OnRechargeTimeChanged;
            starship.MovableSpaceObject.OnPositionUpdate += OnPositionUpdate;
            starship.MovableSpaceObject.OnRotationUpdate += OnRotationUpdate;
            starship.MovableSpaceObject.OnVelocityUpdate += OnVelocityUpdate;
        }

        private void OnScoreValueChanged(int score)
        {
            scoreDrawer.Draw(score);
        }

        private void OnShotCountChanged(int currentShotsCount)
        {
       //     laserShotsDrawer.Draw(currentShotsCount, starship.AdditionalGun.MaxShotCount);
        }

        private void OnRechargeTimeChanged(float currentRechargeTime)
        {
       //     rechargeTimeDrawer.Draw((int) currentRechargeTime, starship.AdditionalGun.RechargeTime);
        }

        private void OnPositionUpdate(Vector2 position)
        {
            shipPositionDrawer.Draw(Math.Round(position.x, 2), Math.Round(position.y, 2));
        }

        private void OnRotationUpdate(Quaternion rotation)
        {
            var axis = Vector3.forward;
            rotation.ToAngleAxis(out float angle, out axis);
            shipRotationDrawer.Draw(angle);
        }

        private void OnVelocityUpdate(Vector2 velocity)
        {
            shipVelocityDrawer.Draw(velocity.magnitude);
        }
    }
}