using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereScript : MonoBehaviour
{
    // [SerializeField]
    GameObject _camera;
    [SerializeField]
    GameObject cameraAnchor;

    Rigidbody body;
    float forceFactor = 500f;
    Vector3 anchorOffset;

    AudioSource collectSound;
    AudioSource backgroundMusic;

    static SphereScript instance = null;
    bool isLoadBack;
    float loadBackTimer = 3f;

    void Start()
    {
        if (instance != null)
        {
            this.transform.position += new Vector3(0, instance.transform.position.y, 0);
            Destroy(instance.gameObject);
        }
        instance = this;
        isLoadBack = SceneManager.GetActiveScene().name == "SolarSystem";

        DontDestroyOnLoad(this.gameObject);
        _camera = Camera.main.gameObject;

        body = GetComponent<Rigidbody>();
        anchorOffset = this.transform.position - cameraAnchor.transform.position;
        AudioSource[] sources = GetComponents<AudioSource>();
        collectSound = sources[0];
        backgroundMusic = sources[1];

        if (!LabyrinthState.isSoundMuted)
        {
            // backgroundMusic.volume = LabyrinthState.musicVolume;
            backgroundMusic.Play();
        }

        LabyrinthState.AddListener(OnLabyrinthStateChanged);
    }

    void Update()
    {
        float kh = Input.GetAxis("Horizontal");
        float kv = Input.GetAxis("Vertical");
        Vector3 right = _camera.transform.right;
        Vector3 forward = _camera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 forceDirection = (kh * right + kv * forward).normalized;
        body.AddForce(forceFactor * Time.deltaTime * forceDirection);
        cameraAnchor.transform.position = this.transform.position - anchorOffset;

        if (isLoadBack)
        {
            if (loadBackTimer > 0f)
            {
                loadBackTimer -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Labyrinth");
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("CheckPoint"))
        {
            if (!LabyrinthState.isSoundMuted)
            {
                collectSound.volume = LabyrinthState.effectsVolume;
                collectSound.Play();
            }
        }
    }

    void OnLabyrinthStateChanged(string propertyName)
    {
        if (propertyName == nameof(LabyrinthState.musicVolume))
        {
            if (backgroundMusic.volume != LabyrinthState.musicVolume)
            {
                backgroundMusic.volume = LabyrinthState.musicVolume;
            }
        }
        else if (propertyName == nameof(LabyrinthState.effectsVolume))
        {
            if (collectSound.volume != LabyrinthState.effectsVolume)
            {
                collectSound.volume = LabyrinthState.effectsVolume;
            }
        }
    }

    void OnDestroy()
    {
        LabyrinthState.RemoveListener(OnLabyrinthStateChanged);
    }
}
