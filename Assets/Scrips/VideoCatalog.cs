using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VideoEntry
{
    public string title;
    public string fileName;
    public Sprite thumbnail;
    public string duration;
    public bool loop;
}

[CreateAssetMenu(menuName = "Video360/Catalog")]
public class VideoCatalog : ScriptableObject
{
    public List<VideoEntry> videos = new();
}