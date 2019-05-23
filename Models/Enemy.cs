using System.Collections.Generic;

namespace FightClub.Models
{
  public class Enemy
  {
    public readonly string Name;
    public int BaseAttack { get; set; }
    public int Health { get; set; }
    // public List<Enemy> NextEnemies { get; set; } 
    public Dictionary<string, Enemy> NextEnemies { get; set; } 
    public List<Weapon> Inventory { get; set; } = new List<Weapon>();

    public Enemy(string name, int baseAttack, int health)
    {
        Name = name;
        BaseAttack = baseAttack;
        Health = health;
        // NextEnemies = new List<Enemy>();
        NextEnemies = new Dictionary<string, Enemy>();
        
    }


  }
}