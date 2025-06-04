using System.Windows.Forms;

public static class ValidationHelper
{
    public static bool TryGetInt(TextBox textBox, out int value, string name)
    {
        if (!int.TryParse(textBox.Text, out value) || value <= 0)
        {
            MessageBox.Show($"Невірне значення для {name}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }
}
