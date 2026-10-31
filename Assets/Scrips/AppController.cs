using UnityEngine;

// Definición de los 4 estados obligatorios[cite: 5, 6]
public enum AppState { Menu, Loading, Playing, Paused }

public class AppController : MonoBehaviour
{
    public AppState State { get; private set; }
    public VideoService videoService;

    public GameObject menuCanvas;
    public GameObject loadingIndicator; // RF-08: Indicador de "Cargando..."[cite: 4]
    public GameObject playbackControls;

    public Material skyboxNeutral;
    public Material skyboxVideo360;
    public RenderTexture videoRenderTexture;

    private bool currentVideoLoops;

    void Start()
    {
        videoService.onVideoPrepared += OnVideoPrepared;
        videoService.onVideoFinished += OnVideoFinished;
        BackToMenu();
    }

    public void SelectVideo(VideoEntry entry)
    {
        State = AppState.Loading;
        menuCanvas.SetActive(false); // RF-05: El menú se oculta[cite: 4]
        loadingIndicator.SetActive(true);

        // Evitar mostrar el último fotograma del video anterior[cite: 7]
        RenderTexture.active = videoRenderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;

        currentVideoLoops = entry.loop;
        videoService.LoadAndPlay(entry.fileName, entry.loop);
    }

    void OnVideoPrepared()
    {
        State = AppState.Playing;
        loadingIndicator.SetActive(false);
        RenderSettings.skybox = skyboxVideo360; // Cambiar skybox al material del video[cite: 7]
        videoService.Play();
    }

    public void TogglePause()
    {
        if (State == AppState.Playing)
        {
            videoService.Pause();
            State = AppState.Paused;
        }
        else if (State == AppState.Paused)
        {
            videoService.Play();
            State = AppState.Playing;
        }
    }

    public void BackToMenu()
    {
        State = AppState.Menu;
        videoService.Stop();
        RenderSettings.skybox = skyboxNeutral; // Skybox neutro para el menú[cite: 7]

        menuCanvas.SetActive(true);
        loadingIndicator.SetActive(false);

        // RF-07: El menú reaparece frente a la mirada actual del usuario[cite: 4, 7]
        Transform camTransform = Camera.main.transform;
        Vector3 forward = camTransform.forward;
        forward.y = 0;
        forward.Normalize();

        // Esto fuerza el menú a 1.5m de altura y a 2m de distancia, cumpliendo el requisito RF-01
        menuCanvas.transform.position = new Vector3(camTransform.position.x, 1.5f, camTransform.position.z) + forward * 2f;
        menuCanvas.transform.rotation = Quaternion.LookRotation(forward);
    }

    void OnVideoFinished()
    {
        // RF-09: Si el video no tiene loop, regresa al menú al terminar
        if (!currentVideoLoops)
        {
            BackToMenu();
        }
    }
}