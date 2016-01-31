///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Sprite Color Outline.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("Sprite Color FX/Color Outline")]
  public sealed class SpriteColorOutline : SpriteColorBase
  {
    /// <summary>
    /// Outline mode.
    /// </summary>
    public enum OutlineMode
    {
      /// <summary>
      /// One solid color.
      /// </summary>
      Normal,

      /// <summary>
      /// Two gradient color.
      /// </summary>
      Gradient,

      /// <summary>
      /// Texture.
      /// </summary>
      Texture,
    }

    /// <summary>
    /// Gradient direction.
    /// </summary>
    public enum GradientDirection
    {
      /// <summary>
      /// Top to bottom.
      /// </summary>
      Vertical,

      /// <summary>
      /// Right to left.
      /// </summary>
      Horizontal,
    }

    /// <summary>
    /// Outline mode.
    /// </summary>
    public OutlineMode Mode
    {
      get { return outlineMode; }
      set
      {
        if (outlineMode != value)
        {
          outlineMode = value;

          if (this.spriteRenderer != null)
            CreateMaterial();
        }
      }
    }

    /// <summary>
    /// Gradient direction.
    /// </summary>
    public GradientDirection Direction
    {
      get { return gradientDirection; }
      set
      {
        if (gradientDirection != value)
        {
          gradientDirection = value;

          if (this.spriteRenderer != null)
            CreateMaterial();
        }
      }
    }

    /// <summary>
    /// Outline size [0 - 0.03].
    /// </summary>
    public float outlineSize = 0.01f;

    /// <summary>
    /// Outline color (RGBA).
    /// </summary>
    public Color outlineColor = Color.white;

    /// <summary>
    /// Second outline color (RGBA).
    /// </summary>
    public Color outlineColor2 = Color.white;

    /// <summary>
    /// Gradient scale [-10 - 10]
    /// </summary>
    public float gradientScale = 1.0f;

    /// <summary>
    /// Gradient offset [0 - 5]
    /// </summary>
    public float gradientOffset = 0.0f;

    /// <summary>
    /// Texture for OutlineMode.Texture mode.
    /// </summary>
    public Texture outlineTexture;

    /// <summary>
    /// Outline texture UV coordinate params (U scale, V scale, U velocity, V velocity).
    /// </summary>
    public Vector4 outlineTextureUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Outline texture angle [0 - 360].
    /// </summary>
    public float outlineTextureUVAngle;

    [SerializeField]
    private OutlineMode outlineMode = OutlineMode.Normal;

    [SerializeField]
    private GradientDirection gradientDirection = GradientDirection.Vertical;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath
    {
      get
      {
        return string.Format("Shaders/Outline/{0}SpriteColorOutline{1}{2}{3}",
          LightMode != SpriteColorFX.LightMode.UnLit ? LightMode.ToString() + "/" : string.Empty,
          Mode == OutlineMode.Normal ? string.Empty : Mode.ToString(),
          Mode == OutlineMode.Gradient ? Direction.ToString() : string.Empty,
          LightMode == SpriteColorFX.LightMode.UnLit ? string.Empty : LightMode.ToString());
      }
    }

    protected override void UpdateShader()
    {
      spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderOutlineSizeParam, outlineSize);
      spriteRenderer.sharedMaterial.SetColor(SpriteColorHelper.ShaderOutlineColorParam, outlineColor);

      if (Mode == OutlineMode.Gradient)
      {
        spriteRenderer.sharedMaterial.SetColor(SpriteColorHelper.ShaderOutlineColor2Param, outlineColor2);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderGradientScaleParam, gradientScale);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderGradientOffsetParam, gradientOffset);
      }
      else if (Mode == OutlineMode.Texture)
      {
        spriteRenderer.sharedMaterial.SetTexture(SpriteColorHelper.ShaderOutlineTexParam, outlineTexture);
        spriteRenderer.sharedMaterial.SetVector(SpriteColorHelper.ShaderUVOutlineTexParamsParam, outlineTextureUVParams);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderUVOutlineTexAngleParam, outlineTextureUVAngle * Mathf.Deg2Rad);
      }
    }
  }
}
