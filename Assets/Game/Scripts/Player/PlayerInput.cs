using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Isometric2DGame.Player
{


    public class PlayerInput : MonoBehaviour
    {
        private PlayerSystemInput _inputActions;

        private void OnEnable()
        {
            _inputActions = new PlayerSystemInput();
            _inputActions.Player.North.performed += OnNorthMovement;
            _inputActions.Player.South.performed += OnSouthMovement;
            _inputActions.Player.West.performed  += OnWestMovement;
            _inputActions.Player.East.performed  += OnEastMovement;
            _inputActions.Player.Shoot.performed += OnPlayerShoot;
            _inputActions.Player.Melee.performed += OnPlayerMeleeAttack;


            _inputActions.Player.North.canceled  += OnMovementStopFromNorth;
            _inputActions.Player.South.canceled  += OnMovementStopFromSouth;
            _inputActions.Player.West.canceled   += OnMovementStopFromWest;
            _inputActions.Player.East.canceled   += OnMovementStopFromEast;

            _inputActions.Enable();
        }



        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.North.performed -= OnNorthMovement;
            _inputActions.Player.South.performed -= OnSouthMovement;
            _inputActions.Player.West.performed  -= OnWestMovement;
            _inputActions.Player.East.performed  -= OnEastMovement;
            _inputActions.Player.Shoot.performed -= OnPlayerShoot;
            _inputActions.Player.Melee.performed -= OnPlayerMeleeAttack;



            _inputActions.Player.North.canceled  -= OnMovementStopFromNorth;
            _inputActions.Player.South.canceled  -= OnMovementStopFromSouth;
            _inputActions.Player.West.canceled   -= OnMovementStopFromWest;
            _inputActions.Player.East.canceled   -= OnMovementStopFromEast;
        }


        private void OnNorthMovement(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementToNorth?.Invoke(Vector2.up);
        }

        private void OnSouthMovement(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementToSouth?.Invoke(Vector2.down);
        }

        private void OnWestMovement(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementToWest?.Invoke(Vector2.left);
        }

        private void OnEastMovement(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementToEast?.Invoke(Vector2.right);
        }

        private void OnMovementStopFromNorth(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementStop?.Invoke(Vector2.up);
        }

        private void OnMovementStopFromSouth(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementStop?.Invoke(Vector2.down);
        }

        private void OnMovementStopFromEast(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementStop?.Invoke(Vector2.right);
        }

        private void OnMovementStopFromWest(InputAction.CallbackContext context)
        {
            PlayerActions.OnMovementStop?.Invoke(Vector2.left);
        }

        private void OnPlayerShoot(InputAction.CallbackContext context)
        {
            PlayerActions.OnPlayerShoot?.Invoke();
        }

        private void OnPlayerMeleeAttack(InputAction.CallbackContext context)
        {
            PlayerActions.OnPlayerMeleeAttack?.Invoke();

        }
    }
}
