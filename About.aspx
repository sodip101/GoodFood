<%@ Page Title="About Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CW._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>GoodFood</h2>
        <p class="lead">
            GoodFood is a virtual company, available in web and mobile platforms that
            provides an online food ordering and delivery system.
        </p>
        <p>
            GoodFood has many restaurants registered into its system. Customers can browse
            through restaurants or their favourite dishes to check price as well as availability of dishes.
        </p>
    </div>

    <div class="row text-center">
        <div class="col-md-4">
            <h3>Multiple Restaurants</h3>
            <img class="img-responsive" src="img/food.png">
        </div>
        <div class="col-md-4">
            <h3>Multi-Platform</h3>
            <img class="img-responsive" src="img/delivery.png">
        </div>
        <div class="col-md-4">
            <h3>Free Delivery</h3>
            <img class="img-responsive" src="img/app.png">
        </div>
    </div>

</asp:Content>
