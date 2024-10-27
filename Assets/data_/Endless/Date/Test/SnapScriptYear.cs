using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class SnapScriptYear : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Transform _screensContainer_year;

    int _screens_year = 1;
    int _startingScreen = 1;

    public List<Vector3> _positions_year;
    public ScrollRect _scroll_rect_year;
    Vector3 _lerp_target;
    bool _lerp;

    public int _containerSize_year;

    public int FastSwipeThreshold = 100;

    public bool isYear;

    bool _startDrag = true;
    Vector3 _startPosition = new Vector3();
    int _currentScreen;

    // Use this for initialization
    void Start()
    {
        DistributePages();

        _screens_year = _screensContainer_year.childCount;

        _lerp = false;

        this._positions_year = new List<Vector3>();

        if (_screens_year > 0)
        {
            Vector3 pos = new Vector3(0, 0, 0);

            for (int i = 0; i < _screensContainer_year.childCount; i++) {
                _positions_year.Add(pos);
                pos.y += 270;
            }

            //    for (int i = 0; i < _screens_year; ++i)
            //    {
            //        _scroll_rect_year.verticalNormalizedPosition = (float)i / (float)(_screens_year - 1);
            //        _positions_year.Add(_screensContainer_year.localPosition);
            //    }
        }

        this._scroll_rect_year.verticalNormalizedPosition = 1;

        _containerSize_year = (int)gameObject.GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().offsetMax.y;

    }

    public void CallStart() {
        this.Start();
    }

    void Update()
    {
        if (_lerp)
        {
            _screensContainer_year.localPosition = Vector3.Lerp(_screensContainer_year.localPosition, _lerp_target, 7.5f * Time.deltaTime);
            if (Vector3.Distance(_screensContainer_year.localPosition, _lerp_target) < 0.005f)
            {
                _lerp = false;
                if (isYear) {
                    Date.instance.UpdateYear();
                }
            }
        }

    }


    //find the closest registered point to the releasing point
    private Vector3 FindClosestFrom(Vector3 start, System.Collections.Generic.List<Vector3> positions)
    {
        Vector3 closest = Vector3.zero;
        float distance = Mathf.Infinity;

        foreach (Vector3 position in _positions_year)
        {
            if (Vector3.Distance(start, position) < distance)
            {
                distance = Vector3.Distance(start, position);
                closest = position;
            }
        }

        return closest;
    }


    //returns the current screen that the is seeing
    public int CurrentScreen()
    {
        float absPoz = Math.Abs(_screensContainer_year.gameObject.GetComponent<RectTransform>().offsetMin.y);

        absPoz = Mathf.Clamp(absPoz, 1, _containerSize_year - 1);

        float calc = (absPoz / _containerSize_year) * _screens_year;

        return (int)calc;
    }

    //used for changing between screen resolutions
    private void DistributePages()
    {
        int _offset = 0;
        int _step = Screen.height;
        int _dimension = 0;

        int currentYPosition = 0;

        for (int i = 0; i < _screensContainer_year.transform.childCount; i++)
        {
            RectTransform child = _screensContainer_year.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            currentYPosition = _offset + i * _step;
            child.anchoredPosition = new Vector2(0f, currentYPosition);
            child.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().sizeDelta.y);
        }

        _dimension = currentYPosition + _offset * -1;

        _screensContainer_year.GetComponent<RectTransform>().offsetMax = new Vector2(0f, _dimension);
    }

    #region Interfaces
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _screensContainer_year.localPosition;
        _currentScreen = CurrentScreen();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _startDrag = true;
        if (_scroll_rect_year.vertical)
        {
                _lerp = true;
                _lerp_target = FindClosestFrom(_screensContainer_year.localPosition, _positions_year);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _lerp = false;
        if (_startDrag)
        {
            OnBeginDrag(eventData);
            _startDrag = false;
        }
    }
    #endregion
}