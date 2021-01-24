using UnityEngine;

public sealed class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public RectTransform center;
    public RectTransform knob;
    public float range;
    public bool fixedJoystick;

    public Vector2 direction;
    private Vector2 start;

    void Start()
    {
        ShowHide(true);
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive)
            return;

        Vector2 pos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            ShowHide(true);
            start = pos;
            knob.position = pos;
            center.position = pos;
        }
        else if (Input.GetMouseButton(0))
        {
            knob.position = pos;
            knob.position = center.position +
                            Vector3.ClampMagnitude(knob.position - center.position, center.sizeDelta.x * range);

            if (knob.position != Input.mousePosition && !fixedJoystick)
            {
                Vector3 outsideBoundsVector = Input.mousePosition - knob.position;
                center.position += outsideBoundsVector;
            }

            direction = (knob.position - center.position).normalized;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ShowHide(false);
        }
    }

    void ShowHide(bool state)
    {
        center.gameObject.SetActive(state);
        knob.gameObject.SetActive(state);
    }
}