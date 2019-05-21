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
      Weapon Pipe = new Weapon ("Lead Pipe", 2);
      Weapon Knife = new Weapon ("Legendary Rusty Knife", 3);
      Weapon Chicken = new Weapon ("Rubber Chicken", -5);
      GlassJawJoe.Inventory.Add (Pipe);
      GlassJawJoe.NextEnemy = FiddleSticks;
      FiddleSticks.Inventory.Add (Knife);
      FiddleSticks.NextEnemy = BigBoss;
      BigBoss.Inventory.Add (Chicken);
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
    private void Fight ()
    {
      if (ActiveEnemy.Health <= 0)
      {
        OnEnemyKilled();
        if (ActiveEnemy.NextEnemy == null)
        {
          Win();
          return;
        }
        ActiveEnemy = ActiveEnemy.NextEnemy;
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
      System.Console.WriteLine($"{ActiveEnemy.Name} is knocked out and dying. They dropped the following item(s):");
      if(ActiveEnemy.Inventory.Count == 0){
        return;
      }
      ActiveEnemy.Inventory.ForEach(weapon =>
      {
        System.Console.WriteLine(weapon.Name);
      });
      System.Console.WriteLine("Pick up weapon? Y/n");
      switch (Console.ReadLine().ToLower())
      {
          case "y":
          ActiveFighter.Inventory.Add(ActiveEnemy.Inventory[0]);
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
