using ArkoneGestionEvenement.DAL;
using ArkoneGestionEvenement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour FEN_ModuleOrganisateur.xaml
    /// </summary>
    public partial class FEN_ModuleOrganisateur : Window
    {
        public ObservableCollection<Evenement> evenements { get; set; }
        public ObservableCollection<Invite> lst_Invites { get; set; }
        public FEN_ModuleOrganisateur()
        {
            InitializeComponent();
            DataContext = this;
            lst_Invites = InviteService.GetAllInvite();
            evenements = EvenementService.GetAllEvenement();
        }

        private void BTN_AddEvent_Click(object sender, RoutedEventArgs e)
        {
            FEN_AddEvent fen = new FEN_AddEvent();
            fen.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BTN_InviteList_Click(object sender, RoutedEventArgs e)
        {
            CNV_Invites.Visibility = Visibility.Visible;
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            CNV_Invites.Visibility = Visibility.Hidden;
        }
    }
}
