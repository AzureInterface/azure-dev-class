[CmdletBinding()]
    Param (
        [Parameter(Mandatory=$true)]
        $StorageAccountName,

        [Parameter(Mandatory=$true)]
        $ResourceGroupName,

        [Parameter(Mandatory=$true)]
        $ConfigPath,

        [Parameter(Mandatory=$true)]
        $ConfigName,

        [Parameter(Mandatory=$true)]
        $VMName
    )

#Push the config to Azure Storage

$pubParams = @{
    ConfigurationPath = $ConfigPath
    ResourceGroupName = $ResourceGroupName
    StorageAccountName = $StorageAccountName
}

Publish-AzureRmVMDscConfiguration @pubParams

#Apply the configuration on the VM

$setParams = @{
    ResourceGroupName = $ResourceGroupName
    ArchiveStorageAccountName = $StorageAccountName
    ArchiveBlobName = "$($ConfigPath.split('\')[-1]).zip"
    VMName = $VMName
    Version = '2.15'
    ConfigurationName = $ConfigName
    WmfVersion = '4.0'
}

Set-AzureRmVMDscExtension @setParams