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

            // Appelez la fonction SelectJoueur avec les valeurs de l'interface utilisateur
            Utilisateur Utilisateur = ConnexionService.SelectJoueur(userName, password);

            if (Utilisateur != null)
            {
                // Le joueur a été trouvé, vous pouvez effectuer des actions supplémentaires ici
                VariablesGlobales.Utilisateur = Utilisateur;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Le joueur n'a pas été trouvé ou le mot de passe est incorrect
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


    }
}
