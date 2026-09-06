using DG.Tweening;
using Rossogames.LevelObjects.DataBehaviour;
using UnityEngine;

namespace Rossogames.LevelObjects.Components
{
    public class AutomaticDoor : MonoBehaviour,
        IAutomaticInteractionEnter,
        IAutomaticInteractionExit
    {
        public AutomaticDoorDataBehaviour _dataBehaviour;

        [SerializeField]
        private GameObject _door;

        [SerializeField]
        private AudioSource _audioOpen;

        [SerializeField]
        private AudioSource _audioClose;

        private Vector3 _closedPosition;
        private Vector3 _openPosition;

        private void Awake()
        {
            _closedPosition = _door.transform.localPosition;
            _openPosition = _closedPosition + Vector3.up * _dataBehaviour.OpenDistance;
        }

        public void OnAutomaticInteractionEnter()
        {
            OpenDoor();
        }
        public void OnAutomaticInteractionExit()
        {
            CloseDoor();
        }

        private void OpenDoor()
        {
            _door.transform.DOKill();
            _audioOpen.Play();

            _door.transform
                .DOLocalMove(_openPosition, _dataBehaviour.Duration)
                .SetEase(_dataBehaviour.EaseCurve);
        }
        private void CloseDoor()
        {
            _door.transform.DOKill();
            _audioClose.Play();

            _door.transform
                .DOLocalMove(_closedPosition, _dataBehaviour.Duration)
                .SetEase(_dataBehaviour.EaseCurve);
        }
    }
}
