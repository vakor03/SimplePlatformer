using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    private Action<TMP_Dropdown> _dropdownItemSelected;
    private List<string> _items;
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    void Start()
    {
        //_dropdown = GetComponent<TMP_Dropdown>();
    }

    public void InitDropdown(List<string> items, Action<TMP_Dropdown> dropdownItemSelected)
    {
        _dropdown.options.Clear();
        _dropdownItemSelected = dropdownItemSelected;
        _items = items;
        foreach (var item in _items)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }

        _dropdown.onValueChanged.AddListener(delegate { _dropdownItemSelected(_dropdown); });
        
        _dropdownItemSelected(_dropdown);
    }
}