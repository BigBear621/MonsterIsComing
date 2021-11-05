using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character
{
    Monster monster;
    public NavMeshAgent meshAgent;

    public Monster() {}
    public Monster(string id, int level, int hp, int atk, int def)
    {
        this.id = id;
        this.level = level;
        this.hp = hp;
        this.maxHp = hp;
        this.atk = atk;
        this.def = def;
    }

    void OnEnable()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Die();
    }

    public virtual void OnTrace()
    {

    }

    void Attack(Player player)
    {
        player.OnHit(monster);
    }

    public void OnHit(Player player)
    {
        HP -= player.atk;
    }

    public override void Die()
    {
        // MonsterPool.instance.Despawn(gameObject);
        MonsterPool.Despawn(gameObject);
        Debug.Log(name + "은 무로 돌아갔다.");
    }

    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            Attack(other.gameObject.GetComponent<Player>());
    }
}
