using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] InputAction clickAction;
    [SerializeField] InputAction holdAction;
    [SerializeField] float drag = 0.1f;
    private Camera mainCamera;
    private Vector2 velocity = Vector2.zero;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnEnable()
    {
        clickAction.Enable();
        holdAction.Enable();
        clickAction.performed += OnClick;
    }

    public void OnDisable()
    {
        clickAction.performed -= OnClick;
        clickAction.Disable();
        holdAction.Disable();
    }
    public void Update()
    {
        Debug.Log(clickAction.ReadValue<Vector2>());
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
        while (holdAction.ReadValue<TouchPhase>() != TouchPhase.Ended)
        {
            float horizontalPointerPos = mainCamera.ScreenToWorldPoint(clickAction.ReadValue<Vector2>()).x;
            Vector2 currentPos = dragable.transform.position;
            Vector2 targetPos = new Vector2(horizontalPointerPos, currentPos.y);
            dragable.transform.position = Vector2.SmoothDamp(dragable.transform.position, targetPos, ref velocity, drag);
            yield return null;
        }
    }
}
