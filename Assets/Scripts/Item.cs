using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    
    public string itemName; // ������ �̸�
    public Sprite itemImage; // ������ �̹���
    public ItemType itemType;
    public GameObject itemPrefab; // ������ ������

    public string weaponType; // ���� Ÿ��

    public enum ItemType
    {
        Equipment,
        Used,
        Ingridient,
        ETC
    }
}
