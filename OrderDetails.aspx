<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" MasterPageFile="~/Site.Master" Inherits="CW.OrderDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Order Details</h1>
        <div class="form-horizontal">
          <div class="form-group p-3">
            <label for="customerName" class="col-sm-2 control-label">Select Customer</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlCustomer" OnDataBound="ddlCustomer_DataBound" class="form-control" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="FULL_NAME" DataValueField="CUSTOMER_ID"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCustomer" runat="server" ControlToValidate="ddlCustomer" ForeColor="Red" ErrorMessage="*Required" InitialValue="Select Customer"></asp:RequiredFieldValidator>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_ID&quot;, &quot;FULL_NAME&quot; FROM &quot;CUSTOMERS&quot;"></asp:SqlDataSource>
            </div>
          </div>
          <div class="form-group">
            <label for="restaurantEmail" class="col-sm-2 control-label">Select Order</label>
            <div class="col-sm-10">
              <asp:DropDownList ID="ddlOrder" class="form-control" runat="server"></asp:DropDownList>
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button ID="Button1" class="btn btn-default" runat="server" Text="Search" OnClick="Button1_Click" />
                <asp:Button ID="Button2" class="btn btn-default" runat="server" Text="Reset" CausesValidation="false" OnClick="Button2_Click" />
            </div>
          </div>
          <asp:GridView ID="GridView1" class="table table-hover table-bordered" runat="server"  EmptyDataText="No records have been added.">
          </asp:GridView>
        </div>
    </div>
</asp:Content>
