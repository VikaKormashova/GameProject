```mermaid
classDiagram
    class Game {
        -bool _isRunning
        +Start()
        +Stop()
        +Run()
        +HandleInput()
        +Update()
        +Render()
    }
    
    class Entity {
        <<abstract>>
        +string Name
        +int Health
        +int MaxHealth
        +TakeDamage(int damage)
        +IsAlive() bool
    }
    
    class Player {
        +int Experience
        +int Level
        +GainExperience(int amount)
        +Heal(int amount)
    }
    
    class Enemy {
        +int Damage
        +int ExperienceReward
        +Attack(Player target)
    }
    
    class Item {
        <<abstract>>
        +string Name
        +string Description
        +Use(Player target)
    }
    
    class GameManager {
        -static GameManager _instance
        +Player CurrentPlayer
        +bool IsInCombat
        +Instance GameManager
        +StartNewGame(string name)
        +StartCombat(List~Enemy~ enemies)
        +EndCombat()
    }
    
    Player --|> Entity : наследует
    Enemy --|> Entity : наследует
    GameManager --> Player : управляет
    GameManager --> Enemy : управляет
    Game --> GameManager : использует
    Game --> Entity : использует