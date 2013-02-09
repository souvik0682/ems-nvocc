<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditVessel.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditVessel" %>

<%@ Register src="../CustomControls/AutoCompleteCountry.ascx" tagname="AutoCompleteCountry" tagprefix="uc1" %>
<%@ Register src="../CustomControls/AutoCompletepPort.ascx" tagname="AutoCompletepPort" tagprefix="uc2" %>


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
        ADD / EDIT VESSEL</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Vessel</legend>
            <table border="0" cellpadding="2" cellspacing="3">
              <tr>
                    <td>Vessel Prefix:<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlVesselPrefix" runat="server" Width="100%"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td style="width:140px;">Vessel Name:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtVesselName" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtVesselName" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td>Vessel Flag:<span class="errormessage1">*</span></td>
                    <td style="width:50%">
                      
                        <uc1:AutoCompleteCountry ID="AutoCompleteCountry1" runat="server"  />
                       
                   </td>
                </tr>
                
                  <%--<tr>
                    <td>Call Sign:</td>
                    <td><asp:TextBox ID="txtCallSign" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                   </td>
                </tr>--%>

                  <tr>
                    <td>IMO Number</td>
                    <td><asp:TextBox ID="txtIMO" runat="server" CssClass="textboxuppercase" 
                            MaxLength="14" Width="250" ></asp:TextBox><br />
                   </td>
                </tr>

                  <tr>
                    <td>Shipping Line Code:</td>
                    <td><asp:TextBox ID="txtShipLineCode" runat="server" CssClass="textboxuppercase"
                            MaxLength="10" Width="250" ></asp:TextBox><br />
                   </td>
                </tr>
                
                  <tr>
                    <td>PAN No:</td>
                    <td><asp:TextBox ID="txtPan" runat="server" CssClass="textboxuppercase"
                            MaxLength="15" Width="250" TextMode="SingleLine"></asp:TextBox><br />
                   </td>
                </tr>

                  <tr>
                    <td>Master Name:</td>
                    <td><asp:TextBox ID="txtMasterCode" runat="server" CssClass="textboxuppercase" 
                            MaxLength="50" Width="250" TextMode="SingleLine"></asp:TextBox><br />
                   </td>
                </tr>
                
                  <tr>
                    <td>Agent Code:</td>
                    <td><asp:TextBox ID="txtAgentCode" runat="server" CssClass="textboxuppercase" 
                            MaxLength="10" Width="250" TextMode="SingleLine"></asp:TextBox><br />
                   </td>
                </tr>

                 <%--<tr>
                    <td>Last Port:<span class="errormessage1">*</span></td>
                    <td>
                        <uc2:AutoCompletepPort ID="AutoCompletepPort1" runat="server" />
&nbsp;<br />
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