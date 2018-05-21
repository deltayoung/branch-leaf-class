using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SinTreeAutoClassificationTester
{
  static class Program
  {
    /*
    Insert into application config file:
     
     <?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.serviceModel>
    <bindings>
      <basicHttpBinding>
        <binding name="LicenseServiceSoap1" />
        <binding name="LicenseServiceSoap2" />
      </basicHttpBinding>
      <customBinding>
        <binding name="LicenseServiceSoap12">
          <textMessageEncoding messageVersion="Soap12" />
          <httpTransport />
        </binding>
      </customBinding>
    </bindings>
    <client>
      <endpoint address="http://localhost:8080/PCSLicenseServer/LicenseService" binding="basicHttpBinding" bindingConfiguration="LicenseServiceSoap1" contract="LicenseServiceReferencePi.LicenseServiceSoap" name="LicenseServiceSoap1" />
      <endpoint address="http://www.digicart.hu/pcs/LicenseService.asmx" binding="customBinding" bindingConfiguration="LicenseServiceSoap12" contract="LicenseServiceReferencePi.LicenseServiceSoap" name="LicenseServiceSoap12" />
      <endpoint address="http://www.digicart.hu/pcs/LicenseService.asmx" binding="basicHttpBinding" bindingConfiguration="LicenseServiceSoap2" contract="LicenseServiceReferenceDC.LicenseServiceSoap" name="LicenseServiceSoap2" />
    </client>
  </system.serviceModel>
</configuration> 
      
     */
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new TesterForm());
    }
  }
}
