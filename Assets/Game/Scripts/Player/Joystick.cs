using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region Variables

    [SerializeField] private float _handlerRange = 1;
    [SerializeField] private float _deadZone = 0;
    [SerializeField] private bool _snapX;
    [SerializeField] private bool _snapY;
    [SerializeField] protected RectTransform _background = null;
    [SerializeField] private RectTransform _handle = null;
    [SerializeField] private AxisOptions _axisOptions = AxisOptions.Both;
    [SerializeField] private Vector2 _input = Vector2.zero;

    private Canvas _canvas;
    private Camera _cam;
    private RectTransform _baseRect = null;

    #endregion

    #region Properties

    public float HandlerRange
    {
        get { return _handlerRange; }
        set { _handlerRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return _deadZone; }
        set { _deadZone = Mathf.Abs(value); }
    }

    public bool SnapX
    {
        get { return _snapX; }
        set { _snapX = value; }
    }

    public bool SnapY
    {
        get { return _snapY; }
        set { _snapY = value; }
    }

    public AxisOptions AxisOption
    {
        get { return _axisOptions; }
        set { _axisOptions = value; }
    }

    public float Horizontal
    {
        get { return (SnapX) ? SnapFloat(_input.x, AxisOptions.Horizontal) : _input.x; }
    }

    public float Vertical
    {
        get { return (SnapY) ? SnapFloat(_input.y, AxisOptions.Vertical) : _input.y; }
    }

    public Vector2 Direction
    {
        get { return new Vector2(Horizontal, Vertical); }
    }

    #endregion

    #region Functions and Methods

    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
        {
            return 0;
        }

        if (_axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(_input, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                {
                    return 0;
                }
                else
                {
                    return (value > 0) ? 1 : -1;
                }
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f && angle < 112.5f)
                {
                    return 0;
                }
                else
                {
                    return (value > 0) ? 1 : -1;
                }
            }
            else
            {
                if (value < 0)
                {
                    return 1;
                }

                if (value > 0)
                {
                    return -1;
                }
            }
        }

        return 0;
    }

    private void FormatInput()
    {
        if (AxisOption == AxisOptions.Horizontal)
        {
            _input = new Vector2(_input.x, 0f);
        }
        else if (AxisOption == AxisOptions.Vertical)
        {
            _input = new Vector2(0f, _input.y);
        }
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalise, Vector2 radius, Camera cam)
    {
        if (magnitude > DeadZone)
        {
            if (magnitude > 1)
            {
                _input = normalise;
            }
        }
        else
        {
            _input = Vector2.zero;
        }
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _cam, out localPoint))
        {
            return localPoint - (_background.anchorMax * _baseRect.sizeDelta);
        }

        return Vector2.zero;
    }

    #endregion

    #region Interface

    public void OnDrag(PointerEventData eventData)
    {
        _cam = null;
        if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            _cam = _canvas.worldCamera;
        }

        Vector2 pos = RectTransformUtility.WorldToScreenPoint(_cam, _background.position);
        Vector2 radius = _background.sizeDelta / 2;

        _input = (eventData.position - pos) / (radius * _canvas.scaleFactor);

        FormatInput();
        HandleInput(_input.magnitude, _input.normalized, radius, _cam);

        _handle.anchoredPosition = _input * radius * _handlerRange;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }

    #endregion

    protected virtual void Start()
    {
        //HandlerRange = handlerRange;
        //DeadZone = deadZone;
        _baseRect = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();

        if (_canvas == null)
        {
            Debug.LogError("The Script isn't placed inside the canvas parent.");
        }

        Vector2 center = new Vector2(.5f, .5f);
        _background.pivot = center;
        _handle.anchorMin = center;
        _handle.anchorMax = center;
        _handle.pivot = center;
        _handle.anchoredPosition = Vector2.zero;
    }
}

public enum AxisOptions
{
    Both,
    Horizontal,
    Vertical
}