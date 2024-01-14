using ArkoneGestionEvenement.DAL;
using ArkoneGestionEvenement.Models;
using ArkoneGestionEvenement.Utils;
using OxyPlot.Series;
using OxyPlot;
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
using System.ComponentModel;

namespace ArkoneGestionEvenement.Vues
{
    /// <summary>
    /// Logique d'interaction pour FEN_ModuleOrganisateur.xaml
    /// </summary>
    public partial class FEN_ModuleOrganisateur : Window
    {
        public ObservableCollection<Evenement> evenements { get; set; }
        public ObservableCollection<Invite> lst_Invites { get; set; }
        private PlotModel _statInvite;
        public PlotModel StatInvite
        {
            get { return _statInvite; }
            set
            {
                _statInvite = value;
                OnPropertyChanged(nameof(StatInvite));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public FEN_ModuleOrganisateur()
        {
            InitializeComponent();
            lst_Invites = InviteService.GetAllInvite();
            evenements = EvenementService.GetAllEvenement();
            StatInvite = new PlotModel { Title = "Statut des Codes d'Accès" };
            DataContext = this;
        }

        private void BTN_AddEvent_Click(object sender, RoutedEventArgs e)
        {
            FEN_AddEvent fen = new FEN_AddEvent();
            fen.Show();
            this.Close();
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
            VariablesGlobales.UtilisateurCourant = null;
            FEN_Login fen = new FEN_Login();
            fen.Show();
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

        private void BTN_ModuleVigile_Click(object sender, RoutedEventArgs e)
        {
            FEN_ModuleVigile fen = new FEN_ModuleVigile();
            fen.Show();
            this.Close();
        }

        private void BTN_AddInvite_Click(object sender, RoutedEventArgs e)
        {
            
            if (TBX_NomInvite.Text != "" && TBX_PrenomInvite.Text != "" && TBX_EmailInvite.Text != "")
            {
                
                if (TBX_EmailInvite.Text.Contains("@") && TBX_EmailInvite.Text.Contains("."))
                {
                    
                    if (InviteService.GetInviteByMail(TBX_EmailInvite.Text) == null)
                    {
                        
                        Invite invite = new Invite();
                        invite.Nom = TBX_NomInvite.Text;
                        invite.Prenom = TBX_PrenomInvite.Text;
                        invite.Email = TBX_EmailInvite.Text;
                        invite.Telephone = TBX_TelephoneInvite.Text;
                        InviteService.AddInvite(invite);
                        lst_Invites.Add(invite);
                        TBX_NomInvite.Text = "";
                        TBX_PrenomInvite.Text = "";
                        TBX_EmailInvite.Text = "";
                        TBX_TelephoneInvite.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("L'email est déjà utilisé");
                    }
                }
                else
                {
                    MessageBox.Show("L'email n'est pas valide");
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
        }

        private void BTN_CreateGrouping_Click(object sender, RoutedEventArgs e)
        {
            if(DTG_Invite.SelectedItems.Count > 1)
            {
                List<Invite> invitesSelectionnes = new List<Invite>();
                foreach (var selectedItem in DTG_Invite.SelectedItems)
                {
                    if (selectedItem is Invite invite)
                    {
                        invitesSelectionnes.Add(invite);
                    }
                }

                bool invitesDejaRegroupes = invitesSelectionnes.Any(invite =>
                {
                    return InviteRegroupeService.GetAllInviteRegroupe()
                        .Any(ir => ir.IdInvite == invite.IdInvite);
                });

                if (invitesDejaRegroupes)
                {
                    MessageBox.Show("Au moins un des invités est déjà dans un regroupement.");
                }
                else
                {
                    RegroupementInvite regroupement = new RegroupementInvite();
                    regroupement.NomRegroupement = "Groupe " + invitesSelectionnes[0].Nom + " " + invitesSelectionnes[0].Prenom;
                    RegroupementService.AddRegroupement(regroupement);

                    foreach (Invite invite in invitesSelectionnes)
                    {
                        InviteRegroupeService.AddInviteRegroupe(new InvitesRegroupement { IdInvite = invite.IdInvite, IdRegroupement = regroupement.IdRegroupement });
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner au moins deux invités");
            }
        }

        private void BTN_Search_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBX_Search.Text))
            {
                DTG_Invite.ItemsSource = lst_Invites;
            }
            else
            {
                var searchText = TBX_Search.Text;
                var filteredData = lst_Invites.Where(invite =>
                    invite.Nom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    invite.Prenom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    invite.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    invite.Telephone.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                DTG_Invite.ItemsSource = filteredData;
            }
        }

        private void TBX_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBX_Search.Text))
            {
                DTG_Invite.ItemsSource = lst_Invites;
            }
            else
            {
                var searchText = TBX_Search.Text;
                var filteredData = lst_Invites.Where(invite =>
                    invite.Nom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    invite.Prenom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    invite.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    invite.Telephone.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                DTG_Invite.ItemsSource = filteredData;
            }
        }

        private void BTN_SearchEvent_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBX_SearchEvent.Text))
            {
                DTG_Event.ItemsSource = evenements;
            }
            else
            {
                var searchText = TBX_SearchEvent.Text;
                var filteredData = evenements.Where(evenement =>
                    evenement.Nom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    evenement.Lieu.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    evenement.IdOrganisateur.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                DTG_Invite.ItemsSource = filteredData;
            }
        }

        private void TBX_SearchEvent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBX_SearchEvent.Text))
            {
                DTG_Event.ItemsSource = evenements;
            }
            else
            {
                var searchText = TBX_SearchEvent.Text;
                var filteredData = evenements.Where(evenement =>
                    evenement.Nom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    evenement.Lieu.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    evenement.IdOrganisateur.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                DTG_Invite.ItemsSource = filteredData;
            }
        }

        private void BTN_DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if(DTG_Event.SelectedItem != null)
            {
                if (MessageBox.Show("Voulez-vous supprimer un évenement ?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    EvenementService.DeleteEvenement((Evenement)DTG_Event.SelectedItem);
                    evenements.Remove((Evenement)DTG_Event.SelectedItem);
                }
            }
        }

        // PAS FINI, DOIT SERVIR A AFFICHER LES STATISTIQUES DE L'EVENT SELECTIONNE
        private void BTN_Statistiques_Click(object sender, RoutedEventArgs e)
        {
            CNV_Statistiques.Visibility = Visibility.Visible;
            StatInvite = new PlotModel { Title = "Statut des Codes d'Accès" };

            ////var pieSeries = new PieSeries(
            ////{
            ////    StrokeThickness = 2.0,
            ////    InsideLabelPosition = 0.5,
            ////    AngleSpan = 360,
            ////    StartAngle = 0
            ////};

            //List<CodesAcce> codes = CodeAccesService.GetCodeAccesByEventId(((Evenement)DTG_Event.SelectedItem).IdEvent);

            //// Compter les codes par statut
            //var statutCounts = codes.GroupBy(c => c.Statut)
            //                        .Select(group => new { Statut = group.Key, Count = group.Count() });

            //// Ajouter les tranches au graphique en fonction des comptes
            //foreach (var statut in statutCounts)
            //{
            //    pieSeries.Slices.Add(new PieSlice(statut.Statut, statut.Count) { IsExploded = true });
            //}

            //StatInvite.Series.Add(pieSeries);
            //DataContext = this;
        }
    }
}
