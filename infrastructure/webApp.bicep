targetScope = 'resourceGroup'

resource webApp 'Microsoft.Web/staticSites@2024-11-01' = {
    location: 'westeurope'
    name: 'boxoffice-flow'
    sku: {
        tier: 'Free'
        name: 'Free'
    }
    properties: {
    }
}
