targetScope = 'subscription'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2025-04-01' = {
    location: 'uksouth'
    name: 'boxofficeflow-rg'
}

module webApp 'webApp.bicep' = {
    name: 'BoxOfficeFlow'
    scope: resourceGroup
}
