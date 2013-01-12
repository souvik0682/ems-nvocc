<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EMS.WebApp.MasterModule.WebForm1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register src="~/CustomControls/AutoCompleteCountry.ascx" tagname="AutoCompleteExtender" tagprefix="uc1" %>--%>
<%@ Register src="../CustomControls/AutoCompletepPort.ascx" tagname="AutoCompletepPort" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
   
    <%--<uc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" />--%>
     <uc2:AutoCompletepPort ID="AutoCompletepPort1" runat="server" />
</asp:Content>
