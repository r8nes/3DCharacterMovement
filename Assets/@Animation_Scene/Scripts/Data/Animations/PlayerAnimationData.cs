using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerAnimationData
    {
        [Header("State Group Parameters Names")]
        [SerializeField] private string _groundedParameterName = "Grounded";
        [SerializeField] private string _movingParameterName = "Moving";
        [SerializeField] private string _stoppingParameterName = "Stopping";
        [SerializeField] private string _landingParameterName = "Landing";
        [SerializeField] private string _airborneParameterName = "Airborne";

        [Header("Grounded Parameters Names")]
        [SerializeField] private string _idleParameterName = "isIdling";
        [SerializeField] private string _dashParameterName = "isDashing";
        [SerializeField] private string _walkingParameterName = "isWalking";
        [SerializeField] private string _runParameterName = "isRunning";
        [SerializeField] private string _sprintParameterName = "isSprinting";
        [SerializeField] private string _mediumStopParameterName = "isMediumStopping";
        [SerializeField] private string _hardStopParameterName = "isHardStopping";
        [SerializeField] private string _rollParameterName = "isRolling";
        [SerializeField] private string _hardLandParameterName = "isHardLanding";

        [Header("Airborne Parameters Names")]
        [SerializeField] private string _fallParameterName = "isFalling";

        public int GroundedParameterHash { get; private set; }
        public int MovingParameterHash { get; private set; }
        public int StoppingParameterHash { get; private set; }
        public int LandingParameterHash { get; private set; }
        public int AirborneParameterHash { get; private set; }

        public int IdleParameterHash { get; private set; }
        public int DashParameterHash { get; private set; }
        public int WalkParameterHash { get; private set; }
        public int RunParameterHash { get; private set; }
        public int SprintParameterHash { get; private set; }
        public int MediumStopParameterHash { get; private set; }
        public int HardStopParameterHash { get; private set; }
        public int RollParameterHash { get; private set; }
        public int HardLandingParameterHash { get; private set; }

        public int FallParameterHash { get; private set; }

        public void Init() 
        {
            GroundedParameterHash = Animator.StringToHash(_groundedParameterName);
            MovingParameterHash = Animator.StringToHash(_movingParameterName);
            StoppingParameterHash = Animator.StringToHash(_stoppingParameterName);
            LandingParameterHash = Animator.StringToHash(_landingParameterName);
            AirborneParameterHash = Animator.StringToHash(_airborneParameterName);

            IdleParameterHash = Animator.StringToHash(_idleParameterName);
            DashParameterHash = Animator.StringToHash(_dashParameterName);
            WalkParameterHash = Animator.StringToHash(_walkingParameterName);
            RunParameterHash = Animator.StringToHash(_runParameterName);
            SprintParameterHash = Animator.StringToHash(_sprintParameterName);
            MediumStopParameterHash = Animator.StringToHash(_mediumStopParameterName);
            HardStopParameterHash = Animator.StringToHash(_hardLandParameterName);
            RollParameterHash = Animator.StringToHash(_rollParameterName);
            HardLandingParameterHash = Animator.StringToHash(_hardLandParameterName);

            FallParameterHash = Animator.StringToHash(_fallParameterName);
        }
    }
}
