using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRBaseInteractable))]
public class PokeToggleInteractable : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Degrees to rotate on the X axis when first poked.")]
    public float rotationAmount = 14f;

    [Header("Objects To Disable On Poke")]
    [Tooltip("These GameObjects will be disabled on the first poke and re-enabled on the second.")]
    public List<GameObject> objectsToDisable = new List<GameObject>();

    [Header("Objects To Enable On Poke")]
    [Tooltip("These GameObjects will be enabled on the first poke and disabled again on the second.")]
    public List<GameObject> objectsToEnable = new List<GameObject>();

    // ── private state ──────────────────────────────────────────────────────────
    private XRBaseInteractable _interactable;
    private Quaternion _originalRotation;
    private Quaternion _pokedRotation;
    private bool _isPokedState = false;

    // ── Unity lifecycle ────────────────────────────────────────────────────────
    private void Awake()
    {
        _interactable     = GetComponent<XRBaseInteractable>();
        _originalRotation = transform.localRotation;
        _pokedRotation    = Quaternion.Euler(rotationAmount, 0f, 0f) * _originalRotation;
    }

    private void OnEnable()
    {
        _interactable.selectEntered.AddListener(OnPoked);
    }

    private void OnDisable()
    {
        _interactable.selectEntered.RemoveListener(OnPoked);
    }

    // ── Poke handler ───────────────────────────────────────────────────────────
    private void OnPoked(SelectEnterEventArgs args)
    {
        // Only react to a poke interactor — remove this guard to allow any interactor
        if (args.interactorObject is not UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)
            return;

        _isPokedState = !_isPokedState;

        if (_isPokedState)
        {
            // ── First poke ──────────────────────────────────────────────────
            transform.localRotation = _pokedRotation;
            SetObjects(objectsToDisable, false);
            SetObjects(objectsToEnable, true);
        }
        else
        {
            // ── Second poke (reset) ─────────────────────────────────────────
            transform.localRotation = _originalRotation;
            SetObjects(objectsToDisable, true);
            SetObjects(objectsToEnable, false);
        }
    }

    // ── Helpers ────────────────────────────────────────────────────────────────
    private void SetObjects(List<GameObject> objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }

    /// <summary>
    /// Force a reset back to the default state from code without needing a poke.
    /// </summary>
    public void ForceReset()
    {
        _isPokedState           = false;
        transform.localRotation = _originalRotation;
        SetObjects(objectsToDisable, true);
        SetObjects(objectsToEnable, false);
    }
}