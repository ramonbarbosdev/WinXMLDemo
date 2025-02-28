using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinXMLDemo
{
    public static class Utilities
    {
        public static void IniciarProgresso(ProgressBar progressoBar, int total)
        {
            if (progressoBar.InvokeRequired)
            {
                progressoBar.Invoke(new Action(() => IniciarProgresso(progressoBar, total)));
                return;
            }

            progressoBar.Style = ProgressBarStyle.Blocks;
            progressoBar.Minimum = 0;
            progressoBar.Maximum = total;
            progressoBar.Value = 0;
            progressoBar.Visible = true; 
        }

        public static void AtualizarProgresso(ProgressBar progressoBar, int valor)
        {
            if (progressoBar.InvokeRequired)
            {
                progressoBar.Invoke(new Action(() => AtualizarProgresso(progressoBar, valor)));
                return;
            }

            progressoBar.Value = valor;
        }

        public static void PararProgresso(ProgressBar progressoBar)
        {
            if (progressoBar.InvokeRequired)
            {
                progressoBar.Invoke(new Action(() => PararProgresso(progressoBar)));
                return;
            }

            progressoBar.Visible = true; 
        }
    }
}