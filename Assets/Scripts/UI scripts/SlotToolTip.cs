using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField] 
    private GameObject go_Base;
    [SerializeField]
    private Text txt_ItemName;
    [SerializeField]
    private Text txt_ItemDesc;
    [SerializeField]
    private Text txt_ItemHowToUsed;
    void Start()
    {
        
    }

   public void ShowTootip(Item _item,Vector3 pos)
    {
        go_Base.SetActive(true);
        pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width *0.5f, go_Base.GetComponent<RectTransform>().rect.height*-1f,0);
        go_Base.transform.position = pos;
        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
        if (_item.itemType == Item.ItemType.Equipment)
        {
            txt_ItemHowToUsed.text = "우클릭-장착";
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            txt_ItemHowToUsed.text = "우클릭-사용";
        }
        else
            txt_ItemHowToUsed.text = "";
    }
    public void HideTooltip()
    {
        go_Base.SetActive(false);
    }
}
