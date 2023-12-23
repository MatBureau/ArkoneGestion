using ArkoneGestionEvenement.DAL;
using ArkoneGestionEvenement.Models;
using ArkoneGestionEvenement.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Logique d'interaction pour FEN_Inscription.xaml
    /// </summary>
    public partial class FEN_Inscription : Window
    {
        public FEN_Inscription()
        {
            InitializeComponent();
        }

        private void BTN_create_Click(object sender, RoutedEventArgs e)
        {

            
            {
                string userName = tbx_username.Text;
                string mail = tbx_mail.Text;
                string password = tbx_password.Password;
                bool isVigile = false;

                if (!Utils.ControlSaisie.IsValidEmail(mail))
                {
                    MessageBox.Show("Adresse e-mail invalide");
                    return;
                }

                // Vérification du format du nom d'utilisateur
                if (!Utils.ControlSaisie.IsValidUsername(userName))
                {
                    MessageBox.Show("Nom d'utilisateur invalide");
                    return;
                }

                // Vérification du format du mot de passe
                if (!Utils.ControlSaisie.IsValidPassword(password))
                {
                    MessageBox.Show("Mot de passe invalide");
                    return;
                }

                if (tbx_password.Password != tbx_passwordConfirme.Password)
                {
                    MessageBox.Show("les mots de passes ne sont pas identiques");

                }
                else

                    if (cbx_vigile.IsChecked == true)
                {
                    isVigile = true;
                }
                else
                {
                    isVigile = false;
                }
                string hashedPassword = Utils.SecurityManager.HashPassword(password);

                Utilisateur utilisateur = new Utilisateur();
                utilisateur.NomUtilisateur = userName;
                utilisateur.Email = mail;
                utilisateur.IsVigile = isVigile;
                utilisateur.MotDePasse = hashedPassword;

                ConnexionService.AddUtilisateur(utilisateur);
                FEN_Login fen_Login = new FEN_Login();
                fen_Login.Show();
                this.Close();
                MessageBox.Show("Utilisateur créé");
            }
            
        }


        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            FEN_Login fen_Login = new FEN_Login();
            fen_Login.Show();
            this.Close();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
