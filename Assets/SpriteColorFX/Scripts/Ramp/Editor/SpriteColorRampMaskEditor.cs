///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorRampMask editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorRampMask))]
  public sealed class SpriteColorRampMaskEditor : SpriteColorBaseEditor
  {
    private SpriteColorRampMask effect;

    /// <summary>
    /// Set the default values.
    /// </summary>
    protected override void ResetDefaultValues()
    {
      if (effect == null)
        effect = this.target as SpriteColorRampMask;

      effect.strength = 1.0f;

      effect.gammaCorrect = 1.2f;

      effect.uvScroll = 0.0f;

      effect.invertLum = false;

      effect.luminanceRangeMin = 0.0f;

      effect.luminanceRangeMax = 1.0f;

      base.ResetDefaultValues();
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
      if (effect == null)
        effect = this.target as SpriteColorRampMask;

      EditorGUIUtility.fieldWidth = 40.0f;

      effect.palettes[0] = (SpriteColorRampPalettes)EditorGUILayout.EnumPopup(@"Palette 1 (Red)", effect.palettes[0]);

      effect.palettes[1] = (SpriteColorRampPalettes)EditorGUILayout.EnumPopup(@"Palette 2 (Green)", effect.palettes[1]);

      effect.palettes[2] = (SpriteColorRampPalettes)EditorGUILayout.EnumPopup(@"Palette 3 (Blue)", effect.palettes[2]);

      effect.strength = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(effect.strength * 100.0f), 0, 100, 100) * 0.01f;

      effect.gammaCorrect = SpriteColorFXEditorHelper.SliderWithReset(@"Gamma", SpriteColorFXEditorHelper.TooltipGamma, effect.gammaCorrect, 0.5f, 3.0f, 1.2f);

      effect.uvScroll = SpriteColorFXEditorHelper.SliderWithReset(@"UV Scroll", SpriteColorFXEditorHelper.TooltipUVScroll, effect.uvScroll, 0.0f, 1.0f, 0.0f);

      EditorGUIUtility.fieldWidth = 60.0f;

      SpriteColorFXEditorHelper.MinMaxSliderWithReset(@"Luminance range", SpriteColorFXEditorHelper.TooltipLuminanceRange, ref effect.luminanceRangeMin, ref effect.luminanceRangeMax, 0.0f, 1.0f, 0.0f, 1.0f);

      effect.invertLum = SpriteColorFXEditorHelper.ToogleWithReset(@"Invert luminance", SpriteColorFXEditorHelper.TooltipInvertLuminance, effect.invertLum, false);

      effect.textureMask = EditorGUILayout.ObjectField(new GUIContent(@"Mask (RGBA)", SpriteColorFXEditorHelper.TooltipTextureMask), effect.textureMask, typeof(Texture2D), false) as Texture2D;
    }
  }
}