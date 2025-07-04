using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false; // 인벤토리 활성화 상태를 저장하는 변수 

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
        if (Input.GetKeyDown(KeyCode.I)) // I 키를 눌러 인벤토리 열기
        {
            inventoryActivated = !inventoryActivated; // 인벤토리 상태 토글
           
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
        go_InventoryBase.SetActive(true); // 인벤토리 UI 활성화
    }
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false); // 인벤토리 UI 비활성화
    }
    public void AcquireItem(Item _item,int _count = 1)
    {
       
        if (_item.itemType != Item.ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null) 
                {
                    if (slots[i].item.itemName == _item.itemName) // 같은 아이템이 있는 경우
                    {
                        slots[i].SetSlotCount(_count); // 아이템 개수 증가
                        return;
                    }
                }
                
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) // 빈 슬롯 찾기
            {
                slots[i].AddItem(_item, _count); // 아이템 추가
                return;
            }
        }
    }
}
