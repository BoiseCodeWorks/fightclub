using System;

namespace FightClub.Models
{
  public class Weapon
  {
    public readonly string Name;
    public int AttackModifier { get; private set; } 

    public Weapon(string name, int attackMod)
    {
        Name = name;
        AttackModifier = attackMod;
    }
  }
}
