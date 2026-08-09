using DG.Tweening;
using RossoGames.LevelObjects.Components;
using RossoGames.Tokens.DataBehaviour;
using UnityEngine;

namespace RossoGames.Tokens.Components
{
    public class Token : LevelObject
    {
        [SerializeField]
        private TokenDataBehaviour _dataBehaviour;

        private Sequence _moveSequence;

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_moveSequence != null)
                _moveSequence.Kill();

            transform.DOKill();
        }

        public async Awaitable Move(Vector3[] positions)
        {
            if (positions == null || positions.Length == 0)
                return;

            var source = new AwaitableCompletionSource();
            _moveSequence = DOTween.Sequence().Pause();

            Vector3 currentPos = transform.position;

            for (int i = 0; i < positions.Length; i++)
            {
                Vector3 targetPos = positions[i];

                float distance = Vector3.Distance(currentPos, targetPos);
                float duration = distance / _dataBehaviour.MoveSpeed;

                var tween = transform
                    .DOMove(targetPos, duration)
                    .SetEase(Ease.Linear);

                _moveSequence.Append(tween);
                currentPos = targetPos;
            }

            _moveSequence.OnComplete(() => source.SetResult());
            _moveSequence.Play();

            await source.Awaitable;
        }
    }
}
