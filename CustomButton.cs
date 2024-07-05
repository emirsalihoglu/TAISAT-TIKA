using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomButton : Button
{
    public CustomButton()
    {
        ApplyDefaultStyle(this);
    }

    public static void SetButton(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is Button button)
            {
                ApplyDefaultStyle(button);
            }

            if (control.HasChildren)
            {
                SetButton(control.Controls);
            }
        }
    }

    public static void SetButtonColors(Button[] buttons, Color backColor, Color foreColor)
    {
        foreach (Button button in buttons)
        {
            ApplyCustomColors(button, backColor, foreColor);
        }
    }

    public static void SetButtonMouseEvents(Button[] buttons, Color mouseOverBackColor, Color mouseDownBackColor)
    {
        foreach (Button button in buttons)
        {
            ApplyMouseEvents(button, mouseOverBackColor, mouseDownBackColor);
        }
    }

    private static void ApplyDefaultStyle(Button button)
    {
        button.BackColor = Color.FromArgb(47, 49, 54);
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 68, 75);
        button.FlatAppearance.MouseOverBackColor = Color.FromArgb(54, 57, 63);
        button.FlatAppearance.BorderColor = Color.FromArgb(32, 34, 37);
        button.Cursor = Cursors.Hand;
    }

    private static void ApplyCustomColors(Button button, Color backColor, Color foreColor)
    {
        button.BackColor = backColor;
        button.ForeColor = foreColor;
    }

    private static void ApplyMouseEvents(Button button, Color mouseOverBackColor, Color mouseDownBackColor)
    {
        button.FlatAppearance.MouseOverBackColor = mouseOverBackColor;
        button.FlatAppearance.MouseDownBackColor = mouseDownBackColor;
    }
}