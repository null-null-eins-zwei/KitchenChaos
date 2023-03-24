using System;
using UnityEngine;
using ZZOT.KitchenChaos.Furniture;
using ZZOT.KitchenChaos.Interfaces;
using ZZOT.KitchenChaos.UserInputSystem;

namespace ZZOT.KitchenChaos.User
{
    public class Player : MonoBehaviour, IKitchenObjectParent
    {
        public static Player Instance { get; private set; }

        [SerializeField] private Transform _objectHandlePoint;

        private KitchenObject _kitchenObject;

        public event EventHandler<OnSelectedConterChangedEventArgs> OnSelectedConterChanged;

        public class OnSelectedConterChangedEventArgs : EventArgs
        {
            public BaseCounter selectedCounter;
        }

        [SerializeField] private float _playerSpeed = 7f;
        [SerializeField] private UserInput _input;
        [SerializeField] private LayerMask _countersLayerMask;

        private readonly float _rotateSpeed = 10f;

        private bool _isWalking = false;
        private Vector3 _lastInteractDirection;
        private BaseCounter _selectedCounter;

        private const float _playerSize = 0.5f;
        private const float _playerHeight = 2f;
        private const float _interactDistance = 2f;

        private void Start()
        {
            _input.OnInteractAction += input_OnInteractAction;
            _input.OnInteractAlternateAction += input_OnInteractAlternateAction;
        }


        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Player is not a Singleton: {this}.");
            }

            Instance = this;
        }

        private void input_OnInteractAction(object sender, EventArgs e)
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact(this);
            }
        }
        
        private void input_OnInteractAlternateAction(object sender, EventArgs e)
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.InteractAlternate(this);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            HandleMovement();
            HandleInteraction();
        }

        public bool IsWalking => _isWalking;

        private void HandleMovement()
        {
            Vector2 input = _input.GetMovementVectorNormalized();

            Vector3 moveDir = new(
                            x: input.x,
                            y: 0f,
                            z: input.y);

            float moveDistance = _playerSpeed * Time.deltaTime;

            var canMove = CanMove(moveDir, moveDistance);

            // we want to slide toward the wall in case if we hit it
            if (!canMove)
            {
                // Attempt only X dir
                Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

                var canMoveX = (moveDirX != Vector3.zero) && CanMove(moveDirX, moveDistance);
                var canMoveZ = (moveDirZ != Vector3.zero) && CanMove(moveDirZ, moveDistance);

                if (canMoveX)
                {
                    canMove = true;
                    moveDir = moveDirX;
                }
                else if (canMoveZ)
                {
                    canMove = true;
                    moveDir = moveDirZ;
                }
            }

            _isWalking = moveDir != Vector3.zero;

            if (canMove)
            {
                // deltaTime required because of variable framerate
                transform.position += moveDistance * moveDir;
            }


            Vector3 lookAt = Vector3.Slerp(
                                 transform.forward,
                                 moveDir,
                                 Time.deltaTime * _rotateSpeed); // this is strange, there is better ways to determine rotation speed

            //rotation = Quaternion.LookRotation(value);
            transform.forward = lookAt;
        }

        private void HandleInteraction()
        {
            Vector2 input = _input.GetMovementVectorNormalized();

            Vector3 moveDir = new(
                            x: input.x,
                            y: 0f,
                            z: input.y);

            if (moveDir != Vector3.zero)
            {
                _lastInteractDirection = moveDir;
            }


            var hit = Physics.Raycast(
                        origin: transform.position,
                        direction: _lastInteractDirection,
                        out var raycastHit,
                        maxDistance: _interactDistance,
                        layerMask: _countersLayerMask);

            if (hit)
            {
                var isCounter = raycastHit.transform.TryGetComponent(out BaseCounter counter);
                if (isCounter)
                {
                    if (counter != _selectedCounter)
                    {
                        SetSelectedCounter(counter);
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

        private bool CanMove(Vector3 moveDir, float moveDistance)
        {
            var hit = Physics.CapsuleCast(
                        point1: transform.position,
                        point2: transform.position + Vector3.up * _playerHeight,
                        radius: _playerSize,
                        direction: moveDir,
                        maxDistance: moveDistance);

            return !hit;
        }

        private void SetSelectedCounter(BaseCounter selected)
        {
            _selectedCounter = selected;
            OnSelectedConterChanged?.Invoke(
                this,
                new OnSelectedConterChangedEventArgs
                {
                    selectedCounter = selected,
                });
        }

        public Transform GetKitchenObjectFollowTransform() => _objectHandlePoint;

        public KitchenObject GetKitchenObject() => _kitchenObject;

        public bool HasKitchenObject() => GetKitchenObject() != null;

        public void ClearKitchenObject() => SetKitchenObject(null);

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }
    }
}