using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace TBA_SDK
{
    public class TBA_SDK_BackgroundVideo : MonoBehaviour
    {

        public RawImage rawimage;
        public VideoPlayer videoPlayer;
        public AudioSource AudioPlayer;
        private string localConfigPath = "Assets/VRCSDK/Dependencies/VRChat/Resources/Settings/BackgroundVideo.txt";
        private string localVideoPath = "Assets/VRCSDK/Dependencies/VRChat/Resources/Videos/";
        private string localMusicPath = "Assets/VRCSDK/Dependencies/VRChat/Resources/Audio/";

        void Start()
        {
            if (!File.Exists(localConfigPath))
            {
                File.WriteAllText(localConfigPath, "true");
                StartCoroutine(PlayVideo());
                StartCoroutine(PlayMusic());
            }
            else
            {
                if (CustomBackground())
                {
                    StartCoroutine(PlayVideo());
                    StartCoroutine(PlayMusic());
                }
            }
        }
        IEnumerator PlayMusic()
        {
            AudioPlayer.clip = Resources.Load<AudioClip>("Audio/" + getRandomAudio());

            
            WaitForSeconds waitForSeconds = new WaitForSeconds(1);
            while (!AudioPlayer)
            {
                yield return waitForSeconds;
            }

            AudioPlayer.Play();
        }

        IEnumerator PlayVideo()
        {
            videoPlayer.clip = Resources.Load<VideoClip>("Videos/" + getRandomBackground());

            rawimage.color = Color.white;
            videoPlayer.Prepare();
            WaitForSeconds waitForSeconds = new WaitForSeconds(1);
            while (!videoPlayer.isPrepared)
            {
                yield return waitForSeconds;
            }

            rawimage.texture = videoPlayer.texture;
            videoPlayer.Play();
        }

        private bool CustomBackground()
        {
            if (File.ReadAllText(localConfigPath).Equals("true", StringComparison.InvariantCultureIgnoreCase))
                return true;
            else
                return false;
        }

        private string getRandomBackground()
        {
            List<string> videos = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(localVideoPath);
            FileInfo[] info = dir.GetFiles("*.mp4");
            foreach (FileInfo f in info)
            {
                videos.Add(Path.GetFileNameWithoutExtension(localVideoPath + f.Name));
            }
            var random = new System.Random();
            int num = random.Next(videos.Count);
            return videos[num];
        }

        private string getRandomAudio()
        {
            List<string> Audio = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(localMusicPath);
            FileInfo[] info = dir.GetFiles("*.mp3");
            foreach (FileInfo f in info)
            {
                Audio.Add(Path.GetFileNameWithoutExtension(localMusicPath + f.Name));
            }
            var random = new System.Random();
            int num = random.Next(Audio.Count);
            return Audio[num];
        }
    }
}