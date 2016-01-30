using System.Collections.Generic;

public struct AttributeModifier
{
    public Attribute attribute;
    public float ratio;

    public AttributeModifier(Attribute attribute, float ratio)
    {
        this.attribute = attribute;
        this.ratio = ratio;
    }
}

public class CalculatedStat : BaseStat
{
    private List<AttributeModifier> modifiers;
    private int modifierValue;

    public CalculatedStat()
    {
        modifiers = new List<AttributeModifier>();
        modifierValue = 0;
    }

    public void AddModifier(AttributeModifier modifier)
    {
        modifiers.Add(modifier);
    }

    private void CalculateModifiedValue()
    {
        modifierValue = 0;
        
        if (modifiers.Count > 0)
        {
            foreach (AttributeModifier modifier in modifiers)
            {
                modifierValue += (int)(modifier.attribute.AdjustedBaseValue * modifier.ratio);
            }
        }
    }

    public new int AdjustedBaseValue
    {
        get { return BaseValue + BuffValue + modifierValue; }
    }

    public void Update()
    {
        CalculateModifiedValue();
    }


}
