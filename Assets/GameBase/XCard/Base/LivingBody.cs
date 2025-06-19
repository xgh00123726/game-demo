using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBody : MonoBehaviour
{
    private int _health;
    private int _armor;
    private int _shield;

    public Rect bounds;

    public void BeAttack(int damage)
    {
        damage -= _armor;
        if (damage > _shield)
        {
            damage -= _shield;
            _shield = 0;
        }
        else
        {
            _shield -= damage;
            damage = 0;
        }
        _health -= damage;
    }
}
