using System;
using System.Windows.Forms;

/*
    Paratori Net Scan
    Andrea Comuzzi - 18/12/2019
    Versione - 5.2
    (Programma Funzionante, Versione2: Shutdown, Reboot, Forced Reboot, Open C$, Shutdown All,VNC)
    (Fix ver 3.1: Velocizzata scansione diminuendo millisecondi timeout, Sistemati messaggi su PC)
    (Fix ver 3.2: Finestra VNC MaximizedState e Scaled View all'apertura)
    (Fix ver 3.3: Scan all'apertura del programma, Scan ogni 30 Minuti, Tolto messaggio 'Host Disconnected perchè invasivo)
    (Fix ver 3.4: Tolto MessageBox di conteggio Hosts trovati alla ricerca Automatica, Aggiunta Ora LastUpdate con descrizione se Auto o Manuale)
    (Fix ver 4.0: Fix Blocchi vari del Thread 
    (Fix ver 5.0: Cambiata ListView in DataGridView, tutto funzionante; Fix errore Tasto Destro Header di GridView 
                  MANCA: Fix Errore sull'exit, quando si chiude il programma senza chiudere altri form; Switch altro monitor VNC)
    (Fix ver 5.1: Finestra Principale Ingrandita; Aggiunto numero di Host scansionati nell'ultimo Scan fatto 
                  MANCA: Switch altro monitor VNC; Task Killer)
    (Fix ver 5.2: Minimizzo il Programma nel SystemTray alla Riduzione a Icona - Consigliato l'avvio in Esecuzione Automatica di Windows
                  Ordinamento automatico ad ogni Scan per Nome Utente - Scan ogni 20 Minuti)
*/

namespace subnetscan2nd
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
