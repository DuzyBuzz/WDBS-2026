using System.Data;
using WDBS_2026.Models;
using WDBS_2026.Services.Cashier;

namespace WDBS_2026.Forms.Pickers;

public partial class CollectionPickerForm : Form
{
    private readonly UserRole _role;

    public int? SelectedCollectionId { get; private set; }

    public CollectionPickerForm(UserRole role, int? currentCollectionId = null)
    {
        _role = role;
        SelectedCollectionId = currentCollectionId;

        InitializeComponent();
        ApplyTheme();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadCollectionsAsync();
    }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);
        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySubtitle(subtitleLabel);
        AppTheme.ApplyInput(searchTextBox);
        AppTheme.ApplySeverityButton(searchButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(refreshButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(confirmButton, ButtonSeverity.Danger);
        AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);

        grid.BackgroundColor = AppTheme.SurfaceColor;
        grid.GridColor = AppTheme.BorderColor;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;
        grid.DefaultCellStyle.Font = AppTheme.BodyFont;
        grid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 251);
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
    }

    private async Task LoadCollectionsAsync(string? searchTerm = null)
    {
        try
        {
            DataTable rows = await CollectionService.GetCollectionsAsync(_role, searchTerm);
            grid.DataSource = rows;
            ApplyGridColumns();
            TrySelectCurrent();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Load Collections", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyGridColumns()
    {
        foreach (DataGridViewColumn column in grid.Columns)
        {
            if (string.Equals(column.Name, "Collection_ID", StringComparison.OrdinalIgnoreCase)
                || string.Equals(column.Name, "collection_id", StringComparison.OrdinalIgnoreCase))
            {
                column.Visible = false;
            }
            else if (column.Name.EndsWith("_At", StringComparison.OrdinalIgnoreCase))
            {
                column.Visible = false;
            }
        }
    }

    private void TrySelectCurrent()
    {
        if (!SelectedCollectionId.HasValue)
        {
            return;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            int id = TryGetCollectionId(row);
            if (id != SelectedCollectionId.Value)
            {
                continue;
            }

            row.Selected = true;
            grid.CurrentCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(cell => cell.Visible);
            return;
        }
    }

    private static int TryGetCollectionId(DataGridViewRow row)
    {
        if (TryGetCellValue(row, "collection_id", out object? collectionIdValue))
        {
            return Convert.ToInt32(collectionIdValue);
        }

        if (TryGetCellValue(row, "Collection_ID", out object? legacyCollectionIdValue))
        {
            return Convert.ToInt32(legacyCollectionIdValue);
        }

        return 0;
    }

    private static bool TryGetCellValue(DataGridViewRow row, string columnName, out object? value)
    {
        value = null;
        if (row.DataGridView is null || !row.DataGridView.Columns.Contains(columnName))
        {
            return false;
        }

        DataGridViewCell cell = row.Cells[columnName];
        if (cell.Value is null or DBNull)
        {
            return false;
        }

        value = cell.Value;
        return true;
    }

    private async void searchButton_Click(object sender, EventArgs e)
    {
        await LoadCollectionsAsync(searchTextBox.Text.Trim());
    }

    private async void refreshButton_Click(object sender, EventArgs e)
    {
        searchTextBox.Clear();
        await LoadCollectionsAsync();
    }

    private void confirmButton_Click(object sender, EventArgs e)
    {
        if (grid.SelectedRows.Count == 0)
        {
            MessageBox.Show(this, "Select a collection record first.", "Void Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        int selectedId = TryGetCollectionId(grid.SelectedRows[0]);
        if (selectedId <= 0)
        {
            MessageBox.Show(this, "Unable to determine selected collection record.", "Void Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SelectedCollectionId = selectedId;
        DialogResult = DialogResult.OK;
        Close();
    }

    private async void searchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        await LoadCollectionsAsync(searchTextBox.Text.Trim());
    }
}
