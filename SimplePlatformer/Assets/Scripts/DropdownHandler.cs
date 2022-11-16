using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    private Action<Dropdown> _dropdownItemSelected;
    private List<string> _items;

    void Start()
    {
    }

    public void InitDropdown(List<string> items, Action<Dropdown> dropdownItemSelected)
    {
        _dropdownItemSelected = dropdownItemSelected;
        _items = items;

        var dropdown = transform.GetComponent<Dropdown>();

        _dropdownItemSelected(dropdown);

        foreach (var item in _items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        dropdown.onValueChanged.AddListener(delegate { _dropdownItemSelected(dropdown); });
    }
}