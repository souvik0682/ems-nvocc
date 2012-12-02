<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCustomer.aspx.cs" Inherits="EMS.WebApp.View.AddEditCustomer" MasterPageFile="~/Site.Master" Title=":: EMS :: Add / Edit Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }    
    </script>
    <style type="text/css">
        .custtable{width:100%;}
        .custtable td{vertical-align:top;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">

</asp:Content>