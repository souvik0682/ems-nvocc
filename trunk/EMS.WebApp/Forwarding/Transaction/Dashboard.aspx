<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="EMS.WebApp.Farwarding.Transaction.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    
    <div id="headercaption">
        ADD / EDIT JOB</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Job</legend>
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
                                                    20-06-2014
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Job Number
                                                </td>
                                                <td>
                                                    SEA13120005
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Cargo Source
                                                </td>
                                                <td>
                                                    Nomination
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Ops. Controlled by
                                                </td>
                                                <td>
                                                    Mumbai
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Doc. Controlled by
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Approver
                                                </td>
                                                <td>
                                                    Display name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Closed By
                                                </td>
                                                <td>
                                                    Display name
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
                                                    Sea Export
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Job Scope
                                                </td>
                                                <td>
                                                    Port-to-Port
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Shipping Mode :
                                                </td>
                                                <td>
                                                    Master Bill of Lading
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
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                1
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
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                1.00
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
                                                    NHAVA SHEVA
                                                </td>
                                                <td>
                                                    POL
                                                </td>
                                                <td>
                                                    INNSA1
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Place of Delv.
                                                </td>
                                                <td>
                                                    NHAVA SHEVA
                                                </td>
                                                <td>
                                                    POD
                                                </td>
                                                <td>
                                                    SNTIM
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Carrier
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Customer
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Customs Ag
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Transporter
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Overseas Ag
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
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
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Total Paid (INR)
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    Total Estimate Payable (INR)
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Total Received (INR)
                                                </td>
                                                <td>
                                                    &nbsp;
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
                                                    <button type="button" class="btn btn-primary btn-xs">
                                                        Advance Payment</button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Achieved Gross Profit
                                                </td>
                                                <td>
                                                    <button type="button" class="btn btn-primary btn-xs">
                                                        Advance Payment</button>
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
                                                            <button type="button" class="btn btn-primary">
                                                                Advance Payment</button>
                                                            <button type="button" class="btn btn-primary">
                                                                Add Payable</button>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <table class="table table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        Unit Type
                                                                    </th>
                                                                    <th>
                                                                        Tot Units
                                                                    </th>
                                                                    <th>
                                                                        Bill From
                                                                    </th>
                                                                    <th>
                                                                        Currency
                                                                    </th>
                                                                    <th>
                                                                        Charge Amt
                                                                    </th>
                                                                    <th>
                                                                        ROE
                                                                    </th>
                                                                    <th>
                                                                        Payment By
                                                                    </th>
                                                                    <th>
                                                                        Amount (INR)
                                                                    </th>
                                                                    <th>
                                                                        Bill Receipt
                                                                    </th>
                                                                    <th>
                                                                        Upload
                                                                    </th>
                                                                    <th>
                                                                        Edit
                                                                    </th>
                                                                    <th>
                                                                        Delete
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
                                                            <button type="button" class="btn btn-primary">
                                                                Advance Payment</button>
                                                            <button type="button" class="btn btn-primary">
                                                                Add Payable</button>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <table class="table table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        Unit Type
                                                                    </th>
                                                                    <th>
                                                                        Tot Units
                                                                    </th>
                                                                    <th>
                                                                        Bill From
                                                                    </th>
                                                                    <th>
                                                                        Currency
                                                                    </th>
                                                                    <th>
                                                                        Charge Amt
                                                                    </th>
                                                                    <th>
                                                                        ROE
                                                                    </th>
                                                                    <th>
                                                                        Payment By
                                                                    </th>
                                                                    <th>
                                                                        Amount (INR)
                                                                    </th>
                                                                    <th>
                                                                        Bill Receipt
                                                                    </th>
                                                                    <th>
                                                                        Upload
                                                                    </th>
                                                                    <th>
                                                                        Edit
                                                                    </th>
                                                                    <th>
                                                                        Delete
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
                                                            <button type="button" class="btn btn-primary">
                                                                Advance Payment</button>
                                                            <button type="button" class="btn btn-primary">
                                                                Add Payable</button>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <table class="table table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        Unit Type
                                                                    </th>
                                                                    <th>
                                                                        Tot Units
                                                                    </th>
                                                                    <th>
                                                                        Bill From
                                                                    </th>
                                                                    <th>
                                                                        Currency
                                                                    </th>
                                                                    <th>
                                                                        Charge Amt
                                                                    </th>
                                                                    <th>
                                                                        ROE
                                                                    </th>
                                                                    <th>
                                                                        Payment By
                                                                    </th>
                                                                    <th>
                                                                        Amount (INR)
                                                                    </th>
                                                                    <th>
                                                                        Bill Receipt
                                                                    </th>
                                                                    <th>
                                                                        Upload
                                                                    </th>
                                                                    <th>
                                                                        Edit
                                                                    </th>
                                                                    <th>
                                                                        Delete
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
                                                            <button type="button" class="btn btn-primary">
                                                                Advance Payment</button>
                                                            <button type="button" class="btn btn-primary">
                                                                Add Payable</button>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <table class="table table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        Unit Type
                                                                    </th>
                                                                    <th>
                                                                        Tot Units
                                                                    </th>
                                                                    <th>
                                                                        Bill From
                                                                    </th>
                                                                    <th>
                                                                        Currency
                                                                    </th>
                                                                    <th>
                                                                        Charge Amt
                                                                    </th>
                                                                    <th>
                                                                        ROE
                                                                    </th>
                                                                    <th>
                                                                        Payment By
                                                                    </th>
                                                                    <th>
                                                                        Amount (INR)
                                                                    </th>
                                                                    <th>
                                                                        Bill Receipt
                                                                    </th>
                                                                    <th>
                                                                        Upload
                                                                    </th>
                                                                    <th>
                                                                        Edit
                                                                    </th>
                                                                    <th>
                                                                        Delete
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                    <td>
                                                                        icon
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
