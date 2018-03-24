using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
[Serializable]
[PostProcess(typeof(PreDeathEffectRenderer), PostProcessEvent.AfterStack, "Custom/PreDeathEffect")]
public sealed class PreDeathEffect : PostProcessEffectSettings
{
    [Tooltip("DisplacementTexX.")]
    public TextureParameter DisplacementTex = new TextureParameter();
    [Tooltip("DisplacementTexY.")]
    public TextureParameter DisplacementTexy = new TextureParameter();
    [Tooltip("VeineTex.")]
    public TextureParameter VeineTex = new TextureParameter();
    [Tooltip("bwBlend.")]
    public FloatParameter bwBlend = new FloatParameter { value = 0f };
    [Tooltip("Zoom.")]
    public FloatParameter Zoom = new FloatParameter { value = 1f };
    [Tooltip("Speed.")]
    public FloatParameter Speed = new FloatParameter { value = 1f };
    [Range(0f, 1f), Tooltip("Strenght.")]
    public FloatParameter Strenght = new FloatParameter { value = 1f };
    [Tooltip("CenterPoint.")]
    public Vector4Parameter CenterPoint = new Vector4Parameter { value = new Vector4(0.5f,0.5f,0.5f,0.5f) };
    [Tooltip("vignette_size.")]
    public FloatParameter vignette_size = new FloatParameter { value = 0f };
    [Range(0f, 1f), Tooltip("greyance.")]
    public FloatParameter greystrenght = new FloatParameter { value = 0f };
    [Tooltip("greyance.")]
    public FloatParameter greytougoum = new FloatParameter { value = 0f };
    [Range(0f, 1f), Tooltip("noircissement.")]
    public FloatParameter noircissement = new FloatParameter { value = 0f };
}
 
public sealed class PreDeathEffectRenderer : PostProcessEffectRenderer<PreDeathEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/PreDeathEffect"));
        if (settings.DisplacementTex.value)
            sheet.properties.SetTexture("_DisplacementTex", settings.DisplacementTex);
        if (settings.DisplacementTexy.value)
            sheet.properties.SetTexture("_DisplacementTexy", settings.DisplacementTexy);
        if (settings.VeineTex.value)
            sheet.properties.SetTexture("_VeineTex", settings.VeineTex);
        sheet.properties.SetFloat("_bwBlend", settings.bwBlend);
        sheet.properties.SetFloat("_Zoom", settings.Zoom);
        sheet.properties.SetFloat("_Speed", settings.Speed);
        sheet.properties.SetFloat("_Strength", settings.Strenght);
        sheet.properties.SetVector("_CenterPoint", settings.CenterPoint);
        sheet.properties.SetFloat("_vignette_size", settings.vignette_size);
        sheet.properties.SetFloat("_greystrenght", settings.greystrenght);
        sheet.properties.SetFloat("_greytougoum", settings.greytougoum);
        sheet.properties.SetFloat("_noircissement", settings.noircissement);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}