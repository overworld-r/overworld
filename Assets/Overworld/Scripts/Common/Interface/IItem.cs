public interface IItem
{
    public string name { get; }
    public string description { get; }
    public int price { get; }

    public void OnBuilt()
    {
        OnEnable();
    }

    public void OnBroken()
    {
        OnDisable();
    }

    public void OnEnable() { }

    public void OnDisable() { }
}
