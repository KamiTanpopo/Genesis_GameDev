using Assets.Scripts.Core.Movement.Data;
using UnityEngine;

namespace Assets.Scripts.Core.Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _maxVerticalSize;
        private readonly Transform _transform;
        private readonly Transform _shadowTransform;
        private readonly Vector2 _shadowLocalPosition;
        private readonly Color _shadowColor; 
        private readonly Vector2 _shadowLocalScale;
        
        private float _startJumpVerticalPos;
        private float _shadowVerticalPos;

        public bool IsJumping { get; private set; }

        public Jumper(Rigidbody2D rigidbody2D, JumpData jumpData, float maxVerticalSize)
        {
            _rigidbody = rigidbody2D;
            _jumpData = jumpData;
            _maxVerticalSize = maxVerticalSize;
            _shadowTransform = _jumpData.Shadow.transform;
            _shadowLocalPosition = _shadowTransform.localPosition;
            _transform = _rigidbody.transform;
            _shadowColor = _jumpData.Shadow.color;
            _shadowLocalScale = _jumpData.Shadow.transform.localScale;
        }

        public void Jump()
        {
            if (IsJumping)
                return;

            IsJumping = true;
            _startJumpVerticalPos = _rigidbody.position.y;
            var jumpModificator = _transform.localScale.y / _maxVerticalSize;
            var currentJumpForce = _jumpData.JumpForce * jumpModificator;
            _rigidbody.AddForce(Vector2.up * currentJumpForce);
            _rigidbody.gravityScale = _jumpData.GravityScale * jumpModificator;
            _shadowVerticalPos = _shadowTransform.position.y;
        }

        public void UpdateJump()
        {
            if (_rigidbody.velocity.y < 0 && _rigidbody.position.y <= _startJumpVerticalPos)
            {
                ResetJump();
                return;
            }

            var distance = _rigidbody.transform.position.y - _startJumpVerticalPos;
            _shadowTransform.position = new Vector2(_shadowTransform.position.x, _shadowVerticalPos);
            _jumpData.Shadow.color = new Color(_jumpData.Shadow.color.r, _jumpData.Shadow.color.g, _jumpData.Shadow.color.b, 1 - (distance * _jumpData.ShadowAlphaModificator));
            _shadowTransform.localScale = _shadowLocalScale * (1 + (distance * _jumpData.ShadowSizeModificator));
        }

        private void ResetJump()
        {
            _rigidbody.gravityScale = 0;
            _transform.position = new Vector2(_rigidbody.position.x, _startJumpVerticalPos);

            _shadowTransform.localPosition = _shadowLocalPosition;
            _shadowTransform.localScale = _shadowLocalScale;
            _jumpData.Shadow.color = _shadowColor;

            IsJumping = false;
        }
    }
}
