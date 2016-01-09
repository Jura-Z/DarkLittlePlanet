using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Projector))]
public class AnimatedProjector : MonoBehaviour
{
    public float fps = 30.0f;
    public Texture2D[] frames;

    public Material projMaterial;
    public Transform player;
    public float sonarTime = 10.0f;

    public Color sonarColor = Color.red;
    [Range(0.0f, 1.0f)]
    public float tintCaustic = 0.3f;

    private int frameIndex;
    private Projector projector;

    void Start()
    {
        projector = GetComponent<Projector>();
        NextFrame();
        InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
    }

    void NextFrame()
    {
        projector.material.SetTexture("_MainTex", frames[frameIndex]);
        projector.material.SetTexture("_ShadowTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
    }

    void Update()
    {
        projMaterial.SetVector("_PlayerWorld", player.transform.position);
        projMaterial.SetFloat("_SonarTime", sonarTime);
        projMaterial.SetColor("_SonarColor", sonarColor);
        projMaterial.SetFloat("_CausticTint", tintCaustic);
    }
}