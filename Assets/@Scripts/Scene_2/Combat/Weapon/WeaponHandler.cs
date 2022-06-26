using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Weapon
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _hand;

        public void EnableAttack() 
        {
            _hand.SetActive(true);
        }

        public void DisableAttack()
        {
            _hand.SetActive(false);

        }
    }
}
