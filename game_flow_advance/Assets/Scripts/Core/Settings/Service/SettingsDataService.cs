using Rossoforge.Audio.DataConfig;
using UnityEngine;

namespace Rossogames.Settings.Service
{
    [CreateAssetMenu(fileName = nameof(SettingsDataService), menuName = "Rossogames/Data Service/Settings")]
    public class SettingsDataService : ScriptableObject
    {
        [field: SerializeField]
        public AudioChannelDataConfig MusicChannelData { get; set; }

        [field: SerializeField]
        public AudioChannelDataConfig SfxChannelData { get; set; }
    }
}
