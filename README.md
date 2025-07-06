⚽ Jeu de Football 3D – Unity

Ce projet est un prototype de jeu de football réalisé dans Unity. Il permet au joueur de contrôler un personnage en vue à la 3e personne, de dribbler, tirer, faire des passes, et marquer des buts dans un terrain défini. Il inclut une interface graphique, un menu principal, un système de score et un timer.
🎮 Comment jouer
Contrôles :

    ZQSD / WASD : Déplacement du joueur.

    Souris : Rotation de la caméra.

    Clic gauche (maintenu) : Charger un tir.

    Relâchement du clic gauche : Tirer.

    Clic droit : Faire une passe.

    Échap : Revenir au menu principal.

Règles :

    Le joueur peut prendre possession de la balle en s’en approchant.

    Le tir est directionnel et influencé par le temps de chargement.

    La balle est ralentie par un frottement réaliste.

    Si la balle sort du terrain, elle réapparaît près de la limite.

    Quand un but est marqué, la balle est replacée au centre et le score augmente.

    Un timer mesure la durée de jeu.

🧠 Fonctionnalités principales

    ✅ Système de possession de balle avec suivi du joueur.

    ✅ Passe rapide vers l'avant.

    ✅ Interface avec :

        Barre de puissance de tir

        Timer de jeu

        Score

    ✅ Respawn automatique de la balle :

        Au centre après un but

        À l’intérieur du terrain avec marge si elle sort

    ✅ Menu principal avec bouton "Jouer", gestion des caméras.

    ✅ Terrain défini par un objet de type cube avec un collider et dimensions visibles.

🧪 Bugs connus / améliorations possibles

- Le canvas de gameplay avec l'affichage du timer, jauge de tire et score ne s'affiche pas
- pour une raison qui m'échappe le tir et trop puissant et sort du terrain à chaque fois