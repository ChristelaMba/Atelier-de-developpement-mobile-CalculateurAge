# CalculateurAge

Application mobile de calcul d'âge, développée en .NET MAUI dans le cadre de l'Activité 6 (atelier de développement mobile).

L'utilisateur saisit son nom et sa date de naissance. L'application affiche son âge, indique s'il est majeur ou mineur et donne le nombre de jours avant son prochain anniversaire. Une seconde page présente le détail du calcul.

## Étapes du TP

Chaque étape a son propre commit dans l'historique.

- Phase A : version en code-behind.
- Phase B : seconde page (ResultatPage) et navigation avec Shell.
- Phase C : réécriture en MVVM (BaseViewModel, RelayCommand, CalculateurViewModel).

## Fonctionnalités ajoutées

Toute la logique est dans le ViewModel, aucune n'est dans le code-behind.

- Message "Majeur" ou "Mineur".
- Bouton Effacer, qui remet tous les champs à zéro.
- Refus d'une date de naissance dans le futur.
- Nombre de jours avant le prochain anniversaire.
- Historique des calculs.
- Résultat transmis à la seconde page par le ViewModel, et non par l'URL.
- Page de détail : âge exact en années, mois et jours, jour de la semaine de naissance, nombre de jours vécus, date du prochain anniversaire.
- Style commun à toutes les pages, défini dans App.xaml.

## Structure du projet

'''
CalculateurAge
    Views
        ResultatPage.xaml
        ResultatPage.xaml.cs
    ViewModels
        BaseViewModel.cs
        RelayCommand.cs
        CalculateurViewModel.cs
    App.xaml
    AppShell.xaml
    MainPage.xaml
    MainPage.xaml.cs
    MauiProgram.cs
'''

## Lancer le projet

1. Ouvrir la solution dans Visual Studio 2022 avec la charge de travail .NET MAUI.
2. Choisir un émulateur Android, par exemple Pixel 7.
3. Lancer l'application avec F5.

## Auteur

AKOUDJOU Mba Noelly, GIT-GLO5, 22G00007.
