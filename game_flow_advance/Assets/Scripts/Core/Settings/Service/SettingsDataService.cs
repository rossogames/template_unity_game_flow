using UnityEngine;

namespace RossoGames.Settings.Service
{
    [CreateAssetMenu(fileName = nameof(SettingsDataService), menuName = "RossoGames/Service Data/Settings")]
    public class SettingsDataService : ScriptableObject
    {
        [field: SerializeField]
        public AudioChannelData MusicChannelData { get; set; }

        [field: SerializeField]
        public AudioChannelData SfxChannelData { get; set; }
    }
}
