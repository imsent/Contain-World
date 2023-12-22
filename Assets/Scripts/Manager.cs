using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<EnemyNear> EnemyList = new();

    public int kills;

    public int towerCountPlace;

    public float zonePercent;

    public int upCount;

    public float maxEnemy = 20;

    public upgradeBar upgradeBar;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame

    public void AddEnemy(EnemyNear enemy)
    {
        EnemyList.Add(enemy);
    }
    public void RemoveEnemy(EnemyNear enemy)
    {
        EnemyList.Remove(enemy);
    }

    public void upgradeTower(UpTower tower)
    {
        if (upgradeBar.upgradeCount[upgradeBar.nowUpgrade] > 0)
        {
            tower.upText.SetActive(false);
            player.upVision = false;
            var now = upgradeBar.nowUpgrade;
            upgradeBar.upgradeCount[now] -= 1;
            upgradeBar.transform.GetChild(now).transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text =
                upgradeBar.upgradeCount[now].ToString();
            tower.upgradeImage.GetComponent<SpriteRenderer>().sprite = upgradeBar.transform.GetChild(now).GetComponent<Image>().sprite;
            if (now < 4)
            {
                tower.ball = upgradeBar.balls[now];
            }
            switch (now)
            {
                case 0://Огненная Башня
                    tower.damage = 8;
                    break;
                case 1://Ледяная Башня
                    tower.damage = 6;
                    break;
                case 2://Ядовитая Башня
                    tower.damage = 1;
                    break;
                case 3://Хаотическая Башня
                    tower.damage = 2;
                    break;
                case 4://Укрепленные пластины
                    tower.damage += 5;
                    tower.protection += 0.2f;
                    break;
                case 5://Дополнительный ствол
                    tower.countBall = 2;
                    tower.startRecharge *= 0.9f;
                    break;
                case 6://Взрывной снаряд
                    tower.damage += 10;
                    break;
                case 7://Оптический прицел
                    tower.radius *= 1.5f;
                    tower.damage += 5;
                    break;
                case 8://Починка
                    tower.regeneration = true;
                    break;
            }
        }else
        {
            player.Error("Нет выбранного улучшения");
        }
    }
}
