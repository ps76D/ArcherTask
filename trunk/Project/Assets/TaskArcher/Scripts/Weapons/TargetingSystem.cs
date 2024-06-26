﻿using NaughtyAttributes;
using TaskArcher.Units;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace TaskArcher.Weapons
{
    public class TargetingSystem : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private GameObject targetPrefab;
        [SerializeField] private bool isTargeting;
        [SerializeField] private AimConstraint constraintToTarget;
        [SerializeField] private Transform startTransform;
        [SerializeField] private Vector3 distance;
        [SerializeField] private Trajectory trajectory;

        [InfoBox("Auto Set:")]
        [SerializeField] private Transform targetTransform;
        
        private Camera _mainCamera;
        
        public Vector3 Distance => distance;

        private void Start()
        {
            InitMainCamera();

            InitTargetCursor();
        }

        private void Update()
        {
            CheckIfTargeting();

            CheckIfShoot();
        }

        private void CalculateTargetDistance()
        {
            distance = startTransform.position - targetTransform.position;
        }
        
        private void InitMainCamera()
        {
            if (Camera.main != null)
            {
                _mainCamera = Camera.main;
            }
        }
        
        private void InitTargetCursor()
        {
            if (targetTransform != null) return;
            targetTransform = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity).transform;

            InitJointToTargetConstraint(constraintToTarget);
            
            targetTransform.gameObject.SetActive(false);
        }

        private void InitJointToTargetConstraint(AimConstraint jointWithConstraint)
        {
            ConstraintSource constraintSource = new ConstraintSource
            {
                sourceTransform = targetTransform,
                weight = 1
            };
            
            jointWithConstraint.AddSource(constraintSource);
        }

        private void CheckIfTargeting()
        {
            if (!Mouse.current.rightButton.isPressed) return;
            
            if (!isTargeting)
            {
                targetTransform.gameObject.SetActive(true);
                
                trajectory.ShowTrajectoryInTime();
                
                player.OnChangeAnim?.Invoke(player.Animator, ConstsAnimNames.AttackStartClipName);

                isTargeting = true;
            }
                
            Vector3 pos = Mouse.current.position.ReadValue();

            if (!_mainCamera) return;
                
            Vector3 targetPos = _mainCamera.ScreenToWorldPoint(pos);
            targetPos.z = 0;
                
            targetTransform.position = targetPos;

            CalculateTargetDistance();
        }

        private void CheckIfShoot()
        {
            if (!Mouse.current.rightButton.wasReleasedThisFrame) return;
            targetTransform.gameObject.SetActive(false);
            trajectory.HideTrajectory();
            
            player.Weapon.OnAttack?.Invoke();
            isTargeting = false;
        }
    }
}