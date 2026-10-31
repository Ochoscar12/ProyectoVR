using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuView : MonoBehaviour
{
    public VideoCatalog catalog;
    public GameObject videoButtonPrefab;
    public Transform menuContainer;
    public AppController appController;

    void Start()
    {
        GenerarBotones();
    }

    void GenerarBotones()
    {
        foreach (VideoEntry video in catalog.videos)
        {

            GameObject btnObj = Instantiate(videoButtonPrefab, menuContainer);

            btnObj.transform.Find("VideoName").GetComponent<TextMeshProUGUI>().text = video.title;
            btnObj.transform.Find("Duration").GetComponent<TextMeshProUGUI>().text = video.duration;
            btnObj.transform.Find("Image").GetComponent<Image>().sprite = video.thumbnail;

            // Registra el clic para avisarle al AppController qué video se eligió
            Button btn = btnObj.GetComponent<Button>();
            btn.onClick.AddListener(() => {
                appController.SelectVideo(video);
            });
        }
    }
}