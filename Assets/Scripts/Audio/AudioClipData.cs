using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [Serializable]
    public struct AudioClipData
    {
        [field:SerializeField]public bool Loop { get; set; }
        [field:SerializeField]public AudioClip Clip { get; set; }
        [field:SerializeField]public AudioMixerGroup Group { get; set; }
    }
}