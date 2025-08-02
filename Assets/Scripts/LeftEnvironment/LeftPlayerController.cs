using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftPlayerController : MonoBehaviour
{
    private LeftPlayer _leftPlayerInput;

    private void Start()
    {
        _leftPlayerInput = null;
        if (InputManager.Instance == null)
        {
            _leftPlayerInput = new LeftPlayer();
            _leftPlayerInput.Enable();
        }
        else
            _leftPlayerInput = InputManager.Instance.LeftPlayerActions;

        _leftPlayerInput.Main.Select.performed += OnLeftPlayerSelect;
        _leftPlayerInput.Main.Add.performed += OnLeftPlayerAdd;
        _leftPlayerInput.Main.Clear.performed += OnLeftPlayerClear;
        UpdatePlayerMovementAsync(this.destroyCancellationToken).Forget();
    }

    private void OnLeftPlayerClear(InputAction.CallbackContext obj)
    {
        if (!obj.performed)
            return;
        GameManager.Instance.Equipment.Deselect();
    }

    private void OnLeftPlayerAdd(InputAction.CallbackContext obj)
    {
        if (!obj.performed)
            return;
        if (GameManager.Instance.Equipment.CanAddItem() && GameManager.Instance.ItemsQueue.CanDequeue())
        {
            var item = GameManager.Instance.ItemsQueue.Dequeue();
            GameManager.Instance.Equipment.AddItem(item);
        }
    }

    private void OnLeftPlayerSelect(InputAction.CallbackContext obj)
    {
        if (!obj.performed)
            return;

        var index = GameManager.Instance.Equipment.SelectIndex;
        if (GameManager.Instance.Equipment.IsIndexValid(index))
        {
            GameManager.Instance.Equipment.SwapItem();
            GameManager.Instance.Equipment.Deselect();
        }
        else
            GameManager.Instance.Equipment.SelectItem();
    }

    public async UniTaskVoid UpdatePlayerMovementAsync(CancellationToken cancellation)
    {
        while (!cancellation.IsCancellationRequested)
        {
            var value = _leftPlayerInput.Main.Movement.ReadValue<Vector2>();
            var intValue = new Vector2Int();
            intValue.x = Mathf.RoundToInt(value.x);
            intValue.y = Mathf.RoundToInt(value.y);
            GameManager.Instance.Equipment.SetFocusItem(intValue);

            await UniTask.WaitForSeconds(0.1f,cancellationToken:cancellation);
        }
    }
}
