# ⚔️ Death Bounce Arena

Welcome to **Death Bounce**, the chaotic weapon brawler where blades clash, shields bounce, and arrows fly!  
Each match is a battle for dominance between autonomous weapons — all controlled by physics and code.

## 🧠 How It Works

- Each **Weapon** (Sword, Spear, Bow, Dagger, Shield) is a `CharacterBody2D` with:
  - Randomized movement
  - Unique stats (speed, damage, health)
  - Parry and bounce mechanics
- The last weapon standing gets a win counted and saved to `stats.json`
- Stats persist across matches

## 🧪 Features

- 🧠 Autonomous AI-based movement and collision logic
- 🛡️ Real-time parry and bounce system
- 📈 Win tracker with JSON serialization
- 💀 Damage and health system with visual indicators
- 🧩 Extensible base class (`WeaponBase`) for all weapon types