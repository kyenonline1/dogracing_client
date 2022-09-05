using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AppConfig;

namespace Sound
{
	public class SoundHelper : MonoBehaviour {
        
		public List<AudioClip> clips = new List<AudioClip>();
		public List<ClientConfig.Sound.SoundId> ids = new List<ClientConfig.Sound.SoundId>();
		public Dictionary<ClientConfig.Sound.SoundId, AudioClip> MapSound = new Dictionary<ClientConfig.Sound.SoundId, AudioClip>();

		void Awake()
		{
            for (int i = 0; i < clips.Count && i < ids.Count; i++)
            {
                MapSound.Add(ids[i], clips[i]);
            }
            ClientConfig.Sound.InitSound(this);
        }
        
	}
}