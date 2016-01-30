using System;
using System.Collections;

using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    private string _name;

    private Attribute[] _primaryAttributes;

    public void Awake()
    {
        _name = string.Empty;

        _primaryAttributes = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];

        SetupPrimaryAttributes();
    }

    public string Name
    {
        get { return _name; }

        set { _name = value; }
    }

    private void SetupPrimaryAttributes()
    {
        for (int i = 0; i < _primaryAttributes.Length; i++)
        {
            _primaryAttributes[i] = new Attribute();
        }
    }

    public Attribute GetPrimaryAttribute(int index)
    {
        return _primaryAttributes[index];
    }
}
