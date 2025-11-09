using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Resources
{
    public class AudioMgr
    {
        internal static GameObject audioSouceObj;
        internal static AudioSource audioSouce;

        static AudioMgr()
        {
            var prefab = ResourcesLoader.LoadAddressable<GameObject>("Prefabs/Audio/AudioSource.prefab");
            audioSouceObj = GameObject.Instantiate(prefab);
            audioSouce = audioSouceObj.GetComponent<AudioSource>();
        }

        public static void Play(string audioFile)
        {
            if (audioFile == null)
            {
                return;
            }
            audioSouce.clip = ResourcesLoader.AudioClip.Get(audioFile);
            audioSouce.Play();
        }

        public static void PlayAt(string audioFile, Vector3 position)
        {
            if (audioFile == null)
            {
                return;
            }
            audioSouce.clip = ResourcesLoader.AudioClip.Get(audioFile);
            audioSouceObj.transform.position = position;
            audioSouce.Play();
        }
    }
}
