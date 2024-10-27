using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


//[RequireComponent(typeof(ScrollRect))]
//[AddComponentMenu("UI/Extensions/Horizontal Scroll Snap")]
public class SnapScriptMonth : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Transform _screensContainer_month;

    int _screens_month = 1;
    int _startingScreen_month = 1;

    public List<Vector3> _positions_month;
    public ScrollRect _scroll_rect_month;
    Vector3 _lerp_target_month;
    bool _lerp;

    public int _containerSize_month;

    public int FastSwipeThreshold = 100;

    bool _startDrag = true;
    Vector3 _startPosition_month = new Vector3();
    public int _currentScreen_month;

    // Use this for initialization
    void Start()
    {
        DistributePages();

        _screens_month = _screensContainer_month.childCount;

        _lerp = false;

        _positions_month = new List<Vector3>();
 
        if (_screens_month > 0)
        {
            Vector3 pos = new Vector3(0, 0, 0);

            for (int i = 0; i < _screensContainer_month.childCount; i++)
            {
                _positions_month.Add(pos);
                pos.y += 270;
            }

            //for (int i = 0; i < _screens_month; ++i)
            //{
            //    _scroll_rect_month.verticalNormalizedPosition = (float)i / (float)(_screens_month - 1);
            //    _positions_month.Add(_screensContainer_month.localPosition);
            //}
        }

        
        this._scroll_rect_month.verticalNormalizedPosition = 1;

        this._containerSize_month = (int)gameObject.GetComponent<ScrollRect>().content.gameObject.gameObject.GetComponent<RectTransform>().offsetMax.y;

    }

    void Update()
    {
        if (_lerp)
        {
            this._screensContainer_month.localPosition = Vector3.Lerp(_screensContainer_month.localPosition, _lerp_target_month, 7.5f * Time.deltaTime);
            if (Vector3.Distance(this._screensContainer_month.localPosition, _lerp_target_month) < 0.005f)
            {
                _lerp = false;
                Date.instance.UpdateMonth();
            }
        }
    }


    //find the closest registered point to the releasing point
    private Vector3 FindClosestFrom(Vector3 start, List<Vector3> positions)
    {
        Vector3 closest = Vector3.zero;
        float distance = Mathf.Infinity;

        foreach (Vector3 position in _positions_month)
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
        float absPoz = Math.Abs(_screensContainer_month.gameObject.GetComponent<RectTransform>().offsetMin.y);

        absPoz = Mathf.Clamp(absPoz, 1, _containerSize_month - 1);

        float calc = (absPoz / _containerSize_month) * _screens_month;

        return (int)calc;
    }


    //used for changing between screen resolutions
    private void DistributePages()
    {
        int _offset = 0;
        int _step = Screen.height;
        int _dimension = 0;

        int currentYPosition = 0;

        for (int i = 0; i < _screensContainer_month.transform.childCount; i++)
        {
            RectTransform child = _screensContainer_month.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            currentYPosition = _offset + i * _step;
            child.anchoredPosition = new Vector2(0f, currentYPosition);
            child.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().sizeDelta.y);
        }

        _dimension = currentYPosition + _offset * -1;

        _screensContainer_month.GetComponent<RectTransform>().offsetMax = new Vector2(0f, _dimension);
    }

    #region Interfaces
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition_month = _screensContainer_month.localPosition;
        _currentScreen_month = CurrentScreen();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _startDrag = true;
            if (_scroll_rect_month.vertical)
            {
                    _lerp = true;
                    _lerp_target_month = this.FindClosestFrom(this._screensContainer_month.localPosition, _positions_month);
                    print(this._screensContainer_month.localPosition);
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