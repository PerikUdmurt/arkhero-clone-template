using DG.Tweening;
using System;
using UnityEngine;

namespace ArkheroClone.Gameplay.Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private float _animationSpeed;

        [SerializeField]
        private Vector3 _initialPosition;

        public void Init(Transform spawnPoint)
        {
            PlayAnimation(_initialPosition, spawnPoint, _animationSpeed);
        }

        private void PlayAnimation(Vector3 initialPosition, Transform endTransform, float animationSpeed)
        {
            transform.position = endTransform.position - initialPosition;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(endTransform.position, animationSpeed)).
                Join(transform.DORotate(endTransform.rotation.eulerAngles, animationSpeed).
                SetEase(Ease.InCirc));

            sequence.Play();
        }
    }
}