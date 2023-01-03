using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


public class DragAndDropController : MonoBehaviour
{
    [SerializeField] InputAction clickAction;
    [SerializeField] InputAction holdAction;
    [SerializeField] float drag = 0.1f;
    [SerializeField] float LeftBorder;
    [SerializeField] float RightBoarder;
    private Camera mainCamera;
    private Vector2 velocity = Vector2.zero;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        clickAction.Enable();
        holdAction.Enable();
        clickAction.performed += OnClick;
        GameManager.LevelEndEvent += OnLevelEnded;

    }

    private void OnLevelEnded(LevelResult level)
    {
        clickAction.Disable();
        holdAction.Disable();
    }

    private void OnDisable()
    {
        clickAction.performed -= OnClick;
        clickAction.Disable();
        holdAction.Disable();
        GameManager.LevelEndEvent -= OnLevelEnded;

    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(context.ReadValue<Vector2>());
        Collider2D hitCollider = Physics2D.GetRayIntersection(ray).collider;
        if(hitCollider != null && hitCollider.tag == "Player")
        {
            StartCoroutine(DragCoroutine(hitCollider.gameObject));
        }
    }

    private IEnumerator DragCoroutine(GameObject dragable)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        while (holdAction.ReadValue<TouchPhase>() != TouchPhase.Ended)
#elif UNITY_STANDALONE || UNITY_EDITOR
        while (holdAction.ReadValue<float>() != 0)
#endif
        {
            float horizontalPointerPos = mainCamera.ScreenToWorldPoint(clickAction.ReadValue<Vector2>()).x;
            horizontalPointerPos = Math.Clamp(horizontalPointerPos, LeftBorder, RightBoarder);
            Vector2 currentPos = dragable.transform.position;
            Vector2 targetPos = new Vector2(horizontalPointerPos, currentPos.y);
            dragable.transform.position = Vector2.SmoothDamp(dragable.transform.position, targetPos, ref velocity, drag);
            yield return null;
        }
    }
}
