targetScope = 'resourceGroup'

resource webApp 'Microsoft.Web/staticSites@2024-11-01' = {
    location: 'westeurope'
    name: 'boxoffice-flow'
    sku: {
        tier: 'free'
        name: 'free'
    }
}
