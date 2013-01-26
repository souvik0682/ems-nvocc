<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditVoyage.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditVoyage" %>

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

        function CheckAdditional() {

            if (document.getElementById('container_AutoCompletepPort1_txtPort').value == 'HALDIA,INHAL1') {
                document.getElementById('AdLand').style.display = '';
                document.getElementById('AddGur').style.display = '';
            }
        }

        function CheckExchRate() {
            var exRateOri = parseFloat(document.getElementById('container_hdntxtExcRate').value);
            var exRateCurr = parseFloat(document.getElementById('container_txtExcRate').value);

            if (exRateCurr < exRateOri || (exRateCurr - exRateOri) > .50) {
                document.getElementById('container_txtExcRate').value = exRateOri;
                alert('Invalid Exchange Rate');
                return;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT VOYAGE</div>
    <center>
        <fieldset style="width:810px; ">
            <legend>Add / Edit Voyage</legend>
            <table border="0" cellpadding="2" cellspacing="3">
            <tr>
              <td style="width:50%; vertical-align:top;" >
                 <table width="100%">
                <tr>
                    <td>Location:<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlLoc" runat="server" Width="100%" AutoPostBack="True" 
                            onselectedindexchanged="ddlLoc_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:140px;">Vessel Name:<span class="errormessage1">*</span></td>
                    <td>
                         <asp:DropDownList ID="ddlVessel" runat="server" Width="100%">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlVessel" InitialValue="0" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Terminal ID:</td>
                    <td>
                      <asp:DropDownList ID="ddlTerminalID" runat="server" Width="100%" 
                           ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:140px;">IGM No:</td>
                    <td><asp:TextBox ID="txtIGMNo" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                <tr  >
                    <td style="width:140px;">Landing Date:</td>
                    <td>
                        <%--<uc3:DatePicker ID="dtLand" runat="server" />--%>
                         <asp:TextBox ID="txtdtLand" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtLand_" Format="dd/MM/yyyy" TargetControlID="txtdtLand" runat="server" />
                   </td>
                </tr>
                <tr>
                    <td>Vessel Type:</td>
                    <td>
                      <asp:DropDownList ID="ddlVesselType" runat="server" Width="100%">
                          <asp:ListItem Value="C">Cargo</asp:ListItem>
                          <asp:ListItem Value="E">Empty</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:140px;">Total Lines</td>
                    <td><asp:TextBox ID="txtTotLine" runat="server" CssClass="textboxuppercase" MaxLength="5" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
        		<tr>
                    <td style="width:140px;">Call Sign</td>
                    <td><asp:TextBox ID="txtCallSign" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                <tr>
                    <td style="width:140px;">ETA Date </td>
                    <td> <%--<uc3:DatePicker ID="dtETA" runat="server" />--%>
                        <asp:TextBox ID="txtdtETA" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtETA_" TargetControlID="txtdtETA" Format="dd/MM/yyyy" runat="server" />
                   </td>
                </tr>
                <tr>
                    <td style="width:140px;">ETA Time </td>
                    <td> 
                      <asp:TextBox ID="txtTime" runat="server" CssClass="textboxuppercase" MaxLength="5" Width="150"></asp:TextBox>
                      <cc2:TextBoxWatermarkExtender ID="txtWMEAbbr" runat="server" TargetControlID="txtTime" WatermarkText="hh:mm" WatermarkCssClass="watermark1"></cc2:TextBoxWatermarkExtender>
                   </td>
                </tr>
                <tr>
                    <td>Same Button Cargo:</td>
                    <td>
                      <asp:DropDownList ID="ddlSameButton" runat="server" Width="40%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                 
        <tr>
                    <td>Crew List:</td>
                    <td>
                      <asp:DropDownList ID="ddlCrewList" runat="server" Width="40%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
        <tr>
                    <td>Crew Effective List:</td>
                    <td>
                      <asp:DropDownList ID="ddlCrewEffList" runat="server" Width="40%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td style="width:140px;">PCC No</td>
                    <td><asp:TextBox ID="txtPCCNo" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                <tr>
                    <td style="width:140px;">VIA No</td>
                    <td><asp:TextBox ID="txtVIA" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                <tr>
                    <td style="width:140px;">Cargo Description</td>
                    <td><asp:TextBox ID="txtCargoDesc" TextMode="MultiLine" Text="GENERAL" runat="server" CssClass="" MaxLength="14" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                <tr runat="server" id="trLandDate">
                    <td style="width:140px;">Land Gurantee No.</td>
                    <td><asp:TextBox ID="txtLGNo" runat="server" CssClass="textboxuppercase" MaxLength="40" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
	            </table>
              </td>
              <td style="width:50%; vertical-align:top; ">
              <table width="100%">
               <tr>
                    <td style="width:140px;">Exchange Rate:<span class="errormessage1">*</span>
                    <input id="hdntxtExcRate" type="hidden" runat="server" />
                    </td>
                    <td><asp:TextBox ID="txtExcRate" runat="server" CssClass="textboxuppercase" style="text-align:right" MaxLength="60" Width="150"  onkeyup="IsDecimal(this)" onblur="CheckExchRate()"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtExcRate" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
              <tr>
                    <td style="width:140px;">Voyage No:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtVoyageNo" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtVoyageNo" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
              <tr>
                    <td style="width:140px;">IGM Date:</td>
                    <td>
                       <%-- <uc3:DatePicker ID="dtIGM" runat="server" />--%>
                        <asp:TextBox ID="txtDtIGM" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtIGM_" TargetControlID="txtDtIGM" Format="dd/MM/yyyy" runat="server" />
                   </td>
                </tr>

                  <tr>
                    <td style="width:140px;">Last Port Called.</td>
                    <td  onkeyup="CheckAdditional()">
                    <div style="width:230px">
                       <uc2:AutoCompletepPort ID="AutoCompletepPort1" runat="server" />
                       </div>
                   </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Port Last but One</td>
                    <td >
                     <div style="width:230px">
                        <uc2:AutoCompletepPort ID="AutoCompletepPort2" runat="server" />
                        </div>
                   </td>
                </tr>
                 <tr>
                    <td style="width:140px;">Port Last but Two</td>
                    <td  >  
                        <div style="width:230px">
                        <uc2:AutoCompletepPort ID="AutoCompletepPort3" runat="server" />
                       </div>
                      
                   </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Light house Due</td>
                    <td><asp:TextBox ID="txtLightHouse" runat="server" style="text-align:right" CssClass="textboxuppercase" MaxLength="10" Width="150"  onkeyup="IsNumeric(this)"></asp:TextBox><br />
                   </td>
                </tr>
                <tr>
                    <td>Ship Store Submitted:</td>
                    <td>
                      <asp:DropDownList ID="ddlShipStoreSubmitted" runat="server" Width="40%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Passenger List:</td>
                    <td>
                      <asp:DropDownList ID="ddlPessengerList" runat="server" Width="40%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                  <tr>
                    <td>Maritime List:</td>
                    <td>
                      <asp:DropDownList ID="ddlMaritime" runat="server" Width="40%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
		   
			
  
      
     
          
       
        <tr>
                    <td style="width:140px;" >PCC Date</td>
                    <td> <%--<uc3:DatePicker ID="dtPCC" runat="server" />--%>
                     <asp:TextBox ID="txtdtPCC" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtPCC_" TargetControlID="txtdtPCC" Format="dd/MM/yyyy" runat="server" />
                    <br />
                   </td>
                </tr>
       
        <tr>
                    <td style="width:140px;">VCN</td>
                    <td><asp:TextBox ID="txtVCN" runat="server" CssClass="textboxuppercase" MaxLength="14" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
        
       
         <tr>
                    <td style="width:140px;">Mother Daughter</td>
                    <td><asp:TextBox ID="txtMotherDaughter" TextMode="MultiLine" runat="server" CssClass="" MaxLength="14" Width="250"></asp:TextBox><br />
                   </td>
                </tr>

                   <tr id="AdLand" style="display:none">
                    <td style="width:140px;">Add. Landing Date:</td>
                    <td>
                        <%--<uc3:DatePicker ID="dtAddLand" runat="server" />--%>
                          <asp:TextBox ID="txtdtAddLand" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtAddLand_" TargetControlID="txtdtAddLand" Format="dd/MM/yyyy" runat="server" />
                   </td>
                </tr>
               <tr id="AddGur" style="display:none">
                    <td style="width:140px;">Addl. Gurantee No</td>
                    <td><asp:TextBox ID="txtAltLGNo" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
              </table>
              </td>
            </tr>
         <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" onclick="btnSave_Click1" 
                              />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                             />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
