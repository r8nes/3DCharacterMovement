using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Weapon
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _lHand;
        [SerializeField] private GameObject _rHand;

        public void EnableLeftPunch() 
        {
            _lHand.SetActive(true);
        }

        public void EnableRightPunch()
        {
            _rHand.SetActive(true);
        }

        public void DisableLeftPunch()
        {
            _lHand.SetActive(false);

        }

        public void DisableRightPunch() 
        {
            _rHand.SetActive(false);
        }
    }
}
