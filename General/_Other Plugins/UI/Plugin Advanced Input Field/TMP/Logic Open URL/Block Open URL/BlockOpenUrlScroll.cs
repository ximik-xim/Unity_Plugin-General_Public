using AdvancedInputFieldPlugin;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Блокирует переход по ссылке пока идет скрол
/// </summary>
public class BlockOpenUrlScroll : MonoBehaviour
{
    [SerializeField]
    private ScrollArea _scrollArea;
    [SerializeField]
    private LogicOpenUrlTextTPM _openUrlTextTpm;
    private void Awake()
    {
        _scrollArea.OnPointerUpHandler += OnPointerUpHandler;
        _scrollArea.OnBeginDragHandler += OnBeginDragHandler;
    }

    private void OnBeginDragHandler(PointerEventData obj)
    {
        _openUrlTextTpm.SetStatusBlockLinc(true);
    }
    
    private void OnPointerUpHandler(PointerEventData obj)
    {
        _openUrlTextTpm.OnPointerClickHandler -= OnPointerClickHandler;
        _openUrlTextTpm.OnPointerClickHandler += OnPointerClickHandler;
    }

    private void OnPointerClickHandler(PointerEventData obj)
    {
        _openUrlTextTpm.SetStatusBlockLinc(false);
    }

    private void OnDestroy()
    {
        _scrollArea.OnPointerUpHandler -= OnPointerUpHandler;
        _scrollArea.OnBeginDragHandler -= OnBeginDragHandler;
    }
}
