using UnityEngine;
public class Mapping
{
    string name;
    KeyCode primaryInput;
    KeyCode? secondaryInput;
    public string Name { get { return name; } set { name = value; } }
    public KeyCode PrimaryInput { get { return primaryInput; } set { primaryInput = value; } }
    public KeyCode? SecondaryInput { get { return secondaryInput; } set { secondaryInput = value; } }

    public Mapping(string name, KeyCode primaryInput, KeyCode? secondaryInput = null)
    {
        Name = name;
        PrimaryInput = primaryInput;
        SecondaryInput = secondaryInput;
    }
}
