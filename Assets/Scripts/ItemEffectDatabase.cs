using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class ItemEffect
{
    public string itemName;
    [Tooltip("HP, SP, DP, HUNGRY, THIRSTY, SATISFY �� ���� �����մϴ�")]
    public string[] part;
    public int[] num;//��ġ
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    //�ʿ��� ������Ʈ
    [SerializeField]
    private StatusController playerStatus;
    [SerializeField]
    private WeaponManager weaponManager;

    private const string HP = "HP", SP = "SP", DP = "DP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", SATISFY = "SATISFY";

    public void UsedItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
            StartCoroutine(weaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));
            Debug.Log("��� ������: " + _item.itemName + "��(��) �����մϴ�.");
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            for (int x = 0; x < itemEffects.Length; x++) 
            { 
                if(itemEffects[x].itemName == _item.itemName)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                                playerStatus.IncreaseHp(itemEffects[x].num[y]);
                                break;
                            case SP:
                                playerStatus.IncreaseSp(itemEffects[x].num[y]);
                                break;
                            case DP:
                                playerStatus.IncreaseDp(itemEffects[x].num[y]);
                                break;
                            case HUNGRY:
                                playerStatus.IncreaseHungry(itemEffects[x].num[y]);
                                break;
                            case THIRSTY:
                                playerStatus.IncreaseThirsty(itemEffects[x].num[y]);
                                break;
                            case SATISFY:
                                playerStatus.IncreaseSatisfy(itemEffects[x].num[y]);
                                break;
                            default:
                                Debug.Log("��ġ�ϴ� Part�� �����ϴ�: HP, SP , DP , HUNGRY, THIRSTY, SATISFY �� ���� �����մϴ�");
                                break;
                        }
                        Debug.Log("������ ���: " + _item.itemName);
                    }
                    return;
                }

            }
            Debug.Log("��ġ�ϴ� Itemname�� �����ϴ�");
        }
    }
}
