<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CW._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center list-group">
        <h3>Dashboard</h3>
        <div class="list-group-item">
            <h4 class="text-left">Dishes Served:</h4>
            <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1">
                <Series>
                    <asp:Series Name="Series1" XValueMember="DISH" YValueMembers="ORDERS"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT d.dish_name as Dish,count(d.dish_name) as Orders FROM order_dish o
            INNER JOIN dishes d on o.dish_code=d.dish_code
            GROUP BY d.dish_name"></asp:SqlDataSource>
        </div>

        <div class="list-group-item">
            <h4 class="text-left">Deliveries Status:</h4>
            <asp:Chart ID="Chart2" runat="server" OnLoad="Chart2_Load" DataSourceID="SqlDataSource2">
                <Series>
                    <asp:Series Name="Series1" XValueMember="STATUS" YValueMembers="COUNT"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT status,count(status)as count FROM orders
            GROUP BY status"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
