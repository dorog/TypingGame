using System;

[Serializable]
public class WordMeta
{
    public string Value { get; set; }
    public float MinSpeed { get; set; }
    public float MaxSpeed { get; set; }
    public Complexity Complexity { get; set; }
}
