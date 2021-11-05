using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Player player;

    public Player() { }
    public Player(string id, int level, int hp, int atk, int def)
    {
        this.id = id;
        this.level = level;
        this.hp = hp;
        this.maxHp = hp;
        this.atk = atk;
        this.def = def;
    }

    void Attack(Monster monster)
    {
        monster.OnHit(player);
    }

    public void OnHit(Monster monster)
    {
        HP -= monster.atk;
    }

    public override void Die()
    {
        gameObject.SetActive(false);
        Debug.Log(name + "은 별이 되었다.");
    }

    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Monster"))
            Attack(other.gameObject.GetComponent<Monster>());
    }
}
