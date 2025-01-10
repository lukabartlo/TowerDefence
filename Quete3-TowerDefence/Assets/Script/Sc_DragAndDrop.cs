using UnityEngine;
using UnityEngine.EventSystems;

public class Sc_DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _tower;
    [SerializeField] private LayerMask _tileLayer;
    [SerializeField] private Sc_PlayerStats _playerStats;

    private float _radius = 0.5f;

    private GameObject _currentPos;

    private bool _isDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = false;
        if (_playerStats.SpendPlayerCash(_tower.GetComponent<Sc_DefenceStats>().price))
        {
            _currentPos = Instantiate(_tower);
            _isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_currentPos == null || !_isDragging)
            return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        _currentPos.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentPos == null || !_isDragging)
            return;

        Collider2D[] HitCollidersReviewMax = Physics2D.OverlapCircleAll(_currentPos.transform.position, _radius, _tileLayer);
        if (HitCollidersReviewMax.Length >= 2)
        {
            Destroy(_currentPos);
            _playerStats.IncreasePlayerCash(_currentPos.GetComponent<Sc_DefenceStats>().price);
        }
        _currentPos.GetComponent<Sc_DefenceAttack>().SetIsPlaced(true);
    }
}