using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    [SerializeField]//체력
    private int hp;
    private int currentHp;
    [SerializeField]//스테미나
    private int sp;
    private int currentSp;

    [SerializeField]//스테미나 젠 속도
    private int spIncreaseSpeed;
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    private bool spUsed;

    [SerializeField]//방어력
    private int dp;
    private int currentDp;

    [SerializeField]//배고픔
    private int hungry;
    private int currentHungry;
    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    [SerializeField]//목마름
    private int thirsty;
    private int currentThirsty;
    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;
    [SerializeField]//만족도
    private int satisfy;
    private int currentsatisfy;

    [SerializeField]
    private Image[] images_Gauge;

    private const int HP=0,DP = 1,SP = 2, HUNGRY = 3,THIRSTY = 4,SATISFY = 5;

    private void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentsatisfy = satisfy;
    }

    private void Update()
    {
        Hungry();
        Thirsty();
        GaugeUpdate();
        SpRechargeTime();
        SpRecover();
    }

    private void SpRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
            {
                currentSpRechargeTime++;
            }
            else
            {
                spUsed = false;
            }
        }
    }

    private void SpRecover()
    {
        if (!spUsed&&currentSp<sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime++;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("배고픔이 0이 되었습니다.");
        }
    }

    private void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentThirstyDecreaseTime++;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("목마름이 0이 되었습니다.");
        }
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[DP].fillAmount = (float)currentDp / dp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
        images_Gauge[SATISFY].fillAmount = (float)currentsatisfy / satisfy;
    }


    public void IncreaseHp(int count)
    {
        if (currentHp + count < hp)
        {
            currentHp += count;
        }
        else
        {
            currentHp = hp;
        }
    }

    public void DecreaseHp(int count)
    {
        if (currentDp > 0)
        {
            DecreaseDp(count);
            return;
        }
            currentHp -= count;
        if (currentHp <= 0)
            Debug.Log("체력이 0이 되었습니다");
    }
    public void IncreaseDp(int count)
    {
        if (currentDp + count < dp)
        {
            currentDp += count;
        }
        else
        {
            currentDp = dp;
        }
    }

    public void DecreaseDp(int count)
    {
        currentDp -= count;
        if (currentDp <= 0)
            Debug.Log("방어력이 0이 되었습니다");
    }

    public void IncreaseHungry(int count)
    {
        if (currentHungry + count < hungry)
        {
            currentHungry += count;
        }
        else
        {
            currentHungry = hungry;
        }
    }

    public void DecreaseHungry(int count)
    {
        if (currentHungry - count < 0)
        {
            currentHungry = 0;
            return;
        }
        else
            currentHungry -= count;
    }

    public void IncreaseThirsty(int count)
    {
        if (currentThirsty + count < thirsty)
        {
            currentThirsty += count;
        }
        else
        {
            currentThirsty = thirsty;
        }
    }

    public void DecreaseThirsty(int count)
    {
        if (currentThirsty - count < 0)
        {
            currentThirsty = 0;
            return;
        }
        else
            currentThirsty -= count;
    }
    public void DecreaseStamina(int count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if(currentSp- count > 0)
        {
            currentSp -= count;
        }
        else
        {
            currentSp = 0;
        }
    }

    public int GetCurrentSp()
    {
        return currentSp;
    }
}
