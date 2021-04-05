<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderActivity.aspx.cs" MasterPageFile="~/Site.Master" Inherits="CW.OrderActivity" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>
        <h1>Customer Order Activity</h1>
        <div class="form-horizontal">
          <div class="form-group p-3">
            <label for="customerName" class="col-sm-2 control-label">Select Customer</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlCustomer" class="form-control" OnDataBound="ddlCustomer_DataBound" runat="server" DataSourceID="SqlDataSource1" DataTextField="FULL_NAME" DataValueField="CUSTOMER_ID"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCustomer" runat="server" ControlToValidate="ddlCustomer" InitialValue="Select Customer" ForeColor="Red" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_ID&quot;, &quot;FULL_NAME&quot; FROM &quot;CUSTOMERS&quot;"></asp:SqlDataSource>
            </div>
          </div>
          <div class="form-group">
            <label for="lpdate" class="col-sm-2 control-label">Month Start Date</label>
            <div class="col-sm-10">
               <asp:TextBox ID="txtStartDate" class="form-control" CausesValidation="true" placeholder="dd-mmm-yy" ReadOnly="true" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorStartDate" runat="server" ControlToValidate="txtStartDate" ForeColor="Red" ErrorMessage="*Required"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
               <asp:Button ID="Button3" class="btn btn-default btn-sm" OnClick="Button3_Click" CausesValidation="false" runat="server" Text="Pick Start Date"/>
               <span class="glyphicon glyphicon-calendar"></span>
                <asp:CustomValidator ID="CustomValidatorStartDate" Display="Dynamic" runat="server" ControlToValidate="txtStartDate" ErrorMessage="*Start Date cannot be greater than End Date" ForeColor="Red" OnServerValidate="CustomValidatorStartDate_ServerValidate" ></asp:CustomValidator>
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
              <asp:Calendar ID="startDate" OnSelectionChanged="startDate_SelectionChanged" runat="server"></asp:Calendar>
            </div>
          </div>

          <div class="form-group">
            <label for="lpdate" class="col-sm-2 control-label">Month End Date</label>
            <div class="col-sm-10">
               <asp:TextBox ID="txtEndDate" class="form-control" placeholder="dd-mmm-yy" ReadOnly="true" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorEndDate" runat="server" ControlToValidate="txtEndDate" ForeColor="Red" ErrorMessage="*Required"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
               <asp:Button ID="Button4" class="btn btn-default btn-sm" OnClick="Button4_Click" CausesValidation="false" runat="server" Text="Pick End Date"/>
               <span class="glyphicon glyphicon-calendar"></span>
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
              <asp:Calendar ID="endDate" OnSelectionChanged="endDate_SelectionChanged" runat="server"></asp:Calendar>
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
    </div>
</asp:Content>
