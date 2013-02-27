<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EDI_Custom.aspx.cs" Inherits="EMS.WebApp.Import.EDI_Custom" %>
    <%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc1" %>
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
        EDI for ICEGATE</div>
    <center>
        <fieldset style="width:800px; ">
            <legend>EDI for ICEGATE</legend>
            <table border="0" cellpadding="2" cellspacing="3">
             <tr>
              <td style="width:50%; vertical-align:top;" >
                 <table width="100%">
                <tr>
                    <td style="width:140px;">Vessel Name:<span class="errormessage1">*</span></td>
                    <td>
                         <asp:DropDownList ID="ddlVessel" runat="server" Width="90%" AutoPostBack="True" 
                             onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlVessel" Display="Dynamic" InitialValue="0" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td style="width:140px;">Voyage No:<span class="errormessage1">*</span></td>
                    <td><asp:DropDownList ID="ddlVoyage" runat="server" Width="90%" AutoPostBack="True" 
                            onselectedindexchanged="ddlVoyage_SelectedIndexChanged">
                        </asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" InitialValue="0" 
                            ControlToValidate="ddlVoyage" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td style="width:140px;">Call Sign</td>
                    <td><asp:TextBox ID="txtCallSign" runat="server" ReadOnly="true" CssClass="textboxuppercaseRO" MaxLength="60" Width="250"></asp:TextBox><br />
                   </td>
                </tr>

                <tr>
                    <td>Message Type:</td>
                    <td>
                      <asp:DropDownList ID="ddlMsgType" runat="server" Width="90%">
                      <asp:ListItem Value="Fresh">Fresh</asp:ListItem>
                       <asp:ListItem Value="Ammendments">Ammendments</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td style="width:140px;">IGM No:</td>
                    <td><asp:TextBox ID="txtIGMNo" runat="server" CssClass="textboxuppercaseRO" ReadOnly="true" MaxLength="10" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                               
                <tr>
                    <td style="width:140px;">IGM Date:</td>
                    <td>
                        <%--<%@ Register Src="../CustomControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc3" %>--%>
                           <asp:TextBox ID="txtDtIGM" CssClass="textboxuppercaseRO"  runat="server" ReadOnly="true"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtIGM_" TargetControlID="txtDtIGM" runat="server" />
                   </td>
                </tr>

                <tr>
                    <td style="width:140px;">Shipping Line Code:</td>
                    <td><asp:TextBox ID="txtShipCode" runat="server" CssClass="textboxuppercaseRO" ReadOnly="true" MaxLength="15" Width="250"></asp:TextBox><br />
                   </td>
                </tr>

                <tr>
                    <td style="width:140px;">Pan No:</td>
                    <td><asp:TextBox ID="txtPAN" runat="server" CssClass="textboxuppercaseRO" ReadOnly="true" MaxLength="13" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Master Name:</td>
                    <td><asp:TextBox ID="txtMaster" runat="server" CssClass="textboxuppercaseRO" ReadOnly="true" MaxLength="20" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                <tr>
                    <td style="width:140px;">Vessel Flag:</td>
                    <td><asp:TextBox ID="txtVesselFlag" runat="server" CssClass="textboxuppercaseRO" ReadOnly="true" MaxLength="20" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                 <tr>
                    <td style="width:140px;">Last Port Called:</td>
                    <td>
                    <%--<asp:TextBox ID="txtLastPort" runat="server" CssClass="textboxuppercase" ReadOnly="true" MaxLength="20" Width="250">
                    </asp:TextBox>--%>
                      <div style="width:230px">
                       <uc2:AutoCompletepPort ID="AutoCompletepPort2"  runat="server" />
                       </div>
                  
                   </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Port Last But One:</td>
                    <td>
                    <%--<asp:TextBox ID="txtPortBefore1" runat="server" CssClass="textboxuppercase"  Width="250"></asp:TextBox>--%>
                      <div style="width:230px">
                       <uc2:AutoCompletepPort ID="AutoCompletepPort3" runat="server" />
                       </div>
                   
                   </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Port Last But Two:</td>
                    <td>
                    <%--<asp:TextBox ID="txtPortBefore2" runat="server" CssClass="textboxuppercase"  Width="250"></asp:TextBox>--%>
                      <div style="width:230px">
                       <uc2:AutoCompletepPort ID="AutoCompletepPort4" runat="server" />
                       </div>
                    
                   </td>
                </tr>
                 <tr>
                    <td>Customs House Code:</td>
                    <td >
                    <div style="width:230px">
                        <%--<uc2:AutoCompletepPort ID="AutoCompletepPort1" runat="server" />--%>
                        <asp:DropDownList ID="ddlCustomHouse" Width="70%" runat="server"></asp:DropDownList>
                        </div>
                        
                    </td>
                </tr>


            </table>

            </td>
           
              <td style="width:50%; vertical-align:top;" >
                 <table width="100%">
                 <tr>
                    <td style="width:140px;">IMO Number:</td>
                    <td><asp:TextBox ID="txtIMONo" runat="server" CssClass="textboxuppercase" MaxLength="5" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                <tr>
                    <td>Vessel Type:</td>
                    <td>
                      <asp:DropDownList ID="ddlVesselType" runat="server" Width="70%">
                          <asp:ListItem Value="C">Cargo</asp:ListItem>
                          <asp:ListItem Value="E">Empty</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td style="width:140px;">Total Lines: <span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtTotLine" runat="server" CssClass="textboxuppercase"  MaxLength="15" Width="250"></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtTotLine" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>
                   </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Cargo Description</td>
                    <td><asp:TextBox ID="txtCargoDesc"  runat="server" CssClass=""  MaxLength="500" Width="250"></asp:TextBox><br />
                   </td>
                </tr>
                 <tr>
                    <td style="width:140px;">Arrival Date </td>
                    <td> <%--<uc3:DatePicker ID="dtIGM" runat="server" />--%>
                         <asp:TextBox ID="txtdtArrival" runat="server"  CssClass="textboxuppercase" Width="100"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtArrival_" TargetControlID="txtdtArrival" runat="server"  />
                        Time <asp:TextBox ID="txtArriveTime" runat="server" width="100"></asp:TextBox>
                         <cc2:TextBoxWatermarkExtender ID="txtWMEAbbr" runat="server" TargetControlID="txtArriveTime" WatermarkText="hh:mm"></cc2:TextBoxWatermarkExtender>
                   </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Light house Due</td>
                    <td>
                    <%--<asp:TextBox ID="txtLightHouse" runat="server" CssClass="numerictextbox"  MaxLength="10" Width="160"  onkeyup="IsNumeric(this)"></asp:TextBox>--%>
                    <cc1:CustomTextBox ID="txtLightHouse" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="13" Precision="10" Scale="2" Width="100"></cc1:CustomTextBox>
                    <br />
                   </td>
                </tr>
                
                <tr>
                    <td>Same Bottom Cargo:</td>
                    <td>
                      <asp:DropDownList ID="ddlSameButton" runat="server" Width="50%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
        <tr>
                    <td>Ship Store Submitted:</td>
                    <td>
                      <asp:DropDownList ID="ddlShipStoreSubmitted" runat="server" Width="50%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
        <tr>
                    <td>Crew List:</td>
                    <td>
                      <asp:DropDownList ID="ddlCrewList" runat="server" Width="50%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
        <tr>
                    <td>Passenger List:</td>
                    <td>
                      <asp:DropDownList ID="ddlPessengerList" runat="server" Width="50%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
        <tr>
                    <td>Crew Effective List:</td>
                    <td>
                      <asp:DropDownList ID="ddlCrewEffList" runat="server" Width="50%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr> 
                <tr>
                    <td>Maritime List:</td>
                    <td>
                      <asp:DropDownList ID="ddlMaritime" runat="server" Width="50%">
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                          <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Terminal Operator:</td>
                    <td>
                      <asp:DropDownList ID="ddlTerminalOperator" runat="server" Width="90%">
                         
                      </asp:DropDownList>
                    </td>
                </tr>
                 </table>
                 </td>
            </tr>
                  <tr>
                    <td colspan="2" style="text-align:center" >
                      <center>
                        <asp:Button ID="btnDownLoad" runat="server" CssClass="button" onclick="btnDownLoad_Click" ValidationGroup="Save"
                            Text="Download" />
                      </center>
                    </td>
                </tr>
            </table>
          
        </fieldset>
    </center>
</asp:Content>
