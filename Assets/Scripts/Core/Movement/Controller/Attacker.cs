using System;
using UnityEngine;

namespace Assets.Scripts.Core.Movement.Controller
{
    public class Attacker
    {

        private event Action ActionRequested;
        private event Action AnimationEnded;

        public bool IsAttack { get; private set; }

        public void StartAttack()
        {
            IsAttack = true;

            ActionRequested += Attack;
            AnimationEnded += EndAttack;
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }

        private void EndAttack()
        {
            ActionRequested -= Attack;
            AnimationEnded -= EndAttack;

            IsAttack = false;
        }

        public void OnActionRequested() => ActionRequested?.Invoke();
        public void OnAnimationEnded() => AnimationEnded?.Invoke();
    }
}
