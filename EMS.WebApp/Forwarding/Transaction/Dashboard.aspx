<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="EMS.WebApp.Farwarding.Transaction.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <script type="text/javascript">
        function popWin(id) {
            alert("test");
            window.open("FileUpload.aspx?Id=" + id, "", "height=300px,toolbar=0,menubar=0,resizable=1,status=1,scrollbars=1"); return false;
        }
        function update(val) {
            alert("File: " + val + " successfully uploaded!");
        }
    </script>
    <div id="headercaption">
        DASHBOARD</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Dashboard</legend>
                <asp:UpdatePanel ID="upBooking" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="maincontainer">
                            <!--start body-->
                            <div class="body">
                                <h3>
                                    Job Details</h3>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    Job Date
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Job Number
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Cargo Source
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCargoSource" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Ops. Controlled by
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOps" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Doc. Controlled by
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDoc" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sales Controlled by
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSales" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Approver
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblApprover" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Closed By
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblClosed" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    Job Type
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobType" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Job Scope
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobScope" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Shipping Mode
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblShipping" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Prime Docs
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPrimeDocs" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table class="table" style="margin-bottom: 0px;">
                                                        <tr>
                                                            <td style="border: 0px;">
                                                                TTL 20'
                                                            </td>
                                                            <td style="border: 0px;">
                                                                TTL 40'
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTTL20" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTTL40" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table class="table" style="margin-bottom: 0px;">
                                                        <tr>
                                                            <td style="border: 0px;">
                                                                Weight (Kgs.)
                                                            </td>
                                                            <td style="border: 0px;">
                                                                Revenue Ton
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    Place of Recv.
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPlaceReceive" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    POL
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPOL" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Place of Delv.
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPlaceDelivery" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    POD
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPOD" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Carrier
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblCarrier" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Customer
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Customs Ag
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblCustomerAgent" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Transporter
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblTransporter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Overseas Ag
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblOverseas" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <h3>
                                    Job Summary</h3>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    Total Estimate Payable (INR)
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalEstimatePayable" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Total Paid (INR)
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalPaid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    Total Estimate Recieveable (INR)
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalEstimateReceiveable" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Total Received (INR)
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalReceived" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    Projected Gross Profit
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProjectedGrossProfit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Achieved Gross Profit
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblArchievedGrossProfit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary btn-xs"
                                                        OnClick="btnApprove_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnCloseJob" runat="server" Text="Close Job" CssClass="btn btn-primary btn-xs"
                                                        OnClick="btnCloseJob_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="bs-exa mple">
                                    <div class="panel-group" id="accordion">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Estimate Payable
                                                        Particulars</a>
                                                </h4>
                                            </div>
                                            <div id="collapseTwo" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAdvPayment" runat="server" Text="Advance Payment" CssClass="btn btn-primary"
                                                                OnClick="btnAdvPayment_Click" />
                                                            <asp:Button ID="btnAddPayable" runat="server" Text="Add Payable" CssClass="btn btn-primary"
                                                                OnClick="btnAddPayable_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvEstimatePayable" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                            BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvEstimatePayable_RowDataBound"
                                                            OnRowCommand="gvEstimatePayable_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblUnitType" runat="server" Text="Unit Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblTotalUnit" runat="server" Text="Total Units"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblBillFrom" runat="server" Text="Bill From"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblChargeAmt" runat="server" Text="Charge Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblRoe" runat="server" Text="ROE"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblPaymentBy" runat="server" Text="Payment By"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAmt" runat="server" Text="Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnBillReceipt" runat="server" CommandName="BillReceipt" ImageUrl="~/Images/status.jpg"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnUpload" runat="server" CommandName="Upload" ImageUrl="~/Images/add.jpeg"
                                                                            Height="16" Width="16"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16"  OnClientClick="javascript:return confirm('Are you sure about delete?');"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree">Estimate Receivable
                                                        Particulars</a>
                                                </h4>
                                            </div>
                                            <div id="collapseThree" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAdvanceReceipt" runat="server" Text="Advance Receipt" CssClass="btn btn-primary"
                                                                OnClick="btnAdvanceReceipt_Click" />
                                                            <asp:Button ID="btnAddRecovery" runat="server" Text="Add Recovery" CssClass="btn btn-primary"
                                                                OnClick="btnAddRecovery_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvEstimateReceivable" runat="server" AutoGenerateColumns="false"
                                                            AllowPaging="false" BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvEstimateReceivable_RowDataBound"
                                                            OnRowCommand="gvEstimateReceivable_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblUnitType" runat="server" Text="Unit Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblTotalUnit" runat="server" Text="Total Units"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblBillFrom" runat="server" Text="Bill From"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblChargeAmt" runat="server" Text="Charge Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblRoe" runat="server" Text="ROE"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblPaymentBy" runat="server" Text="Payment By"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAmt" runat="server" Text="Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnGenInv" runat="server" CommandName="GenInv" ImageUrl="~/Images/status.jpg"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnUpload" runat="server" CommandName="Upload" ImageUrl="~/Images/add.jpeg"
                                                                            Height="16" Width="16"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16"  OnClientClick="javascript:return confirm('Are you sure about delete?');"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour">Creditors Invoice</a>
                                                </h4>
                                            </div>
                                            <div id="collapseFour" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAddInvoiceCred" runat="server" Text="Add Invoice" CssClass="btn btn-primary"
                                                                OnClick="btnAddInvoiceCred_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvCreditors" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                            BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvCreditors_RowDataBound"
                                                            OnRowCommand="gvCreditors_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblPartyName" runat="server" Text="Party Name"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceType" runat="server" Text="Invoice Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceAmt" runat="server" Text="Invoice Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblROE" runat="server" Text="ROE"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblINRAmt" runat="server" Text="INR Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16" OnClientClick="javascript:return confirm('Are you sure about delete?');" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnPayment" runat="server" CommandName="Payment" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive">Debtors Invoice</a>
                                                </h4>
                                            </div>
                                            <div id="collapseFive" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAddInvoiceDebt" runat="server" Text="Add Invoice" CssClass="btn btn-primary"
                                                                OnClick="btnAddInvoiceDebt_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvDebtors" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                            BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvDebtors_RowDataBound"
                                                            OnRowCommand="gvDebtors_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceType" runat="server" Text="Invoice Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkInvoice" runat="server" CommandName="Invoice" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceAmt" runat="server" Text="Invoice Amt"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblReceivedAmt" runat="server" Text="Received Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblCRNAmt" runat="server" Text="CRN Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text="Balance Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnPrint" runat="server" CommandName="Print" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAddMR" runat="server" CommandName="AddMR" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAddCRN" runat="server" CommandName="AddCRN" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end footer-->
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressBooking" runat="server" AssociatedUpdatePanelID="upBooking">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>
