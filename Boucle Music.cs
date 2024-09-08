using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;
using WindowsInput;  // Bibliothèque pour simuler les clics de la souris
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Configuration du chemin vers le WebDriver Opera
        var options = new OperaOptions();
        options.BinaryLocation = @"C:\Users\Yanni\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\opera.exe";  // Remplacez par le chemin vers votre installation d'Opera
        var driver = new OperaDriver(@"C:\Program Files\PackageManagement\NuGet\Packages\Selenium.WebDriver.ChromeDriver.128.0.6613.11900\driver\win32\chromedriver.exe", options);  // Remplacez par le chemin vers le WebDriver d'Opera

        // Ouvrir Opera
        driver.Navigate().GoToUrl("https://www.google.com");
        Thread.Sleep(3000);  // Pause pour permettre à Opera de charger

        // Activer le VPN via InputSimulator
        var sim = new InputSimulator();

        // Déplacez la souris sur le bouton VPN (ajustez les coordonnées en fonction de votre écran)
        sim.Mouse.MoveMouseTo(13000, 3000);  // Utilisez InputSimulator pour ajuster les coordonnées de la souris
        sim.Mouse.LeftButtonClick();  // Simule un clic gauche pour activer le VPN
        Thread.Sleep(5000);  // Pause pour permettre au VPN de s'activer

        // Naviguer vers Tidal
        driver.Navigate().GoToUrl("https://login.tidal.com/");
        Thread.Sleep(5000);  // Pause pour permettre au site de charger

        // Se connecter à Tidal
        var username = "votre_identifiant_tidal";
        var password = "votre_mot_de_passe_tidal";

        // Remplir les champs d'identifiant et mot de passe
        var emailInput = driver.FindElement(By.Id("email"));
        var passwordInput = driver.FindElement(By.Id("password"));
        emailInput.SendKeys(username);
        passwordInput.SendKeys(password);
        passwordInput.SendKeys(Keys.Enter);
        Thread.Sleep(5000);  // Pause pour permettre la connexion

        // Rechercher la chanson "Kezzabi" de Petit Prince
        var searchBox = driver.FindElement(By.XPath("//input[@placeholder='Search']"));
        searchBox.SendKeys("Kezzabi Petit Prince");
        searchBox.SendKeys(Keys.Enter);
        Thread.Sleep(5000);  // Pause pour permettre l'affichage des résultats

        // Sélectionner la chanson
        var song = driver.FindElement(By.XPath("//a[contains(text(),'Kezzabi')]"));
        song.Click();
        Thread.Sleep(5000);  // Pause pour permettre à la chanson de démarrer

        // Boucle pour rejouer la chanson en boucle
        while (true)
        {
            Thread.Sleep(180000);  // Attendre 3 minutes (durée estimée de la chanson)
            driver.Navigate().Refresh();  // Recharger la page pour rejouer la chanson
        }
    }
}
