using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private Material _baseMaterial;
    [SerializeField] private Material _offsetMaterial;
    
    [SerializeField] private Color _standartColor;
    [SerializeField] private Color _hoverColor;

    public Vector3 Position => transform.position;

    public void Init(bool isOffset)
    {
        if (isOffset)
            _meshRenderer.material = _baseMaterial;
        else
            _meshRenderer.material = _offsetMaterial;

        _standartColor = _meshRenderer.material.color;
    }


    // Производительность ?
    public void OnMouseEnter()
    {
        ChangeColor(_hoverColor);
    }

    public void OnMouseExit()
    {
        ChangeColor(_standartColor);
    }

    private void ChangeColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
