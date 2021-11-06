using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class DropdownController : MonoBehaviour
{
    private Dropdown dropdown;
    private string[] shapes = {"check", "circle", "spiral", "square", "star", "triangle"};
    public Text selectedText;
    public Text updates;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)){
            int val = dropdown.value;
            val += 1;
            if (val >= dropdown.options.Count)
            {
                val = 0;
            }
            dropdown.value = val;
        }


        string[] updateArr = new string[dropdown.options.Count];
        for(int i = 0; i < dropdown.options.Count; i++)
        {
            var count = Directory.GetFiles("Assets\\Spell_Templates\\" + shapes[i], shapes[i] + "*.txt", SearchOption.AllDirectories).Count();
            updateArr[i] = shapes[i] + ": " + count;
        }

        updates.text = "";
        foreach(string line in updateArr)
        {
            updates.text += line + "\n";
        }

        selectedText.text = dropdown.options[dropdown.value].text;

    }
}
