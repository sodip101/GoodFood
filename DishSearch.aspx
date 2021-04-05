<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DishSearch.aspx.cs" MasterPageFile="~/Site.Master" Inherits="CW.DishSearch" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Dish Search</h1>
        <div class="form-horizontal">
          <div class="form-group p-3">
            <label for="customerName" class="col-sm-2 control-label">Select Dish</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlDish" OnDataBound="ddlDish_DataBound" class="form-control" runat="server" DataSourceID="SqlDataSource1" DataTextField="DISH_NAME" DataValueField="DISH_CODE"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorDish" runat="server" ControlToValidate="ddlDish" ForeColor="Red" ErrorMessage="*Required" InitialValue="Select Dish"></asp:RequiredFieldValidator>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;DISH_CODE&quot;, &quot;DISH_NAME&quot; FROM &quot;DISHES&quot;"></asp:SqlDataSource>
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button ID="Button1" class="btn btn-default" runat="server" Text="Search" OnClick="Button1_Click" />
                <asp:Button ID="Button2" class="btn btn-default" runat="server" Text="Reset" CausesValidation="false" OnClick="Button2_Click"/>
            </div>
          </div>
          <asp:GridView ID="GridView1" class="table table-hover table-bordered" runat="server"  EmptyDataText="No records have been added.">
          </asp:GridView>
        </div>
    </div>
</asp:Content>
