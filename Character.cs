using System;
namespace Application
{
    class Character
    {
        public string? Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Armor { get; set; }
        public Alignment Alignment { get; set; }



        public Character(string name, Alignment alignment)
        {
            Name = name;
            Alignment = alignment;
            MaxHealth = Health;
            switch (Alignment)
            {
                case Alignment.Good:
                    Health = 100;
                    Strength = 15;
                    Armor = 8;
                    break;
                case Alignment.Evil:
                    Health = 100;
                    Strength = 20;
                    Armor = 5;
                    break;
                case Alignment.Neutral:
                    Health = 100;
                    Strength = 18;
                    Armor = 6;
                    break;
            }
        }

        public void Attack(Character target)
        {
            int damage = CalculateDamage(target);
            target.TakeDamage(damage);
        }
        private int CalculateDamage(Character target)
        {
            int baseDamage = Strength;
            bool isBerserk = (Health < MaxHealth * 0.25);
            double targetProtection = target.Armor * 0.01;
            // Если цель из противоположной фракции, применяем бафф или дебафф
            if ((Alignment == Alignment.Good && target.Alignment == Alignment.Evil) ||
                (Alignment == Alignment.Evil && target.Alignment == Alignment.Good))
            {
                baseDamage = isBerserk ? (int)(baseDamage * 2.0) : (int)(baseDamage * 0.5);
            }
            if (isBerserk)
            {
                baseDamage *= (int)(baseDamage * 2.0);
                targetProtection -= 0.8;
            }
            int damage = (int)(baseDamage * (1.0 - targetProtection));

            return damage;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
            }
        }
        public int MaxHealth { get; private set; }

    }
}

