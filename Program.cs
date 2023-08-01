using System;

enum Alignment
{
    Good,
    Evil,
    Neutral
}
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

class Program
{

    static void Prolog()
    {
        Console.WriteLine("Вы странник, блуждающий по миру.\nВаш карман совсем пуст и в животе урчит голод.\nВ запасах остался последний кусок хлеба и одна медная монета.");
        Console.WriteLine("Вы добрались до врат города Авксом, перед вами стоит Бард, играющий на домбре.\nОн собирает милостыню, чтобы прокормить свою семью.");
        Console.WriteLine("На барта нападает рабойник, он защищается и держит свой улов мертвой хваткой");
    }
    static void GoodPlot()
    {
        Console.WriteLine("Вы решили помочь барту защитить добычу и вступили в бой с разбойником");
        Console.WriteLine("Он, заметив ваше приближение нападает первым");

    }
    static void EvilPlot()
    {
        Console.WriteLine("Вы решили поживиться легкой добычей и помогли рабойнику отобрать добычу");
    }
    static void NeutralPlot()
    {
        Console.WriteLine("Вы решили пройти мимо и нель не в свое дело");
    }
    static void Battle(Character player, Character enemy)
    {
        Console.WriteLine("Бой начинается!");

        while (player.Health > 0 && enemy.Health > 0)
        {
            // Атака бандита на главного персонажа
            enemy.Attack(player);
            Console.WriteLine($"Бандит атакует {player.Name}. Здоровье {player.Name}: {player.Health}");

            // Проверка, не активировано ли состояние берсерка у бандита
            if (enemy.Health < enemy.MaxHealth * 0.25)
            {
                Console.WriteLine("Бандит в состоянии берсерка!");
            }

            // Проверка, не активировано ли состояние берсерка у главного персонажа
            if (player.Health < player.MaxHealth * 0.25)
            {
                Console.WriteLine("Главный персонаж в состоянии берсерка!");
            }

            // Если главный персонаж умер, бой окончен
            if (player.Health <= 0)
            {
                Console.WriteLine($"Главный персонаж {player.Name} погиб...");
                break;
            }

            // Атака главного персонажа на бандита
            player.Attack(enemy);
            Console.WriteLine($"{player.Name} атакует бандита. Здоровье бандита: {enemy.Health}");

            // Если бандит умер, бой окончен
            if (enemy.Health <= 0)
            {
                Console.WriteLine($"Бандит погиб...");
                break;
            }

            Console.WriteLine("Нажмите Enter, чтобы продолжить бой.");
            Console.ReadLine();
        }

        Console.WriteLine("Бой завершен!");
    }

    static Alignment MakeChoice()
    {
        Console.WriteLine("Выберите ваше действие:");
        Console.WriteLine("1. Сделать доброе дело");
        Console.WriteLine("2. Совершить злодеяние");
        Console.WriteLine("3. Проявить пофигизм");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                return Alignment.Good;
            case 2:
                return Alignment.Evil;
            case 3:
                return Alignment.Neutral;
            default:
                Console.WriteLine("Неверный выбор!");
                return MakeChoice();
        }
    }
    

    static void Main()
    {
        Console.WriteLine("Добро пожаловать в игру 'Добро, Зло и пофигизм'!");

        // Здороваемся с игроком и запрашиваем имя
        Console.WriteLine("Введите имя вашего персонажа:");
        string? playerName = Console.ReadLine();
        
        Console.WriteLine($"Привет, {playerName}! Ваша история начинается...");
        // Получаем выбранное выравнивание (Alignment) от игрока
        // Выводим пролог
        Prolog();
        Alignment playerAlignment = MakeChoice();



        // Создаем персонажа с определенным именем
        Character player = new Character(playerName, playerAlignment);
        
    

        Character bandit = new Character("Бандит", Alignment.Evil)
        {
            Health = 20,
            Strength = 40,
            Armor = 1
        };

        // Выводим выбранное выравнивание и начальные параметры персонажа
        Console.WriteLine($"Вы выбрали сторону: {playerAlignment}");
        
        // Вызываем соответствующий сюжет в зависимости от выбранного выравнивания
        switch (playerAlignment)
        {
            case Alignment.Good:
                Console.WriteLine($"Ваши параметры: Здоровье: {player.Health}, Сила: {player.Strength}, Броня: {player.Armor}");
                Console.WriteLine($"Параметры бандита: Здоровье: {bandit.Health}, Сила: {bandit.Strength}, Броня: {bandit.Armor}");
                GoodPlot();
                Battle(player, bandit);

                break;
            case Alignment.Evil:
                EvilPlot();
                break;
            case Alignment.Neutral:
                NeutralPlot();
                break;
        }
    }
}




