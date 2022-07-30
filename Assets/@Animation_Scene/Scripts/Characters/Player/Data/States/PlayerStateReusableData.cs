using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    public class PlayerStateReusableData
    {
        public float MovementSpeedMod { get; set; } = 1f;
        public float MovementOnSlopesSpeedMod { get; set; } = 1f;
        public float MovementDecelerationForce { get; set; } = 1f;

        public bool ShouldWalk { get; set; }
        public bool ShouldSprint { get; set; }

        public Vector2 MovementInput { get; set; }
        public Vector3 CurrentJumpForce { get; set; }
        public PlayerRotationData RotationData { get; set; }

        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedTargetRotationCurrentVelocity;
        private Vector3 _dampedTargetRotationPassedTime;

        public ref Vector3 ÑurrentTargetRotation { get => ref _currentTargetRotation; }
        public ref Vector3 TimeToReachTargetRotation { get => ref _timeToReachTargetRotation; }
        public ref Vector3 DampedTargetRotationCurrentVelocity { get => ref _dampedTargetRotationCurrentVelocity; }
        public ref Vector3 DampedTargetRotationPassedTime { get => ref _dampedTargetRotationPassedTime; }

        public List<PlayerCameraRecenteringData> BackwardsCameraRecentering { get; set; }
        public List<PlayerCameraRecenteringData> SideWaysCameraRecentering { get;  set; }
    }
}