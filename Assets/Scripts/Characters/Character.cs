using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string id;
    public int level;
    public int hp;
    public int maxHp;
    public virtual int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp >= maxHp)
            {
                hp = maxHp;
                Debug.Log(name + " Ç® ÇÇ");
            }
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }
    public int atk;
    public int def;
    
    public Character() {}
    public Character(string id, int level, int hp, int atk, int def)
    {
        this.id = id;
        this.level = level;
        this.hp = hp;
        this.maxHp = hp;
        this.atk = atk;
        this.def = def;
    }

    public virtual void Die() {}

    public virtual void OnCollisionEnter(Collision other) {}
}