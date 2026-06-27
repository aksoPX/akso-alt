using BrightIdeasSoftware;
using RBX_Alt_Manager.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
public static class AksoStyle
{
    public static readonly Color Accent = Color.FromArgb(37, 99, 235);
    public static readonly Color AccentHover = Color.FromArgb(59, 130, 246);
    public static readonly Color Danger = Color.FromArgb(239, 68, 68);
    public static readonly Color PanelDark = Color.FromArgb(15, 23, 42);
    public static readonly Color PanelSoft = Color.FromArgb(17, 24, 39);
    public static readonly Color Border = Color.FromArgb(51, 65, 85);
    public static readonly Color Text = Color.FromArgb(226, 232, 240);
    public static readonly Color MutedText = Color.FromArgb(148, 163, 184);

    public static void Apply(Form form)
    {
        if (form == null) return;

        try
        {
            form.Font = new Font("Segoe UI", Math.Max(8.5f, form.Font.SizeInPoints), FontStyle.Regular, GraphicsUnit.Point);
            form.BackColor = ThemeEditor.FormsBackground;
            form.ForeColor = ThemeEditor.FormsForeground;
            ApplyControls(form.Controls);
        }
        catch
        {
            // Styling should never break the app.
        }
    }

    public static void ApplyControls(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            ApplyControl(control);

            if (control.HasChildren)
                ApplyControls(control.Controls);
        }
    }

    private static void ApplyControl(Control control)
    {
        if (control == null) return;

        if (control is Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = ThemeEditor.ButtonsBorder;
            button.FlatAppearance.MouseOverBackColor = AccentHover;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(29, 78, 216);
            button.BackColor = ThemeEditor.ButtonsBackground;
            button.ForeColor = ThemeEditor.ButtonsForeground;
            button.Cursor = Cursors.Hand;
            button.Padding = new Padding(3, 1, 3, 1);
        }
        else if (control is CheckBox checkBox)
        {
            checkBox.FlatStyle = FlatStyle.Flat;
            checkBox.ForeColor = ThemeEditor.FormsForeground;
            checkBox.Cursor = Cursors.Hand;
        }
        else if (control is TextBoxBase textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = ThemeEditor.TextBoxesBackground;
            textBox.ForeColor = ThemeEditor.TextBoxesForeground;
        }
        else if (control is ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = ThemeEditor.TextBoxesBackground;
            comboBox.ForeColor = ThemeEditor.TextBoxesForeground;
        }
        else if (control is ObjectListView objectListView)
        {
            objectListView.BackColor = ThemeEditor.AccountBackground;
            objectListView.ForeColor = ThemeEditor.AccountForeground;
            objectListView.GridLines = false;
            objectListView.FullRowSelect = true;
        }
        else if (control is ListBox || control is ListView || control is TreeView)
        {
            control.BackColor = ThemeEditor.AccountBackground;
            control.ForeColor = ThemeEditor.AccountForeground;
        }
        else if (control is TabControl tabControl)
        {
            tabControl.BackColor = ThemeEditor.FormsBackground;
            tabControl.ForeColor = ThemeEditor.FormsForeground;
        }
        else if (control is Panel || control is FlowLayoutPanel || control is TableLayoutPanel)
        {
            control.BackColor = ThemeEditor.FormsBackground;
        }
        else if (control is Label label)
        {
            label.ForeColor = ThemeEditor.LabelForeground;
        }
    }
}
