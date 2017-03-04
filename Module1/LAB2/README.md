# Deploy IIS on an Azure VM using the PowerShell DSC Extension

* Create a new virtual machine in a new resource group that will be used as an IIS server. 
Note: it's important that you create this VM in a new resource group, not an existing one 
and that you leave boot diagnostics enabled. We'll use the boot diagnostics storage 
account to host our DSC config.

* RDP to your dev machine. Open PowerShell and use the Login-AzureRMAccount cmdlet to 
authenticate to Azure using your Azure Pass.

* Install the xWebAdministration DSC module (Install-Module xwebadministration)

* Ensure there are no duplicates in the System Environment variable called "PSModulePath". Remove any duplicates if found.

* In the shell, navigate to the azure-dev-class folder to Module1 > Lab2. Run the Push-
DscConfigToVm.ps1 script to push the local DSC config (WindowsWebServer.ps1) to Azure 
Storage and apply the configuration to your new VM. Make sure you fill out all required 
parameters on the Push-DscConfigToVm.ps1 script.

* Verify that IIS has been installed on your new VM by adding a network security group rule 
for HTTP. Navigate to your VM's public ip http://<public-ip>/default.aspx to test.