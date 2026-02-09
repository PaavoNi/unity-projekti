using UnityEngine;

public class LensCameraFollowView : MonoBehaviour
{
    public Transform lensRoot;      // viittaa LensRootiin
    public Transform hmdCamera;     // viittaa XR Originin Cameraan
    public Vector3 localOffset = Vector3.zero;

    void LateUpdate()
    {
        if (lensRoot == null || hmdCamera == null) return;

        // Pidä kamera linssin kohdalla
        transform.position = lensRoot.TransformPoint(localOffset);

        // Ota rotaatio HMD:stä (ei linssistä)
        transform.rotation = hmdCamera.rotation;
    }
}
