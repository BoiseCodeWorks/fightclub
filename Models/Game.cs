using System;

namespace FightClub.Models
{
  public class Game
  {
    public Fighter ActiveFighter { get; set; }
    public Enemy ActiveEnemy { get; set; }

    public void GameSetup ()
    {
      Enemy GlassJawJoe = new Enemy ("Glass Jaw Joe", 0, 1);
      Enemy FiddleSticks = new Enemy ("Fiddle Sticks McGee", 0, 5);
      Enemy BigBoss = new Enemy ("Big Boss Betty", 0, 25);
      Enemy DavidNoShow = new Enemy ("David No Show", 0, 25);
      Enemy PatrickLeftUsBehind = new Enemy ("Patrick Left Us Behind", 0, 25);
      Enemy ZackShowUp = new Enemy ("Zacky Show Up", 0, 25);
      Enemy JordanMasterCode = new Enemy ("Jordan Master of all Codes", 0, 25);
     
      Weapon Pipe = new Weapon ("Lead Pipe", 2);
      Weapon Knife = new Weapon ("Legendary Rusty Knife", 3);
      Weapon Chicken = new Weapon ("Rubber Chicken", -5);
      Weapon Popcorn = new Weapon ("CodeWorks Popcorn",4);
      Weapon Bat = new Weapon ("Baseball Bat", 8);
      Weapon ThirdPartySoftware = new Weapon ("Third Party Software", 1);
      Weapon TheCodes = new Weapon ("All the Codes", 50);

      GlassJawJoe.Inventory.Add (Pipe);
      FiddleSticks.Inventory.Add (Knife);
      DavidNoShow.Inventory.Add(Popcorn);
      ZackShowUp.Inventory.Add(Bat);
      PatrickLeftUsBehind.Inventory.Add(ThirdPartySoftware);
      JordanMasterCode.Inventory.Add(TheCodes); 
      BigBoss.Inventory.Add (Chicken);

      GlassJawJoe.NextEnemies.Add(FiddleSticks.Name.ToLower(), FiddleSticks);
      GlassJawJoe.NextEnemies.Add(ZackShowUp.Name.ToLower(), ZackShowUp);
      FiddleSticks.NextEnemies.Add(GlassJawJoe.Name.ToLower(), GlassJawJoe);
      FiddleSticks.NextEnemies.Add(DavidNoShow.Name.ToLower(), DavidNoShow);
      FiddleSticks.NextEnemies.Add(PatrickLeftUsBehind.Name.ToLower(), PatrickLeftUsBehind);
      DavidNoShow.NextEnemies.Add(FiddleSticks.Name.ToLower(), FiddleSticks);
      PatrickLeftUsBehind.NextEnemies.Add(FiddleSticks.Name.ToLower(), FiddleSticks);
      PatrickLeftUsBehind.NextEnemies.Add(ZackShowUp.Name.ToLower(), ZackShowUp);
      ZackShowUp.NextEnemies.Add(PatrickLeftUsBehind.Name.ToLower(), PatrickLeftUsBehind);
      ZackShowUp.NextEnemies.Add(GlassJawJoe.Name.ToLower(), GlassJawJoe);
      ZackShowUp.NextEnemies.Add(JordanMasterCode.Name.ToLower(), JordanMasterCode);
      JordanMasterCode.NextEnemies.Add(BigBoss.Name.ToLower(), BigBoss);
      JordanMasterCode.NextEnemies.Add(ZackShowUp.Name.ToLower(), ZackShowUp);
      System.Console.WriteLine ("Please enter your name.");
      string userInput = Console.ReadLine ();
      ActiveFighter = new Fighter (userInput, 1);
      ActiveEnemy = GlassJawJoe;
      Greeting ();
      Fight ();
    }
    private void Greeting ()
    {
      System.Console.WriteLine ($@"
    Hello! {ActiveFighter.Name} Welcome to FightClub, 
    Remember never talk about!
  ");
    }

    // ACTIONS OF THE GAME
    /**
    Fight
    if Enemey Health 0
    Tell Fighter Enemy is knocked out
      - Drop item ??? 
      - Give option to take item
        - add item to inventory 
    Ask if ready to proceed? => no quit
      - change active enemy 
        - If no next enemy PLAYER WINS 
      START FIGHT OVER
     */
     private void SelectNextEnemy()
     {
       // invoke a new method will write called SelectNextEnemy()
          //1. list out available next fighters
          //2. allow for user selection
              // 2a. sanitize and validate selection
          //3. assigning the ActiveEnemy the value of the selection
        System.Console.WriteLine("Who's next on your path of destruction?");
        bool isSelecting = true;
        while(isSelecting)
        {
        foreach(var kvp in ActiveEnemy.NextEnemies)
        {
          System.Console.WriteLine($"{kvp.Key}");
        }
        string userInput = Console.ReadLine().ToLower();

        #region Logic for when NextEnemies is of type Dictionary<string, Enemy>        
        if(ActiveEnemy.NextEnemies.ContainsKey(userInput))
        {
          ActiveEnemy = ActiveEnemy.NextEnemies[userInput];
          isSelecting = false;
        }
        #endregion

        #region Logic for when NextEnemies is of type List<Enemy>
        // foreach (Enemy enemy in ActiveEnemy.NextEnemies)
        // {
        //  if(userInput == enemy.Name.ToLower())
        //  {
        //   ActiveEnemy = enemy; 
        //   isSelecting = false;
        //  }
        // }
        #endregion
        if(isSelecting)
        {
          System.Console.WriteLine("invalid selection");
        }
        }
        Fight();
     }
    private void Fight ()
    {
      if (ActiveEnemy.Health <= 0)
      {
        OnEnemyKilled();
        if (ActiveEnemy.NextEnemies.Count == 0)
        {
          Win();
          return;
        }
        System.Console.WriteLine("You've defeated {0}", ActiveEnemy.Name);
        
        SelectNextEnemy();
      }
      //punch should take away hit points
      System.Console.WriteLine ($"You are currently fighting: {ActiveEnemy.Name}, their health is {ActiveEnemy.Health}. What would you like to do?");
      System.Console.WriteLine ("(P)unch");
      System.Console.WriteLine ("(R)un");
      string playerChoice = Console.ReadLine ().ToLower ();
      switch (playerChoice)
      {
        case "p":
          ActiveEnemy.Health -= ActiveFighter.Attack;
          Fight ();
          break;
        case "r":
          BravelyRunAway ();
          break;
        default:
          System.Console.WriteLine ("Invalid command");
          Fight ();
          break;
      }
    }

    private void OnEnemyKilled()
    {
      if(ActiveEnemy.Inventory.Count == 0){
        return;
      }
      System.Console.WriteLine($"{ActiveEnemy.Name} is knocked out and dying. They dropped the following item(s):");
      ActiveEnemy.Inventory.ForEach(weapon =>
      {
        System.Console.WriteLine(weapon.Name);
      });
      System.Console.WriteLine("Pick up weapon? Y/n");
      switch (Console.ReadLine().ToLower())
      {
          case "y":
            while(ActiveEnemy.Inventory.Count > 0) {
              ActiveFighter.Inventory.Add(ActiveEnemy.Inventory[0]);
              ActiveEnemy.Inventory.RemoveAt(0);
            }
            break;
          default:  
          break;
      }
    }

    private void BravelyRunAway ()
    {
      System.Console.WriteLine("COWARD!!! Game Over, You are no longer invited to the Fight Club!");
    }

    private void Win ()
    {
      System.Console.WriteLine ("You are the Winner, but no one will know your name");
      System.Console.WriteLine ("Play Again? Y/n");
      string playAgain = System.Console.ReadLine ().ToLower ();
      if (playAgain == "y")
      {
        GameSetup ();
      }
    }
  }

}
