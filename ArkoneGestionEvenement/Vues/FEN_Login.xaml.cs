using ArkoneGestionEvenement.DAL;
using ArkoneGestionEvenement.Models;
using ArkoneGestionEvenement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArkoneGestionEvenement.Vues
{
    /// <summary>
    /// Logique d'interaction pour FEN_Login.xaml
    /// </summary>
    public partial class FEN_Login : Window
    {
        public FEN_Login()
        {
            InitializeComponent();
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets", "Images", "logo.png");
            var uri = new Uri(path);
            logo.UriSource = uri;
            logo.EndInit();
            img_logo.Source = logo;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string userName = tbx_user.Text;
            string password = tbx_password.Password;

            string hashedPassword = Utils.SecurityManager.HashPassword(password);

            Utilisateur Utilisateur = ConnexionService.SelectUtilisateurByNameAndPwd(userName, hashedPassword);

            if (Utilisateur != null)
            {
                VariablesGlobales.UtilisateurCourant = Utilisateur;
                //random pour avoir 5% de chance d'avoir un easter egg

                Random random = new Random();
                int randomValue = random.Next(0, 101);
                if (VariablesGlobales.UtilisateurCourant.IsVigile == true && randomValue > 5)
                {
                    FEN_ModuleVigile fen_vigile = new FEN_ModuleVigile();
                    fen_vigile.Show();
                    this.Close();
                    
                }
                else if(randomValue <= 5)
                {
                    FEN_EasterEgg fen_easterEgg = new FEN_EasterEgg();
                    fen_easterEgg.Show();
                    this.Close();
                }
                else
                {
                    FEN_ModuleOrganisateur fen_organisateur = new FEN_ModuleOrganisateur();
                    fen_organisateur.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrect.");
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close ();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void tbx_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                loginBtn_Click(sender, e);
            }
        }

        private void tbx_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                loginBtn_Click(sender, e);
            }
        }

        private void BTN_signUp_Click(object sender, RoutedEventArgs e)
        {
            FEN_Inscription fen_inscription = new FEN_Inscription();
            fen_inscription.Show();
            this.Close();
        }

        //CETTE FONCTION EST TEMPORAIRE ET M'EVITE DE DEVOIR ME CONNECTER A CHAQUE FOIS
        private void loginBtn_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Utilisateur Utilisateur = ConnexionService.SelectUtilisateurByNameAndPwd("mathis", "98d0b0c7e5d776a77dd1f21682d6aa482d223cd77795b6171f4d125990761561");

            VariablesGlobales.UtilisateurCourant = Utilisateur;
            
            Random random = new Random();
            int randomValue = random.Next(0, 101);
            if (VariablesGlobales.UtilisateurCourant.IsVigile == true && randomValue > 5)
            {
                FEN_ModuleVigile fen_vigile = new FEN_ModuleVigile();
                fen_vigile.Show();
                this.Close();

            }
            else if (randomValue <= 5)
            {
                //FEN_EasterEgg fen_easterEgg = new FEN_EasterEgg();
                //fen_easterEgg.Show();
                FEN_ModuleOrganisateur fen_organisateur = new FEN_ModuleOrganisateur();
                fen_organisateur.Show();
                this.Close();
            }
            else
            {
                FEN_ModuleOrganisateur fen_organisateur = new FEN_ModuleOrganisateur();
                fen_organisateur.Show();
                this.Close();
            }
        }
    }
}
