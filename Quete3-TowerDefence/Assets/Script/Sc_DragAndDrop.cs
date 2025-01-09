using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sc_DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _tower;
    [SerializeField] private LayerMask _tileLayer;

    private float _radius = 0.5f;

    private GameObject _currentPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _currentPos = Instantiate(_tower);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        _currentPos.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Collider2D[] HitCollidersReviewMax = Physics2D.OverlapCircleAll(_currentPos.transform.position, _radius, _tileLayer);

        Debug.Log(HitCollidersReviewMax.Length);
        if (HitCollidersReviewMax.Length >= 2)
        {
            Destroy(_currentPos);
        }
    }
}