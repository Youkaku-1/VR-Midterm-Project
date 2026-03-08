using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalReveal : MonoBehaviour
{
    public Light revealLight;
    public float fadeSpeed = 2f;
    private DecalProjector decalProjector;
    private float targetFade = 0f;

    void Start()
    {
        decalProjector = GetComponent<DecalProjector>();
        decalProjector.fadeFactor = 0f;
    }

    void Update()
    {
        if (revealLight == null || !revealLight.enabled || !revealLight.gameObject.activeSelf)
        {
            targetFade = 0f;
        }
        else if (IsLightHittingDecal())
        {
            targetFade = 1f;
        }
        else
        {
            targetFade = 0f;
        }

        decalProjector.fadeFactor = Mathf.MoveTowards(
            decalProjector.fadeFactor, 
            targetFade, 
            fadeSpeed * Time.deltaTime
        );
    }

    bool IsLightHittingDecal()
    {
        if (revealLight.type != LightType.Spot) return false;

        Vector3 directionToDecal = transform.position - revealLight.transform.position;
        float distanceToDecal = directionToDecal.magnitude;

        // Check if decal is within light range
        if (distanceToDecal > revealLight.range) return false;

        // Check if decal is within the spot angle
        float angle = Vector3.Angle(revealLight.transform.forward, directionToDecal);
        if (angle > revealLight.spotAngle / 2f) return false;

        // Check if anything is blocking the light
        RaycastHit hit;
        if (Physics.Raycast(revealLight.transform.position, directionToDecal.normalized, 
            out hit, distanceToDecal))
        {
            if (hit.transform != transform) return false;
        }

        return true;
    }
}