using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement Instance { get; private set; }

        public Vector2 Playerlocation;
        private RightPlayer _inputActions;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if(InputManager.Instance == null)
            {
                _inputActions = new RightPlayer();
                _inputActions.Enable();
            }
            else
            {
                _inputActions = InputManager.Instance.RightPlayerActions;
            }
        }



        [SerializeField]
        private Movement _movement;

        private Vector2 _latestMovementDirection;
        
        public Vector2 LatestMovementDirection => _latestMovementDirection;

        private void FixedUpdate()
        {
            var movementVector = _inputActions.Main.Movement.ReadValue<Vector2>();

            if(movementVector.sqrMagnitude > 1f)
                movementVector.Normalize();
            
            if(movementVector.sqrMagnitude > 1e-3f)
                _latestMovementDirection = movementVector.normalized;
            
            _movement.Move(movementVector, Time.fixedDeltaTime);

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);

            Playerlocation = transform.position;
        }
    }
}