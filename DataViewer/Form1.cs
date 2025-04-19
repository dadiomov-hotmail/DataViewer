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
        private SyncFinModeEnum    SyncFinMode;

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
                    MessageBox.Show("Unknown domain selected.");
                    break;
            }
        }

        private void DataChoiceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedData = DataChoiceListBox.SelectedItem.ToString();
            switch(Domain)
            {
                case DomainEnum.Demography:
                    if (Enum.TryParse(selectedData, out DemographyModeEnum mode1))
                        DemographyMode = mode1;
                    else
                        throw new Exception("BAd logic");
                    break;
                case DomainEnum.SyncFin:
                    if (Enum.TryParse(selectedData, out SyncFinModeEnum mode2))
                        SyncFinMode = mode2;
                    else
                        throw new Exception("BAd logic");
                    break;
                default:
                    throw new Exception("Unknown domain selected.");
                    break;
            }

            InitializeTopModePanel();
        }
        private void InitializeTopModePanel()
        {
            if (TopModePanel == null)
                throw new Exception("TopModePanel is null");
            
            TopModePanel.Controls.Clear();
            switch(Domain)
            {
                case DomainEnum.Demography:
                    switch (DemographyMode)
                    {
                        case DemographyModeEnum.CultureLandscape:
                            //InitilizeCultureLandscapeMode();
                            break;
                        case DemographyModeEnum.CultureCompare:
                            //InitilizeCultureCompareMode();
                            break;
                        default:
                            throw new Exception("Unknown mode selected.");
                    }
                    break;
                case DomainEnum.SyncFin:
                    switch (SyncFinMode)
                    {
                        case SyncFinModeEnum.Future:
                            InitilizeFutureMode();
                            break;
                        case SyncFinModeEnum.Sweep:
                            InitilizeSweepMode();
                            break;
                        default:
                            throw new Exception("Unknown mode selected.");
                    }
                    break;
                default:
                    throw new Exception("Unknown domain selected.");
            }
        }

        private void InitilizeFutureMode()
        {

        }
        private void InitilizeSweepMode()
        {

        }
    }
}
