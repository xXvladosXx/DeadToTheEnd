using UnityEngine;
using UnityEngine.Rendering.Universal;

#if KRIPTO_FX_LWRP_RENDERING
using UnityEngine.Rendering.LWRP;
#endif

[ExecuteInEditMode]
public class RFX4_Decal : MonoBehaviour
{
    public bool IsScreenSpace = true;

    // Material mat;
    ParticleSystem ps;
    ParticleSystem.MainModule psMain;
    private MaterialPropertyBlock props;
    MeshRenderer rend;
    private bool startDepthTex;

    private void OnEnable()
    {
        //if (Application.isPlaying) mat = GetComponent<Renderer>().material;
        //else mat = GetComponent<Renderer>().sharedMaterial;

        ps = GetComponent<ParticleSystem>();
        if (ps != null) psMain = ps.main;

        var cam = Camera.main;
        if (cam != null)
        {
            var addCamData = cam.GetComponent<UniversalAdditionalCameraData>();
            if (addCamData != null)
            {
                startDepthTex = addCamData.requiresDepthTexture;
                addCamData.requiresDepthTexture = IsScreenSpace;
            }
        }

        if (!IsScreenSpace)
        {
            var sharedMaterial = GetComponent<Renderer>().sharedMaterial;
            sharedMaterial.EnableKeyword("USE_QUAD_DECAL");
            sharedMaterial.SetInt("_ZTest1", (int)UnityEngine.Rendering.CompareFunction.LessEqual);
            if (Application.isPlaying)
            {
                var pos = transform.localPosition;
                pos.z += 0.1f;
                transform.localPosition = pos;
                var scale = transform.localScale;
                scale.y = 0.001f;
                transform.localScale = scale;
            }
        }
        else
        {
            var sharedMaterial = GetComponent<Renderer>().sharedMaterial;
            sharedMaterial.DisableKeyword("USE_QUAD_DECAL");
            sharedMaterial.SetInt("_ZTest1", (int)UnityEngine.Rendering.CompareFunction.Greater);
        }
    }

    void OnDisable()
    {
        var cam = Camera.main;
        if (cam == null) return;
        var addCamData                                          = cam.GetComponent<UniversalAdditionalCameraData>();
        if (addCamData != null) addCamData.requiresDepthTexture = startDepthTex;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = Matrix4x4.TRS(this.transform.TransformPoint(Vector3.zero), this.transform.rotation, this.transform.lossyScale);
        Gizmos.color = new Color(1, 1, 1, 1);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
