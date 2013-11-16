<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditMLVoyage.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditMLVoyage" %>

<%@ Register Src="../CustomControls/AutoCompleteCountry.ascx" TagName="AutoCompleteCountry"
    TagPrefix="uc1" %>
<%@ Register Src="../CustomControls/AutoCompletepPort.ascx" TagName="AutoCompletepPort"
    TagPrefix="uc2" %>
<%--<%@ Register Src="../CustomControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc3" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT ML VOYAGE</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit ML Voyage</legend>
            <table border="0" cellpadding="2" cellspacing="3">
              <tr>
                    <td style="width:120px;">ML Voyage No:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtVoyageNo" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="220"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtVoyageNo" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                 <tr>
                    <td style="width:120px;">Vessel Name:<span class="errormessage1">*</span></td>
                    <td>
                         <asp:DropDownList ID="ddlVessel" runat="server" Width="98%">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlVessel" InitialValue="0" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                
                <%--<tr>
                    <td style="width:120px;">Port:<span class="errormessage1">*</span></td>
                    <td >
                    <div style="width:230px">
                       <uc2:AutoCompletepPort ID="AutoCompletepPort4" runat="server" />
                       </div>
                   </td>
                </tr>

                <tr  >
                    <td style="width:120px;">Activity Date:</td>
                    <td>
                         <asp:TextBox ID="txtdtActivity" runat="server" CssClass="textboxuppercase" Width="220"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtActivity_" Format="dd/MM/yyyy" TargetControlID="txtdtActivity" runat="server" />
                   </td>
                </tr>--%>
             
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                            onclick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
