function renderDashboard() {

    this.dashboard = BoldBI.create({
        serverUrl: rootUrl + "/" + siteIdentifier,
        dashboardId: "",
        embedContainerId: "",
        embedType: BoldBI.EmbedType.Componet,
        enviroment: enviroment == "enterprise" ? BoldBI.Enviroment.Enterprise : BoldBI.Enviroment.Cloud,
        widht: "100%",
        height: "100%",
        expirationTime: 10000,
        authorizationServer: {
            url: authorizationServerUrl
        },
    });


    console.log(this.dashboard);
    this.dashboard.loadDashBoard();
}