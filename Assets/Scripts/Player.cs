using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //Singleton Pattern Player
    public static Player Instance { get; private set; }


    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public ClearCounter selectedCounterArgs;
    }


    [SerializeField] private float movSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;


    private bool isWalking;
    private Vector3 lastInteraction;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Tem mais de uma instância de Jogador");
        }
        Instance = this;
    }


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sende, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (movDir != Vector3.zero)
        {
            lastInteraction = movDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);

            }
        }
        else
        {
            SetSelectedCounter(null);
        }

    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = movSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(movDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                movDir = moveDirX;
            }
            else
            {
                Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirZ, moveDistance);

                if (canMove)
                {
                    movDir = movDirZ;
                }
                else
                {
                    //Não se move em direção nenhuma
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDistance * movDir;
        }

        isWalking = movDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            selectedCounterArgs = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
