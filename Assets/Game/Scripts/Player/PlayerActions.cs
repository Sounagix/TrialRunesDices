using System;
using UnityEngine;

namespace Isometric2DGame.Player
{
    public static class PlayerActions
    {
        public static Action<Vector2> OnMovementToNorth;
        public static Action<Vector2> OnMovementToSouth;
        public static Action<Vector2> OnMovementToEast;
        public static Action<Vector2> OnMovementToWest;
        public static Action<Vector2> OnMovementStop;
        public static Action OnPlayerShoot;
        public static Action OnPlayerMeleeAttack;
        public static Action<float, float> OnPlayerReceiveDamage;
        public static Action OnPlayerAction;
        public static Action<Weapon> EquipWeapon;
        public static Action<Potion> EquipPotion;
    }
}

