using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false; // �κ��丮 Ȱ��ȭ ���¸� �����ϴ� ���� 

    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) // I Ű�� ���� �κ��丮 ����
        {
            inventoryActivated = !inventoryActivated; // �κ��丮 ���� ���
           
            if (inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true); // �κ��丮 UI Ȱ��ȭ
    }
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false); // �κ��丮 UI ��Ȱ��ȭ
    }
    public void AcquireItem(Item _item,int _count = 1)
    {
       
        if (_item.itemType != Item.ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null) 
                {
                    if (slots[i].item.itemName == _item.itemName) // ���� �������� �ִ� ���
                    {
                        slots[i].SetSlotCount(_count); // ������ ���� ����
                        return;
                    }
                }
                
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) // �� ���� ã��
            {
                slots[i].AddItem(_item, _count); // ������ �߰�
                return;
            }
        }
    }
}
