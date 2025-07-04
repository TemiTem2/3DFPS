using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    
    public string itemName; // 아이템 이름
    public Sprite itemImage; // 아이템 이미지
    public ItemType itemType;
    public GameObject itemPrefab; // 아이템 프리팹

    public string weaponType; // 무기 타입

    public enum ItemType
    {
        Equipment,
        Used,
        Ingridient,
        ETC
    }
}
