using System;

enum ElementalType
{
    Fire,
    Water,
    Earth,
    Air
}

enum MoralType
{
    Good = 1,
    Neutral = 2,
    Evil = 3
}

enum EthicalType
{
    Order = 1,
    Neutral = 2,
    Chaos = 3
}

class Card
{
    public string Name { get; set; }
    public ElementalType Element { get; set; }
    public int Strength { get; set; }
    public MoralType Moral { get; set; }
    public EthicalType Ethical { get; set; }

    public int MoralEthicalValue
    {
        get { return (int)Moral * (int)Ethical; }
    }

    // Конструктор для создания карты
    public Card(string name, ElementalType element, int strength, MoralType moral, EthicalType ethical)
    {
        Name = name;
        Element = element;
        Strength = strength;
        Moral = moral;
        Ethical = ethical;
    }
}

class Enemy
{
    public string Name { get; set; }
    public ElementalType Element { get; set; }
    public int Health { get; set; }
    public MoralType Moral { get; set; }
    public EthicalType Ethical { get; set; }

    // Конструктор для создания врага
    public Enemy(string name, ElementalType element, int health, MoralType moral, EthicalType ethical)
    {
        Name = name;
        Element = element;
        Health = health;
        Moral = moral;
        Ethical = ethical;
    }

    // Метод для атаки врага
    public void Attack(Card card)
    {
        int damage = card.Strength;

        // Проверяем, есть ли иммунитет у врага к стихии карты
        if (card.Element == Element)
        {
            damage = (int)(damage * 0.7); // 30% дебафф на силу карты
        }

        // Вызываем метод получения урона у врага
        TakeDamage(damage);
    }

    // Метод для получения урона врагом
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    // Метод для проверки возможности договориться с врагом
    public bool TryToNegotiate(Card card)
    {
        int diff = Math.Abs(card.MoralEthicalValue - (int)Moral * (int)Ethical);
        float maxDiff = 9;
        double chance = 1 - diff / maxDiff;

        Random random = new Random();
        double randomDouble = random.NextDouble();

        return randomDouble < chance;
    }
}

class Program
{
    // ... Ваш предыдущий код ...

    // Метод для проведения боя между картой и врагом
    static void Battle(Card card, Enemy enemy)
    {
        Console.WriteLine($"Вы выбрали карту: {card.Name}");
        Console.WriteLine($"Противник: {enemy.Name}, Здоровье: {enemy.Health}");

        // Проверяем, можно ли договориться с врагом
        if (enemy.TryToNegotiate(card))
        {
            Console.WriteLine("Вы договорились с врагом. Победа!");
            return;
        }

        // Производим атаку картой
        enemy.Attack(card);
        Console.WriteLine($"Вы нанесли {card.Strength} урона врагу.");

        // Проверяем, победил ли игрок
        if (enemy.Health <= 0)
        {
            Console.WriteLine("Вы победили врага!");
        }
        else
        {
            Console.WriteLine("Вы проиграли. Игра окончена.");
        }
    }

    static void Main()
    {
        // Ваш предыдущий код...

        // Создаем карты и врага
        Card card1 = new Card("Карта 1", ElementalType.Fire, 10, MoralType.Good, EthicalType.Order);
        Card card2 = new Card("Карта 2", ElementalType.Water, 15, MoralType.Neutral, EthicalType.Neutral);
        Card card3 = new Card("Карта 3", ElementalType.Earth, 12, MoralType.Evil, EthicalType.Chaos);

        Enemy enemy = new Enemy("Враг", ElementalType.Air, 20, MoralType.Good, EthicalType.Chaos);

        // Выводим значения карты и врага
        Console.WriteLine($"Карта: {card1.Name}, Стихия: {card1.Element}, Сила: {card1.Strength}");
        Console.WriteLine($"Враг: {enemy.Name}, Стихия: {enemy.Element}, Здоровье: {enemy.Health}");

        // Вступаем в бой с врагом
        Battle(card1, enemy);

        // ... Остальной код ...
    }
}
