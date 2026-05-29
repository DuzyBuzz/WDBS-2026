using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.DTOs.Auth;

namespace WDBS_2026.Forms.Pickers
{
    public partial class ZonePickerForm : Form
    {
        private readonly AuthenticatedUserDto _user;

        public int SelectedZoneId { get; private set; }

        public string SelectedZoneName { get; private set; } = string.Empty;

        public ZonePickerForm(AuthenticatedUserDto user)
        {
            _user = user;
            InitializeComponent();
            ApplyTheme();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadZonesAsync();
        }

        private void ApplyTheme()
        {
            AppTheme.ApplyFormSurface(this);
            AppTheme.ApplyPageTitle(titleLabel);
            AppTheme.ApplySubtitle(subtitleLabel);

            zoneLabel.Font = AppTheme.SectionFont;
            zoneLabel.ForeColor = AppTheme.BodyTextColor;

            zoneComboBox.Font = AppTheme.BodyFont;
            zoneComboBox.ForeColor = AppTheme.BodyTextColor;

            AppTheme.ApplySeverityButton(selectButton, ButtonSeverity.Primary);
            AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);
        }

        private async Task LoadZonesAsync()
        {
            try
            {
                DBConfig.SetConnectionString(_user.Role);
                await using MySqlConnection connection = DBConfig.GetConnection();
                await connection.OpenAsync();

                const string sql = @"
SELECT zone_id, zone_name
FROM zone
ORDER BY zone_name;";

                await using var command = new MySqlCommand(sql, connection);
                await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                var zones = new List<LookupItem>();
                while (await reader.ReadAsync())
                {
                    zones.Add(new LookupItem(reader.GetInt32("zone_id"), reader.GetString("zone_name")));
                }

                zoneComboBox.DataSource = zones;
                zoneComboBox.DisplayMember = nameof(LookupItem.Name);
                zoneComboBox.ValueMember = nameof(LookupItem.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Load Zones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
                Close();
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (zoneComboBox.SelectedItem is not LookupItem zone)
            {
                MessageBox.Show(this, "Select a zone first.", "Select Zone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedZoneId = zone.Id;
            SelectedZoneName = zone.Name;
            DialogResult = DialogResult.OK;
            Close();
        }

        private readonly record struct LookupItem(int Id, string Name);
    }
}
