public class ItemSkinSelector
{
    public string NameSkin { get; set; } = null;
    public UnityEngine.Sprite SkinSprite { get; set; } = null;

    public ItemSkinSelector(UnityEngine.Sprite sprite, string skin_name)
    {
        SkinSprite = sprite;
        NameSkin = skin_name;
    }
}
