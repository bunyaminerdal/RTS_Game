// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/InputBasics/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e1265742-e8a1-4ef0-a279-57d2ecc423cd"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1b455362-be95-4f73-b83b-29a3004461c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""59ffc102-269a-491b-94e3-9fba28d5035b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Selection"",
                    ""type"": ""Button"",
                    ""id"": ""e6ddb73f-9549-4a01-bd79-d116d98a89a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PauseAction"",
                    ""type"": ""Button"",
                    ""id"": ""97ba9c72-a97b-4c36-8221-1e9597fbb5d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MenuAction"",
                    ""type"": ""Button"",
                    ""id"": ""4ef4e33c-8281-4c2e-861a-7c3af1c866a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SelectionBox"",
                    ""type"": ""PassThrough"",
                    ""id"": ""aa091f07-dc8c-4203-a98e-ce09a8780c4a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""ZoomAction"",
                    ""type"": ""PassThrough"",
                    ""id"": ""00d3766d-0346-4e0f-8d2a-f936b250707f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""16c23b3a-bd8a-4415-bf40-0087302b0dd3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuickSaveAction"",
                    ""type"": ""Button"",
                    ""id"": ""2c517b7a-138f-4380-b532-b9d92fdb2390"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuickLoadAction"",
                    ""type"": ""Button"",
                    ""id"": ""a2201026-a3c4-4447-a195-9dd21ece4003"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""96763cd7-ff85-4f7f-93fa-d94b7bb40aa3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""2395c364-6cbb-4110-95f3-35a4ac86160f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""37232bd3-78af-4413-91f1-5e30258fdd3b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8d711879-cc60-4f1d-8449-5e5e92906e29"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""becf0949-21b4-4573-bfe1-066aea2def4f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a35f60c7-543d-4109-a18a-37bb2f332134"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""3d957eba-ddb8-41c3-87d1-5a59f2e22e53"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ac00dfb4-6dd1-4ee9-990c-67c44a81f7cc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a2c3f013-81a1-4c4a-9aba-2eccb54f037b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0c25b93f-e1be-4156-8c80-599fea89c40a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""caa04558-d04d-4478-bcdd-e51c0b87e079"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b7387b0f-51f0-4200-837a-1de9b828af1f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6e172dc-557f-432a-9b8a-346295a81860"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a415a5f-1b8e-4cd6-a50a-022ff9a8cf88"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e5535ff-3b8b-434d-bcec-26ef2c3aba83"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectionBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cda1e8a-545a-4ed7-b2a3-8060989b5b91"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e3d4a91-51e0-498f-a24c-4355d93e0fc8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d115c17-8597-4abd-9b9e-96bf169d69e2"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e29db291-8493-455a-afb1-e2801b4f0abd"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickSaveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e652fd1c-cdc0-4a78-9e03-a7e710174db8"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickLoadAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e66519c-94c6-4d88-bf76-244425d5dbbd"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Selection = m_Player.FindAction("Selection", throwIfNotFound: true);
        m_Player_PauseAction = m_Player.FindAction("PauseAction", throwIfNotFound: true);
        m_Player_MenuAction = m_Player.FindAction("MenuAction", throwIfNotFound: true);
        m_Player_SelectionBox = m_Player.FindAction("SelectionBox", throwIfNotFound: true);
        m_Player_ZoomAction = m_Player.FindAction("ZoomAction", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_QuickSaveAction = m_Player.FindAction("QuickSaveAction", throwIfNotFound: true);
        m_Player_QuickLoadAction = m_Player.FindAction("QuickLoadAction", throwIfNotFound: true);
        m_Player_Rotation = m_Player.FindAction("Rotation", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Selection;
    private readonly InputAction m_Player_PauseAction;
    private readonly InputAction m_Player_MenuAction;
    private readonly InputAction m_Player_SelectionBox;
    private readonly InputAction m_Player_ZoomAction;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_QuickSaveAction;
    private readonly InputAction m_Player_QuickLoadAction;
    private readonly InputAction m_Player_Rotation;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Selection => m_Wrapper.m_Player_Selection;
        public InputAction @PauseAction => m_Wrapper.m_Player_PauseAction;
        public InputAction @MenuAction => m_Wrapper.m_Player_MenuAction;
        public InputAction @SelectionBox => m_Wrapper.m_Player_SelectionBox;
        public InputAction @ZoomAction => m_Wrapper.m_Player_ZoomAction;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @QuickSaveAction => m_Wrapper.m_Player_QuickSaveAction;
        public InputAction @QuickLoadAction => m_Wrapper.m_Player_QuickLoadAction;
        public InputAction @Rotation => m_Wrapper.m_Player_Rotation;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Selection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                @Selection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                @Selection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                @PauseAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseAction;
                @PauseAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseAction;
                @PauseAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseAction;
                @MenuAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuAction;
                @MenuAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuAction;
                @MenuAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuAction;
                @SelectionBox.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionBox;
                @SelectionBox.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionBox;
                @SelectionBox.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectionBox;
                @ZoomAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomAction;
                @ZoomAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomAction;
                @ZoomAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomAction;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @QuickSaveAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSaveAction;
                @QuickSaveAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSaveAction;
                @QuickSaveAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSaveAction;
                @QuickLoadAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickLoadAction;
                @QuickLoadAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickLoadAction;
                @QuickLoadAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickLoadAction;
                @Rotation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Selection.started += instance.OnSelection;
                @Selection.performed += instance.OnSelection;
                @Selection.canceled += instance.OnSelection;
                @PauseAction.started += instance.OnPauseAction;
                @PauseAction.performed += instance.OnPauseAction;
                @PauseAction.canceled += instance.OnPauseAction;
                @MenuAction.started += instance.OnMenuAction;
                @MenuAction.performed += instance.OnMenuAction;
                @MenuAction.canceled += instance.OnMenuAction;
                @SelectionBox.started += instance.OnSelectionBox;
                @SelectionBox.performed += instance.OnSelectionBox;
                @SelectionBox.canceled += instance.OnSelectionBox;
                @ZoomAction.started += instance.OnZoomAction;
                @ZoomAction.performed += instance.OnZoomAction;
                @ZoomAction.canceled += instance.OnZoomAction;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @QuickSaveAction.started += instance.OnQuickSaveAction;
                @QuickSaveAction.performed += instance.OnQuickSaveAction;
                @QuickSaveAction.canceled += instance.OnQuickSaveAction;
                @QuickLoadAction.started += instance.OnQuickLoadAction;
                @QuickLoadAction.performed += instance.OnQuickLoadAction;
                @QuickLoadAction.canceled += instance.OnQuickLoadAction;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSelection(InputAction.CallbackContext context);
        void OnPauseAction(InputAction.CallbackContext context);
        void OnMenuAction(InputAction.CallbackContext context);
        void OnSelectionBox(InputAction.CallbackContext context);
        void OnZoomAction(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnQuickSaveAction(InputAction.CallbackContext context);
        void OnQuickLoadAction(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
    }
}
