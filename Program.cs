using System;
using FightClub.Models;
namespace FightClub
{
    class Program
    {
        static void Main(string[] args)
        {
            Game fightClub = new Game();
            fightClub.GameSetup();
        }
    }
}
