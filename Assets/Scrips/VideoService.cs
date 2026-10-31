using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoService : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Action onVideoPrepared;
    public Action onVideoFinished;

    void Awake()
    {
        // Eventos del VideoPlayer[cite: 6]
        videoPlayer.prepareCompleted += (vp) => onVideoPrepared?.Invoke();
        videoPlayer.loopPointReached += (vp) => onVideoFinished?.Invoke();
    }

    public void LoadAndPlay(string fileName, bool loop)
    {
        // Carga el video desde StreamingAssets por URL[cite: 4]
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        videoPlayer.url = path;
        videoPlayer.isLooping = loop;
        videoPlayer.Prepare();
    }

    public void Play() { videoPlayer.Play(); }
    public void Pause() { videoPlayer.Pause(); }
    public void Stop() { videoPlayer.Stop(); }
}