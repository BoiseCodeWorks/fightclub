using System;
using System.Collections.Generic;

namespace FightClub.Models
{
  public class Fighter
  {
    private int _baseAttack { get; set; }
    public readonly string Name;
    public int Attack
    {
      get
      {
        var damage = 0;
        foreach (var weapon in Inventory)
        {
            damage += weapon.AttackModifier;
        }

        return _baseAttack + damage;
      }
    }
    public List<Weapon> Inventory { get; set; } = new List<Weapon> ();

    public Fighter (string name, int baseAttack)
    {
      Name = name;
      _baseAttack = baseAttack;
    }
  }
}
