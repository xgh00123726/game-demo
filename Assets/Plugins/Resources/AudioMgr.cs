using GameBase.Resources;
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
            audioSouce.Play();
        }

        public static void PlayAt(string audioFile, Vector3 position)
        {
            audioSouceObj.transform.position = position;
            audioSouce.Play();
        }
    }
}
