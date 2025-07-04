using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;

public class Slot : MonoBehaviour , IPointerClickHandler,IBeginDragHandler, IDragHandler, IEndDragHandler,IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindAnyObjectByType<WeaponManager>();
    }

    private void SetColor(float _alpha) //이미지 투명도 조절
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count=1)//아이템 추가
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        

        SetColor(1);
    }

    public void SetSlotCount(int _count)//아이템 개수 조절
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }

    }
    public void ClearSlot()//슬롯 초기ㅏ화
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;

        text_Count.text = "0";
        go_CountImage.SetActive(false);
        SetColor(0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    StartCoroutine(weaponManager.ChangeWeaponCoroutine(item.weaponType, item.itemName));
                    Debug.Log("장비 아이템: " + item.itemName + "을(를) 장착합니다.");
                }
                else
                {
                    Debug.Log("아이템 사용: " + item.itemName);
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
        
    }

    private void ChangeSlot()
    {
        
         Item tempItem = item;
         int tempCount = itemCount;

         AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
         
        if (tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(tempItem, tempCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;

    }
}
