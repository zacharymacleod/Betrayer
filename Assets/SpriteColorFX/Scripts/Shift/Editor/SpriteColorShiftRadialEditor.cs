///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// Sprite Color Shift Radial editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorShiftRadial))]
  public sealed class SpriteColorShiftRadialEditor : SpriteColorBaseEditor
  {
    private SpriteColorShiftRadial effect;

    /// <summary>
    /// Set the default values.
    /// </summary>
    protected override void ResetDefaultValues()
    {
      if (effect == null)
        effect = this.target as SpriteColorShiftRadial;

      effect.strength = 0.0f;
      
      effect.noiseAmount = 0.0f;

      base.ResetDefaultValues();
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
      if (effect == null)
        effect = this.target as SpriteColorShiftRadial;

      EditorGUIUtility.fieldWidth = 40.0f;

      effect.strength = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(effect.strength * 100.0f), 0, 100, 0) * 0.01f;

      effect.noiseAmount = SpriteColorFXEditorHelper.SliderWithReset(@"Noise amount", SpriteColorFXEditorHelper.TooltipNoiseAmount, effect.noiseAmount * 100.0f, 0.0f, 100.0f, 0.0f) * 0.01f;

      effect.noiseSpeed = SpriteColorFXEditorHelper.SliderWithReset(@"Noise speed", SpriteColorFXEditorHelper.TooltipNoiseSpeed, effect.noiseSpeed * 100.0f, 0.0f, 100.0f, 0.0f) * 0.01f;
    }
  }
}