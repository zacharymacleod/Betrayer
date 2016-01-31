///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// UV transform.
  /// </summary>
  public class UVTransform : MonoBehaviour
  {
    public Vector2 uvAnimationRate = Vector2.zero;

    protected Vector2 uvOffset = Vector2.zero;

    private MeshRenderer meshRenderer;

    private void OnEnable()
    {
      meshRenderer = base.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
      uvOffset += uvAnimationRate * Time.deltaTime;

      if (uvOffset.x >= 1.0f)
        uvOffset.x = uvOffset.x - 1.0f;

      if (uvOffset.y >= 1.0f)
        uvOffset.y = uvOffset.y - 1.0f;

      if (meshRenderer.material != null)
        meshRenderer.material.mainTextureOffset = uvOffset;
    }
  }
}