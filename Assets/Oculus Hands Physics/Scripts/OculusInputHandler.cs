using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusInputHandler : MonoBehaviour
{
    public XRNode hand;

    InputDevice handController;

    // Variables para guardar los valores de los botones
    public bool primaryButtonPressed_YB;
    public bool secondaryButtonPressed_XA;
    public bool menuButtonPressed_Menu;

    public bool gripButtonPressed;
    public bool triggerButtonPressed;


    private bool primaryButtonWasPressed;
    private bool secondaryButtonWasPressed;
    private bool menuButtonWasPressed;

    private bool gripButtonWasPressed;
    private bool triggerButtonWasPressed;

    public bool primaryButtonJustPressed_YB;
    public bool secondaryButtonJustPressed_XA;
    public bool menuButtonJustPressed;

    public bool gripButtonJustPressed;
    public bool triggerButtonJustPressed;

    // Variables para el movimiento de los joysticks
    public Vector2 _joystick;

    List<InputDevice> leftHandDevices = new List<InputDevice>();
    List<InputDevice> rightHandDevices = new List<InputDevice>();

    // Variables para los valores de presión de los triggers
    public float triggerValue;
    public float gripValue;
    private void Start()
    {
        LoadDevices();
    }

    private void LoadDevices()
    {
        // Obtener dispositivos para la mano izquierda o derecha
        if (hand == XRNode.LeftHand)
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left, leftHandDevices);
            if (leftHandDevices.Count > 0)
            {
                handController = leftHandDevices[0];
            }
        }
        else
        if (hand == XRNode.RightHand)
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right, rightHandDevices);

            if (rightHandDevices.Count > 0)
            {
                handController = rightHandDevices[0];
            }
        }
    }
    void Update()
    {

        if (hand == XRNode.LeftHand || hand == XRNode.RightHand)
        {
            if (!handController.isValid)
            {
                LoadDevices(); // Volver a intentar la detección de dispositivos si son inválidos porque al inicio no lo carga
            }

            // Obtener estado de los botones y joysticks
            UpdateButtonStates();
            UpdateJoystickStates();
            UpdateTriggerValues();


            primaryButtonJustPressed_YB = primaryButtonPressed_YB && !primaryButtonWasPressed;
            secondaryButtonJustPressed_XA = secondaryButtonPressed_XA && !secondaryButtonWasPressed;
            secondaryButtonJustPressed_XA = secondaryButtonPressed_XA && !secondaryButtonWasPressed;
            menuButtonJustPressed = menuButtonPressed_Menu && !menuButtonWasPressed;

            gripButtonJustPressed = gripButtonPressed && !gripButtonWasPressed;
            triggerButtonJustPressed = triggerButtonPressed && !triggerButtonWasPressed;


            primaryButtonWasPressed = primaryButtonPressed_YB;
            secondaryButtonWasPressed = secondaryButtonPressed_XA;
            gripButtonWasPressed = gripButtonPressed;
            triggerButtonWasPressed = triggerButtonPressed;
            menuButtonWasPressed = menuButtonPressed_Menu;
        }



    }

    private void UpdateButtonStates()
    {
        handController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed_YB);
        handController.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonPressed_XA);
        handController.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonPressed);
        handController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonPressed);
        handController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed_Menu);
    }

    private void UpdateJoystickStates()
    {
        handController.TryGetFeatureValue(CommonUsages.primary2DAxis, out _joystick);
    }

    private void UpdateTriggerValues()
    {
        // Obtén el valor de presión de los triggers
        handController.TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
        handController.TryGetFeatureValue(CommonUsages.grip, out gripValue);
    }
}
