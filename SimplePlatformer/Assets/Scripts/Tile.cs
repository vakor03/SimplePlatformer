using System;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer _renderer;

    public TMP_Text TileText;

    public bool ObjActive { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
       
        TileText = GetComponentInChildren<TMP_Text>();
        TileText.color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (!ObjActive)
        {
            ChangeColor(Color.red);
        }
        else
        {
            ChangeColor(Color.grey);
        }

        ObjActive = !ObjActive;
    }

    public void ChangeColor(Color color, bool changeActive = false)
    {
        if (!ObjActive)
        {
            _renderer.material.color = color;
            if (changeActive)
            {
                TileText.color = Color.blue;
                ObjActive = !ObjActive;
            }
        }
    }

    public void Reset()
    {
        ObjActive = false;
        ChangeColor(Color.white);
        TileText.text = String.Empty;
        TileText.color = Color.grey;
    }
}