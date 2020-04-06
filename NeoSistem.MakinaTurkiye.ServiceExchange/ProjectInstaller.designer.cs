namespace NeoSistem.MakinaTurkiye.ExchangeService
{
    partial class ProjectInstaller
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.ExchangeProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
      this.ExchangeServiceInstaller = new System.ServiceProcess.ServiceInstaller();
      // 
      // ExchangeProcessInstaller
      // 
      this.ExchangeProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
      this.ExchangeProcessInstaller.Password = null;
      this.ExchangeProcessInstaller.Username = null;
      // 
      // ExchangeServiceInstaller
      // 
      this.ExchangeServiceInstaller.ServiceName = "Makina Türkiye Kur Servisi";
      this.ExchangeServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ExchangeProcessInstaller,
            this.ExchangeServiceInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller ExchangeProcessInstaller;
    private System.ServiceProcess.ServiceInstaller ExchangeServiceInstaller;
  }
}