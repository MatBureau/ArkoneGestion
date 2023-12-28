using ArkoneGestionEvenement.DAL;
using ArkoneGestionEvenement.Models;
using ArkoneGestionEvenement.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// Logique d'interaction pour FEN_AddEvent.xaml
    /// </summary>
    public partial class FEN_AddEvent : Window
    {
        public ObservableCollection<Invite> lst_Invites { get; set; }
        public List<Invite> selectedInvites { get; set; }
        private HashSet<DataGridRow> selectedRows = new HashSet<DataGridRow>();


        public FEN_AddEvent()
        {
            InitializeComponent();
            DataContext = this;
            lst_Invites = InviteService.GetAllInvite();
            selectedInvites = new List<Invite>();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            if (grid == null) return;

            foreach (Invite item in e.AddedItems)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null && !selectedRows.Contains(row))
                {
                    selectedRows.Add(row);
                    row.Background = new SolidColorBrush(Colors.Green);
                }
            }

            foreach (Invite item in e.RemovedItems)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null && selectedRows.Contains(row))
                {
                    selectedRows.Remove(row);
                    row.Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BTN_Annuler_Click(object sender, RoutedEventArgs e)
        {
            FEN_ModuleOrganisateur fen = new FEN_ModuleOrganisateur();
            fen.Show();
            this.Close();
        }

        private void BTN_AddSousEvent_Click(object sender, RoutedEventArgs e)
        {
            CNV_AddSousEvent.Visibility = Visibility.Visible;
        }

        private void BTN_SaveAndSend_Click(object sender, RoutedEventArgs e)
        {
            
            foreach(var item in DTG_Invite.SelectedItems)
            {
                selectedInvites.Add((Invite)item);
            }
            if (ValidateFields())
            {
                Evenement evenement = new Evenement();
                evenement.DateEvenement = DTP_DateEvent.SelectedDate;
                evenement.Nom = TBX_NomEvent.Text;
                evenement.IdOrganisateur = VariablesGlobales.UtilisateurCourant.IdUtilisateur;
                evenement.Latitude = TBX_Latitude.Text;
                evenement.Longitude = TBX_Longitude.Text;
                evenement.Lieu = TBX_Lieu.Text;

                evenement = EvenementService.AddEvenement(evenement);

                foreach(Invite inv in selectedInvites)
                {
                    CodesAcce code = new CodesAcce();
                    code.Statut = "Attente";
                    code.Code = CodeAccesService.GenerateUniqueCode();
                    code.IdEvent = evenement.IdEvent;
                    code.IdInvite = inv.IdInvite;
                    code = CodeAccesService.AddCodeAcces(code);

                    string smtpServer = "smtp-relay.brevo.com";
                    int smtpPort = 587;
                    bool enableSsl = true;
                    string smtpUsername = "mathisbureau@gmail.com";
                    string smtpPassword = "SNBjdK0xGbLPW7qa";
                    string fromAddress = "mathisbureau@gmail.com";
                    string toAddress = inv.Email;
                    string subject = "Invitation à l'évenement : " + TBX_NomEvent.Text;
                    string body = "Félicitation " + inv.Prenom + " " + inv.Nom + ", vous avez été invité pour l'évenement suivant : " + TBX_NomEvent.Text +
                        ".\n\rVous trouverez ci-joint le lieu, les coordonnées gps et votre code d'invitation" +
                        "\n\r Code : " + code.Code + "\n\r Coordonnées : " + evenement.Latitude + " " + evenement.Longitude + " \n\r Lieu : " + evenement.Lieu +
                        "\n\r Au plaisir de vous voir parmis nous ! \n\r Arkone Gestion.";

                    if (SMTPManager.IsValidEmail(toAddress))
                    {
                        SMTPManager.SendEmail(smtpServer, smtpPort, enableSsl, smtpUsername, smtpPassword, fromAddress, toAddress, subject, body);
                    }
                    else
                    {
                        MessageBox.Show("L'adresse email du destinataire n'est pas valide.", "Erreur");
                    }
                }
            }
            MessageBox.Show("Invitations envoyées ! Retour au menu");
            FEN_ModuleOrganisateur modOrga = new FEN_ModuleOrganisateur();
            modOrga.Show();
            this.Close();
        }

        private bool ValidateFields()
        {
            if(ValidateTextBox(TBX_Latitude) && ValidateTextBox(TBX_Longitude) && ValidateTextBox(TBX_NomEvent) && ValidateTextBox(TBX_Lieu) && ValidateDatePicker(DTP_DateEvent))
            {
                return true;
            }
            return false;
        }

        private bool ValidateFieldsSousEvent()
        {
            if (ValidateTextBox(TBX_NomSousEvent) && ValidateDatePicker(DTP_DateSousEvent)) return true;
            return false;
        }
        private bool ValidateTextBox(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Red;
                return false;
            }
            else
            {
                textBox.BorderBrush = Brushes.Gray;
                return true;
            }
                
        }

        private bool ValidateDatePicker(DatePicker datePicker)
        {
            if (!datePicker.SelectedDate.HasValue)
            {
                datePicker.BorderBrush = Brushes.Red;
                return false;
            }
            else
            {
                datePicker.BorderBrush = Brushes.Gray;
                return true;
            }
                
        }

        private void BTN_AnnulerSousEvent_Click(object sender, RoutedEventArgs e)
        {
            CNV_AddSousEvent.Visibility= Visibility.Hidden;
            TBX_NomSousEvent.Text = "";
            DTP_DateSousEvent.SelectedDate = null;
        }

        private void BTN_SaveSousEvent_Click(object sender, RoutedEventArgs e)
        {
            SousEvenement sous = new SousEvenement();
            if (ValidateFieldsSousEvent())
            {
                sous.NomSousEvent = TBX_NomSousEvent.Text;
                sous.DateHeure = DTP_DateSousEvent.SelectedDate;
                




                //SousEventService.AddSousEvent()
            }
            
        }
    }
}
