using UnityEngine;
using Assets.Scripts.Core.Tools;
using Assets.Scripts.Core.Animation;
using Assets.Scripts.Core.Movement.Data;
using Assets.Scripts.Core.Movement.Controller;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private DirectionalMovementData _directionalMovementData;
        [SerializeField] private JumpData _jumpData;
        [SerializeField] private DirectionalCameraPair _cameras;

        private Rigidbody2D _rigidbody;
        private DirectionalMover _directionalMover;
        private Jumper _jumper;
        private Attacker _attacker;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _directionalMover = new DirectionalMover(_rigidbody, _directionalMovementData);
            _jumper = new Jumper(_rigidbody, _jumpData, _directionalMovementData.MaxSize);
            _attacker = new Attacker ();

        }
        private void Update()
        {
            if (_jumper.IsJumping)
                _jumper.UpdateJump();

            UpdateAnimations();
            UpdateCameras();
        }
        private void UpdateCameras()
        {
            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = cameraPair.Key == _directionalMover.Direction;
        }

        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Idle, true);
            _animator.PlayAnimation(AnimationType.Jump, _jumper.IsJumping);
            _animator.PlayAnimation(AnimationType.Attack, _attacker.IsAttack);
            _animator.PlayAnimation(AnimationType.Run, _directionalMover.IsMoving);
        }
        public void MoveHorizontally(float direction) => _directionalMover.MoveHorizontally(direction);
        public void MoveVertically(float direction)
        {
            if (_jumper.IsJumping)
                return;

            _directionalMover.MoveVertically(direction);
        }

        public void Jump() => _jumper.Jump();
        public void StartAttack() => _attacker.StartAttack();


        protected void OnActionRequested() => _attacker.OnActionRequested();
        protected void OnAnimationEnded() => _attacker.OnAnimationEnded();

    }
}