using UnityEngine;

/// <summary>
/// Responsive Camera Scaler
/// </summary>
public class CameraAspectRatio : MonoBehaviour
{
    public static CameraAspectRatio instance;
    public float speed = 0.2f;

    public float distanceToSpeedUp = 14f;

    public float deadYPosition = -10f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Reference Resolution like 1920x1080
    /// </summary>
    public Vector2 ReferenceResolution;

    /// <summary>
    /// Zoom factor to fit different aspect ratios
    /// </summary>
    public Vector3 ZoomFactor = Vector3.one;

    /// <summary>
    /// Design time position
    /// </summary>
    [HideInInspector]
    public Vector3 OriginPosition;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        OriginPosition = transform.position;
    }

    /// <summary>
    /// Update per Frame
    /// </summary>
    void Update()
    {

        if (ReferenceResolution.y == 0 || ReferenceResolution.x == 0)
            return;

        var refRatio = ReferenceResolution.x / ReferenceResolution.y;
        var ratio = (float)Screen.width / (float)Screen.height;

        transform.position = OriginPosition + transform.forward * (1f - refRatio / ratio) * ZoomFactor.z
                                            + transform.right * (1f - refRatio / ratio) * ZoomFactor.x
                                            + transform.up * (1f - refRatio / ratio) * ZoomFactor.y;

        if (UIManager.instance.distance >= distanceToSpeedUp)
        {
            speed += 0.25f;
            distanceToSpeedUp += 14f;
        }
        Movement();
    }

    void Movement()
    {
        if (GameManager.instance.gameStarted && !GameManager.instance.failed)
        {
            if (PlayerController.instance.transform.position.y > transform.position.y + 4f)
            {
                transform.Translate(0f, Time.deltaTime * 12f, 0f);
            }
            if (UIManager.instance.distance >= 10f)
            {
                transform.Translate(0f, Time.deltaTime * speed, 0f);
                deadYPosition = transform.position.y - 7f;
            }
        }
        else
        {
            transform.position = new Vector3(0f, 1f, -5f);
        }
    }
}
