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

            lbl_ErreurConnexion.Visibility = Visibility.Hidden;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string userName = tbx_user.Text;
            string password = tbx_password.Password;

            string hashedPassword = Utils.SecurityManager.HashPassword(password);

            //LBL_ErreurConnexion.Foreground = Brushes.Black;


            // Appelez la fonction SelectJoueur avec les valeurs de l'interface utilisateur
            Utilisateur Utilisateur = ConnexionService.SelectUtilisateurByNameAndPwd(userName, hashedPassword);

            if (Utilisateur != null)
            {
                VariablesGlobales.UtilisateurCourant = Utilisateur;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                lbl_ErreurConnexion.Visibility = Visibility.Visible;
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
    }
}
