using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Combat
{
    public class Targeter : MonoBehaviour
    {
        private Camera _mainCamera;

        [SerializeField] private Cinemachine.CinemachineTargetGroup _targetGroup;
        [SerializeField] private List<Target> targets = new List<Target>();

        public Target CurrentTarget { get; private set; }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        public bool SelectTarget() 
        {
            if (targets.Count == 0) return false;

            Target closestTarget = null;
            float closestTargetDistance = Mathf.Infinity;

            foreach (var target in targets)
            {
                Vector2 viewPos = _mainCamera.WorldToViewportPoint(target.transform.position);

                if (viewPos.x < 0 || viewPos.y > 1 || viewPos.y < 0 || viewPos.y > 1)
                {
                    continue;
                }

                Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);

                if (toCenter.sqrMagnitude < closestTargetDistance)
                {
                    closestTarget = target;
                    closestTargetDistance = toCenter.sqrMagnitude;
                }
            }

            if (closestTarget == null) return false;
            
            CurrentTarget = closestTarget;
            _targetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

            return true;
        }

        public void Cancel() 
        {
            if (CurrentTarget == null) return;
            _targetGroup.RemoveMember(CurrentTarget.transform);

            CurrentTarget = null;
        }


        private void RemoveTarget(Target target)
        {
            if (CurrentTarget == target)
            {
                _targetGroup.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
            }

            target.OnDestroyed -= RemoveTarget;
            targets.Remove(target);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Target target)) return;

            targets.Add(target);
            target.OnDestroyed += RemoveTarget; 
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Target target)) return;

            RemoveTarget(target);
        }
    }
}
