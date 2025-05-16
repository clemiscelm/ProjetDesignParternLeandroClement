
# Design Patern

Ce projet a été réalisé au cours de la semaine "Design Patern CPOO", lors du cursus IIM - Game Programing




## Authors

- [@Léandro Benites](https://github.com/Selsanat)
- [@Clément Bostyn](https://www.github.com/clemiscelm)

## Features

- State Machine
- Behavior Tree
- Proxy
- Singleton
- Interfaces
- Scriptable Objects

### State Machine

Le comportement du joueur (Idle, Running, Attack, etc.) est géré par une machine à états explicite, via des classes dérivées de PlayerState.

![altText](https://media.discordapp.net/attachments/1110529644261802004/1372859344072212490/image.png?ex=68284e66&is=6826fce6&hm=f6c69ad9a3c1ebc5535812fba1f53194f3fa8ad36e329d0a3c184b0b5662e276&=&format=webp&quality=lossless&width=533&height=349)

Chaque état (AttackPlayerState, RunningState, etc.) gère son propre comportement.

Le changement d’état est déclenché par les entrées joueur ou le contexte (ChangeState()).

Permet de séparer proprement la logique d’entrée, d’animation et de déplacement selon le contexte.

### Behavior Tree

L’IA des ennemis est construite autour d’un Behavior Tree personnalisé, qui permet de structurer les comportements de manière hiérarchique et déclarative. L’ennemi peut patrouiller, poursuivre le joueur, lui tirer dessus à distance, ou l’attaquer au corps-à-corps selon sa proximité.

![altText](https://media.discordapp.net/attachments/1110529644261802004/1372859087867482132/image.png?ex=68284e29&is=6826fca9&hm=f3c016d358e63cc82c701cb8c46c9932127db6000f39d45c422e015b4a3abbbb&=&format=webp&quality=lossless&width=1720&height=334)

Le Behavior Tree, modèle arborescent permettant de comprendre, étendre et maintenir facilement les comportements complexes.

Chaque nœud est une classe dérivée de Node, organisée en :

- __Composite nodes :__ Selector, Sequence

- __Condition nodes :__ IsPlayerVisibleNode, IsInShootingRangeNode, IsInMeleeRangeNode

- __Action nodes :__ ChaseNode, ShootNode, MeleeAttackNode, PatrolNode

### Proxy

Le système de gestion des points de vie est implémenté avec un Proxy Pattern pour intercepter, modifier ou bloquer les dégâts.

✔ Design Pattern utilisé : Proxy
Permet de contrôler l’accès à l’objet réel (PlayerHealth, EnemyHealth)

_Exemples d’utilisation :_

__Joueur :__ PlayerHealthProxy bloque les dégâts si invincible, applique l’armure

__Ennemi :__ EnemyHealthProxy applique une réduction des dégâts ou des effets contextuels

### Singleton

Le système de gestion des inputs est implémenté de façon a ce que l'on puisse récupérer les inputs du joueur depuis n'importe quel élément. 

__Le player input manager__ est constitué d'un singelton qui permet a la state machine de gérer les inputs du joueur*

### Interfaces

Tous les objets pouvant recevoir des dégâts (joueur, ennemis) implémentent une interface commune IDamageable.

✔ Design Pattern utilisé : Interface / Contract
Permet au code d’attaque (projectile, attaque au corps à corps) de rester générique et réutilisable

_Exemple : damageable.TakeDamage(damage) fonctionne pour le joueur et les ennemis_

### Scriptable Objects

![altText](https://cdn.discordapp.com/attachments/1110529644261802004/1372858384755462234/image.png?ex=68284d81&is=6826fc01&hm=b3adeb03b3e0c3f45e86615be07fdef01c7a4849ccd060984ea5f79e24ad99ed&)

Afin de pouvoir éditer et tweak facilement les paramètres de mouvement du joueur, un scriptable object a été mit en place afin de pouvoir paramétrer ces dernières
## UML

![altText](https://media.discordapp.net/attachments/1110529644261802004/1372850457260720229/ClassDiagram1.png?ex=6828461f&is=6826f49f&hm=b4806e19fdfcd60318e4abb60627c88b7defff8f7127d68ba40a61c58faa738e&=&format=webp&quality=lossless&width=1860&height=420)
## ✅ Conclusion

Cette architecture met l'accent sur la séparation des responsabilités et l'utilisation de patterns éprouvés pour renforcer la lisibilité, la modularité et la facilité d'évolution du code. Elle permet :

d'ajouter ou modifier des comportements sans impacter le reste du système,

de simuler des IA plus complexes sans avoir à coder des arbres à la main,

et d'intégrer facilement de nouveaux effets de gameplay (défense, buffs, attaques spéciales...).
