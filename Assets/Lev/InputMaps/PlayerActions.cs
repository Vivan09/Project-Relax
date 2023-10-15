//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Lev/InputMaps/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""SoftTetrisTouch"",
            ""id"": ""e086d3f4-2861-4ec9-adf1-01cc8ed3dee5"",
            ""actions"": [
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""48dc4916-ca50-48c8-9bf1-bf6cfb82c853"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""790cc7d0-89db-4e83-ba83-0dc7701f173c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c674c7d3-1d03-4dd6-b7a2-0d1443adf12a"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9a07d05-1a21-460f-b457-139829df5263"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fffae86c-f276-4404-9e98-76abe3d98a3c"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // SoftTetrisTouch
        m_SoftTetrisTouch = asset.FindActionMap("SoftTetrisTouch", throwIfNotFound: true);
        m_SoftTetrisTouch_PrimaryContact = m_SoftTetrisTouch.FindAction("PrimaryContact", throwIfNotFound: true);
        m_SoftTetrisTouch_PrimaryPosition = m_SoftTetrisTouch.FindAction("PrimaryPosition", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // SoftTetrisTouch
    private readonly InputActionMap m_SoftTetrisTouch;
    private List<ISoftTetrisTouchActions> m_SoftTetrisTouchActionsCallbackInterfaces = new List<ISoftTetrisTouchActions>();
    private readonly InputAction m_SoftTetrisTouch_PrimaryContact;
    private readonly InputAction m_SoftTetrisTouch_PrimaryPosition;
    public struct SoftTetrisTouchActions
    {
        private @PlayerActions m_Wrapper;
        public SoftTetrisTouchActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryContact => m_Wrapper.m_SoftTetrisTouch_PrimaryContact;
        public InputAction @PrimaryPosition => m_Wrapper.m_SoftTetrisTouch_PrimaryPosition;
        public InputActionMap Get() { return m_Wrapper.m_SoftTetrisTouch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SoftTetrisTouchActions set) { return set.Get(); }
        public void AddCallbacks(ISoftTetrisTouchActions instance)
        {
            if (instance == null || m_Wrapper.m_SoftTetrisTouchActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SoftTetrisTouchActionsCallbackInterfaces.Add(instance);
            @PrimaryContact.started += instance.OnPrimaryContact;
            @PrimaryContact.performed += instance.OnPrimaryContact;
            @PrimaryContact.canceled += instance.OnPrimaryContact;
            @PrimaryPosition.started += instance.OnPrimaryPosition;
            @PrimaryPosition.performed += instance.OnPrimaryPosition;
            @PrimaryPosition.canceled += instance.OnPrimaryPosition;
        }

        private void UnregisterCallbacks(ISoftTetrisTouchActions instance)
        {
            @PrimaryContact.started -= instance.OnPrimaryContact;
            @PrimaryContact.performed -= instance.OnPrimaryContact;
            @PrimaryContact.canceled -= instance.OnPrimaryContact;
            @PrimaryPosition.started -= instance.OnPrimaryPosition;
            @PrimaryPosition.performed -= instance.OnPrimaryPosition;
            @PrimaryPosition.canceled -= instance.OnPrimaryPosition;
        }

        public void RemoveCallbacks(ISoftTetrisTouchActions instance)
        {
            if (m_Wrapper.m_SoftTetrisTouchActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISoftTetrisTouchActions instance)
        {
            foreach (var item in m_Wrapper.m_SoftTetrisTouchActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SoftTetrisTouchActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SoftTetrisTouchActions @SoftTetrisTouch => new SoftTetrisTouchActions(this);
    public interface ISoftTetrisTouchActions
    {
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
    }
}