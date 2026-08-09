using Rossoforge.Core.Audio;
using UnityEngine;

namespace RossoGames.Settings.Service
{
    [CreateAssetMenu(fileName = nameof(SettingsServiceData), menuName = "RossoGames/Service Data/Settings")]
    public class SettingsServiceData : ScriptableObject
    {
        [field: SerializeField]
        public AudioChannelData MusicChannelData { get; set; }

        [field: SerializeField]
        public AudioChannelData SfxChannelData { get; set; }
    }
}
