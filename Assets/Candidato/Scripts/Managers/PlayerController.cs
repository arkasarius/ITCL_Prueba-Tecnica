using System;
using System.Collections;
using Candidato.Scripts.Input_Actions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Candidato.Scripts.Managers
{
 public class PlayerController : MonoBehaviour
 {
  private PlayerInput _playerInput;
  private Coroutine _zoomCoroutine;
  private Vector3 _target;
  private Movement _movement;
  private bool _pointerOverUI;
  private AnimationState _animationState;
  private Vector3 _lastPos;
  
  public Camera mainCamera;
  public GameObject cameraAxis;

  private InputAction _touch;
  private InputAction _pinch;

  private Vector2 _initialTouchPosition;
  private bool _holdTouch;
  private DateTime _deltaTouch;

  private Coroutine _zoomCorrutine;
  private bool _zoomMobile;

  
  /// <summary>
  ///   <para>Sets all events for the player Input</para>
  /// </summary>
  private void OnEnable()
  {
   _touch = _playerInput.Player.Touch;
   _pinch = _playerInput.Player.Pinch;
  
   _touch.Enable();
   _pinch.Enable();
   _playerInput.Player.PrimaryFingerPosition.Enable();
   _playerInput.Player.SecondaryFingerPosition.Enable();
   _playerInput.Player.SecondaryFingerTap.Enable();
  
   _touch.performed += OnTouch;
   _touch.canceled += OnReleaseTouch;
   _pinch.performed += Pinch;

   _playerInput.Player.SecondaryFingerTap.performed += MobileZoom;
   _playerInput.Player.SecondaryFingerTap.canceled += MobileZoomCancel;
   

  }

  /// <summary>
  ///   <para>Unsubscribes from events when disabled</para>
  /// </summary>
  private void OnDisable()
  {
   _touch.Disable();
   _pinch.Disable();
   _playerInput.Player.PrimaryFingerPosition.Disable();
   _playerInput.Player.SecondaryFingerPosition.Disable();
  }

  private void Awake()
  {
   _playerInput = new PlayerInput();
   _movement=transform.GetComponent<Movement>();
   _animationState=transform.GetComponent<AnimationState>();
   }
 
  /// <summary>
  ///   <para>Checks if we are holding the Input Action for Touch over 150 milliseconds. If we are holding over 150 milliseconds it consider the input as a sweep.</para>
  /// </summary>
  /// <param name="context">New state. GameplayState can be Idle or Moving</param>
  private void OnTouch(InputAction.CallbackContext context)
  {
   if (!_holdTouch)
   {
    _holdTouch = true;
    _deltaTouch = DateTime.Now;
    _initialTouchPosition = context.ReadValue<Vector2>();
    return;
   }
   if (DateTime.Now - _deltaTouch > TimeSpan.FromMilliseconds(150)) 
    Swipe(context);
  }
 
  /// <summary>
  ///   <para>Handles Touch release. if the release is before 150 milliseconds calls Touch callback.</para>
  /// </summary>
  private void OnReleaseTouch(InputAction.CallbackContext context)
  {
   _holdTouch = false;
   if (DateTime.Now - _deltaTouch < TimeSpan.FromMilliseconds(150))
    Touch(_initialTouchPosition);
  }
 
  /// <summary>
  ///   <para>sets world location of screen touch over a Ray cast. Checks if we are ray casting against an UI item.</para>
  /// </summary>
  private void Touch(Vector2 context)
  {
   if (Time.timeScale==0) return;
   var ray = mainCamera.ScreenPointToRay(context);
   //si no hay un objetivo de raycast anulamos touch.
   if (!Physics.Raycast(ray, out var hit)) return;
   //si el cursor esta en la interfaz anulamos touch.
   if (_pointerOverUI) return;
   _target = hit.point;
   _movement.SetTargetPlayer(_target);
  }
  
  /// <summary>
  ///   <para>Handles camera rotation over 180 degrees Euler. Disables itself if two Touchs are present for clarity.</para>
  /// </summary>
  private void Swipe(InputAction.CallbackContext context)
  {
   if (_zoomMobile) return;
   var deltaPosition = context.ReadValue<Vector2>() - _initialTouchPosition;
  
   //rotación del eje de camara de 180 grados para un swipe de pantalla completa.
   cameraAxis.transform.eulerAngles += new Vector3(0f, deltaPosition.x/Screen.width*180, 0f);
   _initialTouchPosition = context.ReadValue<Vector2>();
  }
 
  /// <summary>
  ///   <para>Sets the Fov of the camera for pc </para>
  /// </summary>
  private void Pinch(InputAction.CallbackContext context)
  {
   var zoomInput = context.ReadValue<float>();
   var newFOV = mainCamera.fieldOfView - zoomInput * 5 * Time.deltaTime; 
   newFOV = Mathf.Clamp(newFOV, 40f, 80f);
   mainCamera.fieldOfView = newFOV;
  }
 
  /// <summary>
  ///   <para>Updates the Animation for the player.</para>
  /// </summary>
 private void Update()
  {
   _pointerOverUI = EventSystem.current.IsPointerOverGameObject();
   _animationState.SetState(Vector3.Distance(transform.position, _lastPos) >= 0.01f
    ? GameplayState.Moving
    : GameplayState.Idle);
   _lastPos = transform.position;
  }

  /// <summary>
  ///   <para>starts Corrutine for Mobile Zoom logic</para>
  /// </summary>
  private void MobileZoom(InputAction.CallbackContext context)
  {
   _zoomCorrutine=StartCoroutine(MobileZoom());
  }
  
  /// <summary>
  ///   <para>Sets the Zoom on mobile over Touch[0] and Touch[1] delta magnitudes.</para>
  /// </summary>
  private IEnumerator MobileZoom()
  {
   var startLeftPos = _playerInput.Player.PrimaryFingerPosition.ReadValue<Vector2>();
   var startRightPos = _playerInput.Player.SecondaryFingerPosition.ReadValue<Vector2>();
   _zoomMobile = true;
   while (true)
   {
    var leftPos = _playerInput.Player.PrimaryFingerPosition.ReadValue<Vector2>();
    var rightPos = _playerInput.Player.SecondaryFingerPosition.ReadValue<Vector2>();
    var deltaLeft = leftPos - startLeftPos;
    var deltaRight = rightPos - startRightPos;
    var previousMagnitude = (deltaLeft - deltaRight).magnitude;
    var currentMagnitude = (startLeftPos - startRightPos).magnitude;
    var difference = currentMagnitude - previousMagnitude;
    //zoom lerp segun distancia entre la posición entre Touch0 y Touch1
    var newFOV = Mathf.Lerp( 40f, 80f,difference/1080);
    mainCamera.fieldOfView = newFOV;

    startLeftPos = leftPos;
    startRightPos = rightPos;
    
    yield return new WaitForEndOfFrame();
   }
   // ReSharper disable once IteratorNeverReturns
  }

  /// <summary>
  ///   <para>Stops Mobile Zoom Coroutine.</para>
  /// </summary>
  private void MobileZoomCancel(InputAction.CallbackContext context)
  {
   StopCoroutine(_zoomCorrutine);
   _zoomMobile = false;
  }
 }
}
