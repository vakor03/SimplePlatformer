using System;
using Additional;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer _renderer;

    public TMP_Text tileText;
    public event Action<Tile> OnMouseDownEvent;
    public Coordinates Coordinates;

    public bool ObjActive { get; set; }

    
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
       
        tileText = GetComponentInChildren<TMP_Text>();
        tileText.color = Color.grey;
    }

    private void OnMouseEnter()
    {
        if (!ObjActive)
        {
            ChangeColor(Color.grey);
        }
    }

    private void OnMouseExit()
    {
        if (!ObjActive)
        {
            ChangeColor(Color.white);
        }
    }

    private void OnMouseDown()
    {
        OnMouseDownEvent?.Invoke(this);
    }

    public void ChangeColor(Color color, bool changeActive = false)
    {
        if (!ObjActive)
        {
            _renderer.material.color = color;
            if (changeActive)
            {
                tileText.color = Color.blue;
                ObjActive = !ObjActive;
            }
        }
    }

    public void Reset()
    {
        ObjActive = false;
        ChangeColor(Color.white);
        tileText.text = String.Empty;
        tileText.color = Color.grey;
    }
}