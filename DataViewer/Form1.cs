using System.Net.Http.Headers;

namespace DataViewer
{
    public enum DomainEnum
    {
        Demography,
        SyncFin
    };
    public enum DemographyModeEnum
    {
        CultureLandscape,
        CultureCompare
    };
    public enum SyncFinModeEnum
    {
        Future,
        Sweep
    };


    public partial class Form : System.Windows.Forms.Form
    {
        private DomainEnum Domain;
        private string Location;
        private DemographyModeEnum DemographyMode;
        private SyncFinModeEnum SyncFinMode;

        private CultureLandscapeForm cultureLandscapeForm = null;
        private CultureCompareForm cultureCompareForm = null;
        private SyncFinFutureForm syncFinFutureForm = null;
        private SyncFinSweepForm syncFinSweepForm = null;

        public Form()
        {
            InitializeComponent();

            foreach (var d in Enum.GetValues(typeof(DomainEnum)))
                DomainListBox.Items.Add(d.ToString());
        }

        private void DomainListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DomainListBox.SelectedItem == null)
                return;

            TopModePanel.Controls.Clear();
            DropSubForms();

            string selectedDomain = DomainListBox.SelectedItem.ToString();
            switch (selectedDomain)
            {
                case "Demography":
                    Domain = DomainEnum.Demography;
                    Location = "C:\\Users\\Sasha\\OneDrive\\CountryDemographyData\\ResultingData";
                    
                    DataChoiceListBox.Items.Clear();
                    foreach (var d in Enum.GetValues(typeof(DemographyModeEnum)))
                        DataChoiceListBox.Items.Add(d.ToString());
                    break;

                case "SyncFin":
                    Domain = DomainEnum.SyncFin;
                    Location = "C:\\Users\\Sasha\\Data\\Results";
                    
                    DataChoiceListBox.Items.Clear();
                    foreach (var d in Enum.GetValues(typeof(SyncFinModeEnum)))
                        DataChoiceListBox.Items.Add(d.ToString());
                    break;

                default:
                    throw new Exception("Bad logic");
            }
        }

        private void DataChoiceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropSubForms();

            var selectedData = DataChoiceListBox.SelectedItem.ToString();
            switch (Domain)
            {
                case DomainEnum.Demography:
                    if (Enum.TryParse(selectedData, out DemographyModeEnum mode1))
                        DemographyMode = mode1;
                    else
                        throw new Exception("Bad logic");
                    break;

                case DomainEnum.SyncFin:
                    if (Enum.TryParse(selectedData, out SyncFinModeEnum mode2))
                        SyncFinMode = mode2;
                    else
                        throw new Exception("Bad logic");
                    break;

                default:
                    throw new Exception("Unknown domain selected.");
            }

            InitializeTopModePanel();
        }
        private void InitializeTopModePanel()
        {
            if (TopModePanel == null)
                throw new Exception("TopModePanel is null");

            //TopModePanel.Controls.Clear();
            switch (Domain)
            {
                case DomainEnum.Demography:
                    switch (DemographyMode)
                    {
                        case DemographyModeEnum.CultureLandscape:
                            if (cultureCompareForm != null)
                                cultureCompareForm.Hide();

                            cultureLandscapeForm ??= (CultureLandscapeForm)InitilizeSubform(
                                new CultureLandscapeForm(Location));
                            cultureLandscapeForm.Show();
                            break;

                        case DemographyModeEnum.CultureCompare:
                            if (cultureLandscapeForm != null)
                                cultureLandscapeForm.Hide();

                            cultureCompareForm ??= (CultureCompareForm)InitilizeSubform(
                                new CultureCompareForm(Location));
                            cultureCompareForm.Show();
                            break;

                        default:
                            throw new Exception("Unknown mode selected.");
                    }
                    break;
                case DomainEnum.SyncFin:
                    switch (SyncFinMode)
                    {
                        case SyncFinModeEnum.Future:
                            if (syncFinSweepForm != null)
                                syncFinSweepForm.Hide();

                            syncFinFutureForm ??= (SyncFinFutureForm)InitilizeSubform(new SyncFinFutureForm());
                            syncFinFutureForm.Show();
                            break;

                        case SyncFinModeEnum.Sweep:
                            if (syncFinFutureForm != null)
                                syncFinFutureForm.Hide();

                            syncFinSweepForm ??= (SyncFinSweepForm)InitilizeSubform(new SyncFinSweepForm());
                            syncFinSweepForm.Show();
                            break;

                        default:
                            throw new Exception("Unknown mode selected.");
                    }
                    break;
                default:
                    throw new Exception("Unknown domain selected.");
            }
        }

        private System.Windows.Forms.Form InitilizeSubform(System.Windows.Forms.Form subform)
        {
            subform.TopLevel = false;
            subform.FormBorderStyle = FormBorderStyle.None;
            subform.Dock = DockStyle.Fill;

            TopModePanel.Controls.Clear();
            TopModePanel.Controls.Add(subform);
            return subform;
        }

        private void DropSubForms()
        {
            DropSyncFinForms();
            DropDemographyForms();
        }

        private void DropSyncFinForms()
        {
            if (syncFinFutureForm != null)
            {
                syncFinFutureForm.Close();
                syncFinFutureForm.Dispose();
                syncFinFutureForm = null;
            }
            if (syncFinSweepForm != null)
            {
                syncFinSweepForm.Close();
                syncFinSweepForm.Dispose();
                syncFinSweepForm = null;
            }
        }

        private void DropDemographyForms()
        {
            if (cultureLandscapeForm != null)
            {
                cultureLandscapeForm.Close();
                cultureLandscapeForm.Dispose();
                cultureLandscapeForm = null;
            }
            if (cultureCompareForm != null)
            {
                cultureCompareForm.Close();
                cultureCompareForm.Dispose();
                cultureCompareForm = null;
            }
        }
    }
}
