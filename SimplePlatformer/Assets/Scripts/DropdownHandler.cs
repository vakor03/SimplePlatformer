using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        List<string> items = new List<string>
        {
            "A* algorithm",
            "Lee algorithm"
        };
        DropdownItemSelected(dropdown);
        
        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData(){text = item});
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown);});
    }

    private void DropdownItemSelected(Dropdown dropdown)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
